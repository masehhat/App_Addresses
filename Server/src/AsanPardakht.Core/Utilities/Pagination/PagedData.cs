using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsanPardakht.Core.Utilities.Pagination;

public class PagedData<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int? TotalPagesCount { get; set; }
    public int? TotalItemsCount { get; set; }
    public T[] Items { get; set; }
}