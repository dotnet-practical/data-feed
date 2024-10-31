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

public interface ISiteRepo
{
    Task AddSite(Site site);
    Task<List<Site>> ListAsync(Expression<Func<Site, bool>> predicate);
    Task<bool> ExistsAsync(Expression<Func<Site, bool>> predicate);
    IQueryable<Site> AsQueryable();
}