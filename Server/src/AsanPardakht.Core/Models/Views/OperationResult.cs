using AsanPardakht.Core.Enums;

namespace AsanPardakht.Core.Models.Views;

public class ApiOperationResult<TData>
{
    public TData Data { get; private set; }
    public ErrorCode? ErrorCode { get; private set; }

    public static ApiOperationResult<TData> BuildSuccess(TData data)
    {
        return new ApiOperationResult<TData>
        {
            Data = data
        };
    }

    public static ApiOperationResult<TData> BuildFailure(ErrorCode errorCode)
    {
        return new ApiOperationResult<TData>
        {
            Data = default,
            ErrorCode = errorCode
        };
    }
}