using AsanPardakht.Core.Models.Views;
using AsanPardakht.Core.Services;
using AsanPardakht.Core.Utilities.Pagination;
using AsanPardakht.Presentation.Controllers.LocationControllers.Models;
using AsanPardakht.Presentation.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace AsanPardakht.Presentation.Controllers.LocationControllers;

[Route(LocationRoutes.BaseRoute)]
[ApiController]
public class LocationController : ControllerBase
{
    private readonly ILocationService _locationService;

    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    //TODO: Check Console App IP as a service that has permission to get all locations (we could implement Action Filter)
    [HttpGet]
    [ProducesResponseType(typeof(ApiOperationResult<PagedData<LocationView>>), 200)]
    public async Task<IActionResult> GetLocationsAsync(int? userId, string province, string city, string address, bool sortByCreatedAt,
     int? fromDateTime, int? toDateTime, int pageNumber = 1, int pageSize = 10)
    {
        ApiOperationResult<PagedData<LocationView>> result = await _locationService
            .GetLocationsAsync(userId, province, city, address, sortByCreatedAt, fromDateTime, toDateTime, pageNumber, pageSize);

        return result.AsApiActionResult();
    }

    //TODO: Authenticate if UserId filtered (use Identity [Authorize])
    [HttpPost]
    [ProducesResponseType(typeof(ApiOperationResult<int>), 200)]
    public async Task<IActionResult> AddLocationAsync([FromBody] AddLocationModel model)
    {
        ApiOperationResult<int> result = await _locationService.AddLocationAsync(model.UserId, model.Province, model.City, model.Address);

        return result.AsApiActionResult();
    }

    //TODO: Authenticate if UserId filtered (use Identity [Authorize])
    [HttpPut(LocationRoutes.ModifyOrRemoveRoute)]
    [ProducesResponseType(typeof(ApiOperationResult<bool>), 200)]
    public async Task<IActionResult> ModifyLocationAsync(int id, [FromBody] ModifyLocationModel model)
    {
        ApiOperationResult<bool> result = await _locationService.ModifyLocationAsync(id, model.Province, model.City, model.Address);

        return result.AsApiActionResult();
    }

    //TODO: Authenticate if UserId filtered (use Identity [Authorize])
    [HttpDelete(LocationRoutes.ModifyOrRemoveRoute)]
    [ProducesResponseType(typeof(ApiOperationResult<bool>), 200)]
    public async Task<IActionResult> RemoveLocationAsync(int id)
    {
        ApiOperationResult<bool> result = await _locationService.RemoveLocationAsync(id);

        return result.AsApiActionResult();
    }
}