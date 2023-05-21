namespace AsanPardakht.Core.Models.Views;

public record ApiResponseStructure<TData>
{
    public byte Status { get; init; }
    public string Message { get; init; }
    public TData Data { get; init; }
}