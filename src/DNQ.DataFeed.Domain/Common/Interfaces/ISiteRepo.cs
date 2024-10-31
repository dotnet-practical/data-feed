using DNQ.DataFeed.Domain.Sites;
using System.Linq.Expressions;

namespace DNQ.DataFeed.Domain.Common.Interfaces;

public static class SiteSpecifications
{
    public static Expression<Func<Site, bool>> HasCode(string code)
    {
        return s => s.Code == code;
    }

    public static Expression<Func<Site, bool>> HasName(string name)
    {
        return s => s.Name == name;
    }
}

public static class PredicateBuilder
{
    public static Expression<Func<T, bool>> True<T>() { return x => true; }
    public static Expression<Func<T, bool>> False<T>() { return x => false; }

    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                        Expression<Func<T, bool>> expr2)
    {
        var parameter = Expression.Parameter(typeof(T));

        var body = Expression.AndAlso(
            Expression.Invoke(expr1, parameter),
            Expression.Invoke(expr2, parameter)
        );

        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    // Tương tự cho Or nếu cần
}

public interface ISiteRepo
{
    Task AddSite(Site site);
    Task UpdateSite(Site site);
    Task RemoveSite(Site site);
    Task<Site> FirstOrDefaultAsync(Expression<Func<Site, bool>> predicate);
    Task<List<Site>> ListAsync(Expression<Func<Site, bool>> predicate, string? sort = null);
    Task<bool> ExistsAsync(Expression<Func<Site, bool>> predicate);
    IQueryable<Site> AsQueryable();
}