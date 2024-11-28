using MediatR;
using System.Text.Json.Serialization;

namespace DNQ.DataFeed.Application.Accounts.Commands.DeleteAccount;

public class DeleteAccountCommand: IRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }

    public DeleteAccountCommand(Guid id)
    {
        Id = id;
    }
}
