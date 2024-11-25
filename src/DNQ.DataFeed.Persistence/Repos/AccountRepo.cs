using DNQ.DataFeed.Domain.Accounts;
using DNQ.DataFeed.Domain.Common.Interfaces;
using DNQ.DataFeed.Domain.Sites;
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

    public async Task<bool> ExistsAsync(Expression<Func<Account, bool>> predicate)
    {
        return await _dbContext.Accounts.AnyAsync(predicate);
    }

    public async Task<Account?> FirstOrDefaultAsync(Expression<Func<Account, bool>> predicate)
    {
        return await _dbContext.Accounts.FirstOrDefaultAsync(predicate);
    }
}
