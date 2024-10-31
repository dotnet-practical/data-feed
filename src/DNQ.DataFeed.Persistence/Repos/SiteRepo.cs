using DNQ.DataFeed.Domain.Common.Interfaces;
using DNQ.DataFeed.Domain.Sites;
using DNQ.DataFeed.Persistence.Extensions;
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

    public async Task<Site?> FirstOrDefaultAsync(Expression<Func<Site, bool>> predicate)
    {
        return await _dbContext.Sites.FirstOrDefaultAsync(predicate);
    }

    public async Task<List<Site>> ListAsync(Expression<Func<Site, bool>> predicate, string? sort)
    {
        var query = AsQueryable();

        query = query.Where(predicate);

        query = query.ApplySort(sort);

        var sites = await query.ToListAsync();

        return sites;
    }

    public async Task RemoveSite(Site site)
    {
         _dbContext.Sites.Remove(site);
    }

    public async Task UpdateSite(Site site)
    {
        _dbContext.Sites.Update(site);
    }
}
