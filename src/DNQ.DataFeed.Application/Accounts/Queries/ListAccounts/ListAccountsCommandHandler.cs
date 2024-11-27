using DNQ.DataFeed.Application.Accounts.Queries.Dtos;
using DNQ.DataFeed.Domain.Common.Interfaces;
using MediatR;

namespace DNQ.DataFeed.Application.Accounts.Queries.ListAccounts;

public class ListAccountsCommandHandler : IRequestHandler<ListAccountsCommand, AccountPagingDto>
{
    private readonly IAccountRepo _accountRepo;
    public ListAccountsCommandHandler(IAccountRepo accountRepo) 
    {
        _accountRepo = accountRepo;
    }
    public async Task<AccountPagingDto> Handle(ListAccountsCommand request, CancellationToken cancellationToken)
    {
        request.AllocateDefaultValueIfRequired();

        var filter = AccountSpecifications.Filter(request.PlatformId, request.InternalId, request.ReferenceValue, request.SiteId, request.StartDate, request.EndDate, request.FinYear);
        var accounts = await _accountRepo.ListAsync(filter, request.Sort, request.Page, request.PageSize);
        var totalItems = await _accountRepo.CountAsync(filter);
        var accountDtos = accounts.Select(x => new AccountDto { EndDate = x.EndDate, FinYear = x.FinYear, Id = x.Id, InternalId = x.InternalId, PlatformId = x.PlatformId, ReferenceValue = x.ReferenceValue, SiteId = x.SiteId, StartDate = x.StartDate }).ToList();
        var accountPagingDto = new AccountPagingDto(accountDtos, totalItems, request.Page, request.PageSize);

        return accountPagingDto;
    }
}
