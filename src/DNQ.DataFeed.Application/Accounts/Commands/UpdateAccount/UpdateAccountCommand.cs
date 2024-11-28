using MediatR;
using System.Text.Json.Serialization;

namespace DNQ.DataFeed.Application.Accounts.Commands.UpdateAccount;

public class UpdateAccountCommand: IRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public Guid PlatformId { get; init; }
    public Guid SiteId { get; init; }
    public Guid InternalId { get; init; }
    public string ReferenceValue { get; init; } = default!;
    public DateTime StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public uint FinYear { get; init; }
}
