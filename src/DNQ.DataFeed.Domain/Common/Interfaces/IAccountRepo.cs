using DNQ.DataFeed.Domain.Accounts;
using System.Linq.Expressions;

namespace DNQ.DataFeed.Domain.Common.Interfaces;

public static class AccountSpecifications
{
    public static Expression<Func<Account, bool>> HasReferenceValue(Guid siteId, Guid platformId, string referenceValue)
    {
        return s => s.SiteId == siteId && s.PlatformId == platformId && s.ReferenceValue == referenceValue;
    }

    public static Expression<Func<Account, bool>> HasInternalId(Guid internalId)
    {
        return s => s.InternalId == internalId;
    }

    public static Expression<Func<Account, bool>> HasAnotherAccountWithSameInternalId(Guid internalId, Guid id)
    {
        return s => s.InternalId == internalId && s.Id != id;
    }

    public static Expression<Func<Account, bool>> HasAnotherAccountWithSameReferenceValue(Guid siteId, Guid platformId, string referenceValue)
    {
        return s => s.SiteId == siteId && s.PlatformId == platformId && s.ReferenceValue == referenceValue;
    }

    public static Expression<Func<Account, bool>> Filter(Guid? platformId, Guid? internalId, string? referenceValue, Guid? siteId, DateTime? startDate, DateTime? endDate, uint? finYear)
    {
        Expression<Func<Account, bool>> predicate = PredicateBuilder.True<Account>();

        if (platformId.HasValue)
        {
            predicate = predicate.And(x => x.PlatformId == platformId.Value);
        }

        if (internalId.HasValue)
        {
            predicate = predicate.And(x => x.InternalId == internalId.Value);
        }

        if (!string.IsNullOrEmpty(referenceValue))
        {
            predicate = predicate.And(x => x.ReferenceValue == referenceValue);
        }

        if (siteId.HasValue)
        {
            predicate = predicate.And(x => x.SiteId == siteId.Value);
        }

        if (startDate.HasValue)
        {
            predicate = predicate.And(x => x.StartDate >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            predicate = predicate.And(x => x.EndDate <= endDate.Value);
        }

        if (finYear.HasValue)
        {
            predicate = predicate.And(x => x.FinYear == finYear.Value);
        }

        return predicate;
    }
}
public interface IAccountRepo
{
    Task AddAccount(Account account);
    Task UpdateAccount(Account account);
    Task RemoveAccount(Account account);
    Task<bool> ExistsAsync(Expression<Func<Account, bool>> predicate);
    Task<Account?> FirstOrDefaultAsync(Expression<Func<Account, bool>> predicate);
    Task<List<Account>> ListAsync(Expression<Func<Account, bool>> predicate, string? sort, int? page, int? pageSize);
    Task<int> CountAsync(Expression<Func<Account, bool>> predicate);
}
