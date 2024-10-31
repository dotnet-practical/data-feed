using MediatR;
using System.ComponentModel.DataAnnotations;

namespace DNQ.DataFeed.Application.Sites.Commands.CreateSite;

public record CreateSiteCommand : IRequest<Guid>
{
    public string Code { get; init; } = null!;
    public string Name { get; init; } = null!;
}