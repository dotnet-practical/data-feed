﻿namespace DNQ.DataFeed.Application.Sites.Queries.Dtos;

public class ListSitesPagingDto
{
    public List<SiteDto> Data { get; init; } = new List<SiteDto>();
    public int? Page { get; init; }
    public int? PageSize { get; init; }
    public int? TotalItems { get; init; }
    public int? TotalPages => (TotalItems.HasValue && PageSize.HasValue && PageSize.Value > 0) ? (int)Math.Ceiling((double)TotalItems.Value / PageSize.Value) : 0;

    public ListSitesPagingDto(List<SiteDto> data, int totalItems, int? page, int? pageSize)
    {
        Data = data;
        TotalItems = totalItems;
        Page = page;
        PageSize = pageSize;
    }
}
