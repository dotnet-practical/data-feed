using DNQ.DataFeed.Application.Sites.Queries.Dtos;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DNQ.DataFeed.Application.Sites.Queries.ListSites;

public class ListSitesCommand: IRequest<List<SiteDto>>
{
    [Display(Name = "code")]
    public string? Code { get; init; }

    [Display(Name = "name")]
    public string? Name { get; init; }

    [Display(Name = "sort")]
    public string? Sort { get; init; }
}
