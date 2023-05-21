using AsanPardakht.Core.Data;
using AsanPardakht.Core.Entities;
using AsanPardakht.Core.Enums;
using AsanPardakht.Core.Models.Views;
using AsanPardakht.Core.Utilities.Pagination;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AsanPardakht.Core.Services;

public class LocationService : ILocationService
{
    private readonly IMapper _mapper;
    private readonly AsanPardakhtDbContext _context;

    public LocationService(AsanPardakhtDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ApiOperationResult<PagedData<LocationView>>> GetLocationsAsync(int? userId, string province, string city, string address, bool sortByCreatedAt,
       int? fromDateTime, int? toDateTime, int pageNumber, int pageSize)
    {
        IQueryable<Location> query = _context.Locations
            .AsQueryable();

        if (userId.HasValue)
            query = query.Where(x => x.UserId == userId.Value);

        if (!string.IsNullOrWhiteSpace(province))
            query = query.Where(x => x.Province.Contains(province.Trim()));

        if (!string.IsNullOrWhiteSpace(city))
            query = query.Where(x => x.City.Contains(city.Trim()));

        if (!string.IsNullOrWhiteSpace(address))
            query = query.Where(x => x.Address.Contains(address.Trim()));

        if (fromDateTime.HasValue)
            query = query.Where(x => x.CreatedAt >= fromDateTime.Value);

        if (toDateTime.HasValue)
            query = query.Where(x => x.CreatedAt <= toDateTime.Value);

        IQueryable<LocationView> projectedQuery = query.ProjectTo<LocationView>(_mapper.ConfigurationProvider);

        PagedData<LocationView> result = sortByCreatedAt ?
            await projectedQuery.ToPagedDataAsync(pageNumber, pageSize, x => x, x => x.CreatedAt, needCount: true) :
            await projectedQuery.ToPagedDataAsync(pageNumber, pageSize, x => x, x => x.CombinedAddress, needCount: true);

        return ApiOperationResult<PagedData<LocationView>>.BuildSuccess(result);
    }

    public async Task<ApiOperationResult<int>> AddLocationAsync(int userId, string province, string city, string address)
    {
        ErrorCode? errorCode = ValidateLocation(province, city, address);

        if (errorCode.HasValue)
            return ApiOperationResult<int>.BuildFailure(errorCode.Value);

        bool alreadyExists = await _context.Locations
            //TODO: Use Spec pattern
            .Where(x => x.Province == province.Trim() && x.City == city.Trim() && x.Address == address.Trim())
            .AnyAsync();

        bool userIdIsValid = await _context.Users.AnyAsync(x => x.Id == userId);

        if (!userIdIsValid)
            return ApiOperationResult<int>.BuildFailure(ErrorCode.UserIdIsInvalid);

        if (alreadyExists)
            return ApiOperationResult<int>.BuildFailure(ErrorCode.LocationIsDuplicated);

        Location location = new(userId, province, city, address);

        _context.Locations.Add(location);

        await _context.SaveChangesAsync();

        return ApiOperationResult<int>.BuildSuccess(location.Id);
    }

    public async Task<ApiOperationResult<bool>> ModifyLocationAsync(int id, string province, string city, string address)
    {
        ErrorCode? errorCode = ValidateLocation(province, city, address);

        if (errorCode.HasValue)
            return ApiOperationResult<bool>.BuildFailure(errorCode.Value);

        Location location = await _context.Locations
            .FirstOrDefaultAsync(x => x.Id == id);

        if (location is null)
            return ApiOperationResult<bool>.BuildFailure(ErrorCode.LocationNotFound);

        location.Modify(province, city, address);
        await _context.SaveChangesAsync();

        return ApiOperationResult<bool>.BuildSuccess(true);
    }

    public async Task<ApiOperationResult<bool>> RemoveLocationAsync(int id)
    {
        Location location = await _context.Locations
            .FirstOrDefaultAsync(x => x.Id == id);

        if (location is null)
            return ApiOperationResult<bool>.BuildFailure(ErrorCode.LocationNotFound);

        _context.Locations.Remove(location);

        await _context.SaveChangesAsync();

        return ApiOperationResult<bool>.BuildSuccess(true);
    }

    private static ErrorCode? ValidateLocation(string province, string city, string address)
    {
        if (string.IsNullOrWhiteSpace(province))
            return ErrorCode.ProvinceIsInvalid;

        if (province.Length > 50)
            return ErrorCode.ProvinceIsInvalid;

        if (string.IsNullOrWhiteSpace(city))
            return ErrorCode.CityIsInvalid;

        if (city.Length > 50)
            return ErrorCode.CityIsInvalid;

        if (string.IsNullOrWhiteSpace(address))
            return ErrorCode.AddressIsInvalid;

        return null;
    }
}