using DNQ.DataFeed.Domain.Common.Exceptions;
using DNQ.DataFeed.Domain.Common.Interfaces;

namespace DNQ.DataFeed.Domain.Accounts;

public interface IAccountManager
{
    Task<Account> CreateAsync(Guid platformId, Guid siteId, Guid internalId, string referenceValue, DateTime startDate, DateTime? endDate, uint finYear);
    Task UpdateAsync(Account account, Guid platformId, Guid siteId, Guid internalId, string referenceValue, DateTime startDate, DateTime? endDate, uint finYear);
    Task<Account> SynchronizeAsync(Guid platformId, Guid siteId, Guid internalId, string referenceValue, DateTime startDate, DateTime? endDate, uint finYear);
}
public class AccountManager : IAccountManager
{
    private readonly IAccountRepo _accountRepo;
    public AccountManager(IAccountRepo accountRepo)
    {
        _accountRepo = accountRepo;
    }

    public async Task<Account> CreateAsync(Guid platformId, Guid siteId, Guid internalId, string referenceValue, DateTime startDate, DateTime? endDate, uint finYear)
    {
        if (await _accountRepo.ExistsAsync(AccountSpecifications.HasInternalId(internalId)))
        {
            throw new BussinessException("Cannot have two accounts with the same internal id.");
        }

        return new Account(platformId, siteId, internalId, referenceValue, startDate, endDate, finYear);
    }

    public async Task UpdateAsync(Account account, Guid platformId, Guid siteId, Guid internalId, string referenceValue, DateTime startDate, DateTime? endDate, uint finYear)
    {
        if (await _accountRepo.ExistsAsync(AccountSpecifications.HasAnotherAccountWithSameInternalId(internalId, account.Id)))
        {
            throw new BussinessException("Cannot have two accounts with the same internal id.");
        }

        if (await _accountRepo.ExistsAsync(AccountSpecifications.HasAnotherAccountWithSameReferenceValue(siteId, platformId, referenceValue)))
        {
            throw new BussinessException("Cannot have two accounts with the same reference value.");
        }

        account.Update(platformId, siteId, internalId, referenceValue, startDate, endDate, finYear);
    }

    public async Task<Account> SynchronizeAsync(Guid platformId, Guid siteId, Guid internalId, string referenceValue, DateTime startDate, DateTime? endDate, uint finYear)
    {
        /* internalId is unique */
        var account = await _accountRepo.FirstOrDefaultAsync(AccountSpecifications.HasInternalId(internalId));
        if (account == null) return new Account(platformId, siteId, internalId, referenceValue, startDate, endDate, finYear);

        return account;
    }
}
