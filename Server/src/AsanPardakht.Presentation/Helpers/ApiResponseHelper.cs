using AsanPardakht.Core.Models.Views;
using AsanPardakht.Core.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace AsanPardakht.Presentation.Helpers;

public static class ApiResponseHelper
{
    public static ObjectResult AsApiActionResult<TData>(this ApiOperationResult<TData> operationResult)
    {
        if (operationResult.ErrorCode.HasValue)
            return new ObjectResult(new ApiResponseStructure<TData>
            {
                Data = default,
                Status = (byte)operationResult.ErrorCode.Value,
                Message = operationResult.ErrorCode.Value.GetDescriptionFromEnumValue()
            })
            {
                StatusCode = 400
            };
        else
            return new ObjectResult(new ApiResponseStructure<TData>
            {
                Data = operationResult.Data,
                Message = "موفق",
                Status = 1
            })
            {
                StatusCode = 200
            };
    }
}
