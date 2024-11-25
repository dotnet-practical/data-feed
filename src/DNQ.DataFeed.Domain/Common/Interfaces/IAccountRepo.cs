using DNQ.DataFeed.Domain.Accounts;
using DNQ.DataFeed.Domain.Sites;
using System.Linq.Expressions;

namespace DNQ.DataFeed.Domain.Common.Interfaces;

public static class AccountSpecifications
{
    public static Expression<Func<Account, bool>> HasReferenceValue(Guid siteId, Guid platformId, Guid internalId, string referenceValue)
    {
        return s => s.SiteId == siteId && s.PlatformId == platformId && s.InternalId == internalId && s.ReferenceValue == referenceValue;
    }

    public static Expression<Func<Account, bool>> HasInternalId(Guid internalId)
    {
        return s => s.InternalId == internalId;
    }
}
public interface IAccountRepo
{
    Task AddAccount(Account account);
    Task<bool> ExistsAsync(Expression<Func<Account, bool>> predicate);
    Task<Account?> FirstOrDefaultAsync(Expression<Func<Account, bool>> predicate);
}
