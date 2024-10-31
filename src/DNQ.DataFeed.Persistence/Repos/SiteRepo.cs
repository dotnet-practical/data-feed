using DNQ.DataFeed.Domain.Common.Interfaces;
using DNQ.DataFeed.Domain.Sites;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DNQ.DataFeed.Persistence.Repos;

public class SiteRepo : ISiteRepo
{
    private readonly AppDbContext _dbContext;

    public SiteRepo(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddSite(Site site)
    {
        await _dbContext.Sites.AddAsync(site);
    }

    public IQueryable<Site> AsQueryable()
    {
        return _dbContext.Sites.AsQueryable();
    }

    public async Task<bool> ExistsAsync(Expression<Func<Site, bool>> predicate)
    {
        return await _dbContext.Sites.AnyAsync(predicate);
    }

    public async Task<List<Site>> ListAsync(Expression<Func<Site, bool>> predicate)
    {
        return await _dbContext.Sites.Where(predicate).ToListAsync();
    }
}
