using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vorex.Application.others;

public class PaginatedResponse<T>
{
    public IEnumerable<T> Data { get; set; }

    public PaginationInfo Pagination { get; set; }

    public PaginatedResponse(IEnumerable<T> data, int totalItems, int page, int pageSize)
    {
        Data = data;
        Pagination = new PaginationInfo
        {
            PageIndex = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
        };
    }

    public PaginatedResponse(IEnumerable<T> data, PaginationInfo paginationInfo)
    {
        Data = data;
        Pagination = paginationInfo;
    }
}

public record PaginationInfo
{
    public int PageIndex { get; init; }
    public int PageSize { get; init; }
    public int TotalPages { get; init; }
    public int TotalItems { get; init; }
}