using MediatR;
using System.ComponentModel.DataAnnotations;

namespace DNQ.DataFeed.Application.Sites.Commands.CreateSite;

public record CreateSiteCommand : IRequest<Guid>
{
    [Required] /* Not null or empty */
    [StringLength(50)]
    public string Code { get; init; } = null!;

    [Required]
    [StringLength(100)]
    public string Name { get; init; } = null!;
}