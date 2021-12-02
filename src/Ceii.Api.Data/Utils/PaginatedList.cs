using Ceii.Api.Data.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Ceii.Api.Data.Utils;

/// <summary>
/// List used to build pagination. Accepts generic type
/// </summary>
public class PaginatedList<T> : List<T>
{
    /// <summary>
    /// Indicates current page.
    /// </summary>
    public int PageIndex { get; private set; }

    /// <summary>
    /// Indicates total pages available. Calculated with count.
    /// </summary>
    public int TotalPages { get; private set; }

    /// <summary>
    /// Instantiates a new PaginatedList, uses List as base type for elements.
    /// </summary>
    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        AddRange(items);
    }

    public bool HasPreviousPage => PageIndex > 1;

    public bool HasNextPage => PageIndex < TotalPages;

    /// <summary>
    /// Build a paginated list asynchronously from an IQueryabel source (LINQ Query)
    /// </summary>
    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T>? source, PagingInfo info)
    {
        var (pageIndex, pageSize) = info;

        var count = await source.CountAsync();
        var items = await source.Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }
}