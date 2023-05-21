namespace AsanPardakht.Consumer.App;

public class PagedData<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int? TotalPagesCount { get; set; }
    public int? TotalItemsCount { get; set; }
    public T[] Items { get; set; }
}