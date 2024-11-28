using DNQ.DataFeed.Application.Sites.Queries.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DNQ.DataFeed.Application.Sites.Queries.ListSitesPaging;

public class ListSitesPagingCommand : IRequest<ListSitesPagingDto>
{
    public string? Code { get; init; }
    public string? Name { get; init; }
    public string? Sort { get; init; }
    public int? Page { get; set; }
    public int? PageSize { get; set; } 
    public void AllocateDefaultValueIfRequired()
    {
        Page = Page.HasValue ? Page.Value : 1;
        PageSize = PageSize.HasValue ? PageSize.Value : 10;
    }
}
