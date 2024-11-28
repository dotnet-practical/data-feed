using DNQ.DataFeed.Application.Accounts.Queries.Dtos;
using MediatR;

namespace DNQ.DataFeed.Application.Accounts.Queries.GetAccount;

public class GetAccountCommand : IRequest<AccountDto>
{
    public Guid Id { get; set; }

    public GetAccountCommand(Guid id)
    {
        Id = id;
    }
}
