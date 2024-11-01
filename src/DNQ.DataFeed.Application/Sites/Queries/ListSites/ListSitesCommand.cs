using DNQ.DataFeed.Application.Sites.Queries.Dtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace DNQ.DataFeed.Application.Sites.Queries.ListSites;

public class ListSitesCommand: IRequest<List<SiteDto>>
{
    [Display(Name = "code")]
    public string? Code { get; init; }

    [Display(Name = "name")]
    public string? Name { get; init; }

    [Display(Name = "sort")]
    public string? Sort { get; init; }

    public ListSitesCommand(string? code, string? name, string? sort) 
    {
        Code = code;
        Name = name;
        Sort = sort;
    }
}
