using DNQ.DataFeed.Domain.Accounts;
using DNQ.DataFeed.Domain.Common.Interfaces;
using DNQ.DataFeed.Domain.Sites;
using DNQ.DataFeed.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DNQ.DataFeed.Persistence.Repos;

public class AccountRepo : IAccountRepo
{
    private readonly AppDbContext _dbContext;

    public AccountRepo(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAccount(Account account)
    {
        await _dbContext.Accounts.AddAsync(account);
    }

    public async Task<int> CountAsync(Expression<Func<Account, bool>> predicate)
    {
        return await _dbContext.Accounts.CountAsync(predicate);
    }

    public async Task<bool> ExistsAsync(Expression<Func<Account, bool>> predicate)
    {
        return await _dbContext.Accounts.AnyAsync(predicate);
    }

    public async Task<Account?> FirstOrDefaultAsync(Expression<Func<Account, bool>> predicate)
    {
        return await _dbContext.Accounts.FirstOrDefaultAsync(predicate);
    }

    public async Task<List<Account>> ListAsync(Expression<Func<Account, bool>> predicate, string? sort, int? page, int? pageSize)
    {
        var query = _dbContext.Accounts.AsQueryable();

        query = query.Where(predicate);

        query = query.Sorting(sort);

        query = query.Paging(page, pageSize);

        var accounts = await query.ToListAsync();

        return accounts;
    }

    public async Task RemoveAccount(Account account)
    {
        _dbContext.Accounts.Remove(account);
        await Task.CompletedTask;
    }

    public async Task UpdateAccount(Account account)
    {
        _dbContext.Accounts.Update(account);
        await Task.CompletedTask;
    }
}
