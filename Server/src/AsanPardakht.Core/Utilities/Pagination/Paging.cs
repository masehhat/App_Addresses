using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AsanPardakht.Core.Utilities.Pagination;

public static class Paging
{
    public static async Task<PagedData<TResult>> ToPagedDataAsync<TSource, TResult, TKey>(this IQueryable<TSource> source, int pageNumber, int pageSize,
        Expression<Func<TSource, TResult>> selector, Expression<Func<TSource, TKey>> orderKeySelector, bool needCount = false) where TSource : class
    {
        int skipCount = (pageNumber - 1) * pageSize;

        PagedData<TResult> result = new()
        {
            TotalItemsCount = needCount ? await source.CountAsync() : null
        };

        result.TotalPagesCount = needCount ? (result.TotalItemsCount % pageSize == 0 ? result.TotalItemsCount / pageSize : (result.TotalItemsCount / pageSize) + 1) : null;
        result.PageNumber = pageNumber;
        result.PageSize = pageSize;

        IQueryable<TSource> data = source.AsNoTracking();

        data = data.OrderBy(orderKeySelector);

        result.Items = await data.Skip(skipCount)
            .Take(pageSize)
            .Select(selector)
            .ToArrayAsync();

        return result;
    }
}