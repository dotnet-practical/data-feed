using DNQ.DataFeed.Domain.Common.Exceptions;
using DNQ.DataFeed.Domain.Common.Interfaces;
namespace DNQ.DataFeed.Domain.Accounts;

public interface IAccountManager
{
    Task<Account> CreateAsync(Guid platformId, Guid siteId, Guid internalId, string referenceValue, DateTime startDate, DateTime? endDate, uint finYear);
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
        if (await _accountRepo.ExistsAsync(AccountSpecifications.HasReferenceValue(siteId, platformId, internalId, referenceValue)))
        {
            throw new BussinessException("Cannot have two accounts with the same reference value, internal id at site.");
        }

        return new Account(platformId, siteId, internalId, referenceValue, startDate, endDate, finYear);
    }

    public async Task<Account> SynchronizeAsync(Guid platformId, Guid siteId, Guid internalId, string referenceValue, DateTime startDate, DateTime? endDate, uint finYear)
    {
        /* int platformId, int siteId, Guid internalId, string referenceValue is unique */
        var account = await _accountRepo.FirstOrDefaultAsync(AccountSpecifications.HasReferenceValue(siteId, platformId, internalId, referenceValue));
        if (account == null) return new Account(platformId, siteId, internalId, referenceValue, startDate, endDate, finYear);

        return account;
    }
}
