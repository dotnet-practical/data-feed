using DNQ.DataFeed.Application.Sites.Queries.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DNQ.DataFeed.Application.Sites.Queries.ListSitesPaging;

public class ListSitesPagingCommand : IRequest<ListSitesPagingDto>
{
    [FromQuery(Name = "code")]
    public string? Code { get; init; }

    [FromQuery(Name = "name")]
    public string? Name { get; init; }

    [FromQuery(Name = "sort")]
    public string? Sort { get; init; }

    [FromQuery(Name = "page")]
    public int? Page { get; set; }

    [FromQuery(Name = "page_size")]
    public int? PageSize { get; set; } 

    public void AllocateDefaultValueIfRequired()
    {
        Page = Page.HasValue ? Page.Value : 1;
        PageSize = PageSize.HasValue ? PageSize.Value : 10;
    }
}
