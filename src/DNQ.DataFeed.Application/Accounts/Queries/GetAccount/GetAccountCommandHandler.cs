using DNQ.DataFeed.Application.Accounts.Queries.Dtos;
using DNQ.DataFeed.Application.Common.Exceptions;
using DNQ.DataFeed.Domain.Common.Interfaces;
using MediatR;

namespace DNQ.DataFeed.Application.Accounts.Queries.GetAccount;

public class GetAccountCommandHandler : IRequestHandler<GetAccountCommand, AccountDto>
{
    private readonly IAccountRepo _accountRepo;
    public GetAccountCommandHandler(IAccountRepo accountRepo)
    {
        _accountRepo = accountRepo;
    }
    public async Task<AccountDto> Handle(GetAccountCommand request, CancellationToken cancellationToken)
    {
        var accountDomain = await _accountRepo.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (accountDomain == null)
        {
            throw new NotFoundException($"The account '{request.Id}' isn't found.");
        }

        return new AccountDto
        {
            Id = accountDomain.Id,
            PlatformId = accountDomain.PlatformId,
            InternalId = accountDomain.InternalId,
            ReferenceValue = accountDomain.ReferenceValue,
            SiteId = accountDomain.SiteId,
            StartDate = accountDomain.StartDate,
            EndDate = accountDomain.EndDate,
            FinYear = accountDomain.FinYear
        };
    }
}
