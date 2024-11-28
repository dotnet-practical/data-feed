using DNQ.DataFeed.Application.Sites.Queries.Dtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace DNQ.DataFeed.Application.Sites.Queries.ListSites;

public class ListSitesCommand: IRequest<List<SiteDto>>
{
    public string? Code { get; init; }
    public string? Name { get; init; }
    public string? Sort { get; init; }

    public ListSitesCommand(string? code, string? name, string? sort) 
    {
        Code = code;
        Name = name;
        Sort = sort;
    }
}
