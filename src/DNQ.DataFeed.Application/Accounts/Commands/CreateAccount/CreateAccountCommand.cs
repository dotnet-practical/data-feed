using MediatR;

namespace DNQ.DataFeed.Application.Accounts.Commands.CreateAccount;

public record CreateAccountCommand : IRequest<Guid>
{
    public Guid PlatformId { get; init; }
    public Guid SiteId { get; init; }
    public Guid InternalId { get; init; }
    public string ReferenceValue { get; init; } = default!;
    public DateTime StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public uint FinYear { get; init; }
}