using AsanPardakht.Core.Models.Views;
using AsanPardakht.Core.Utilities.Pagination;

namespace AsanPardakht.Core.Services;

public interface ILocationService
{
    Task<ApiOperationResult<PagedData<LocationView>>> GetLocationsAsync(int? userId, string province, string city, string address, bool sortByCreatedAt,
       int? fromDateTime, int? toDateTime, int pageNumber, int pageSize);

    Task<ApiOperationResult<int>> AddLocationAsync(int userId, string province, string city, string address);

    Task<ApiOperationResult<bool>> ModifyLocationAsync(int id, string province, string city, string address);

    Task<ApiOperationResult<bool>> RemoveLocationAsync(int id);
}