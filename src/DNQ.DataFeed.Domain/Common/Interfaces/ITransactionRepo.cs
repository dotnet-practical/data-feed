using DNQ.DataFeed.Domain.Transactions;
using System.Linq.Expressions;

namespace DNQ.DataFeed.Domain.Common.Interfaces;

public interface ITransactionRepo
{
    Task AddTransaction(Transaction Transaction);
    Task UpdateTransaction(Transaction Transaction);
    Task RemoveTransaction(Transaction Transaction);
    Task<bool> ExistsAsync(Expression<Func<Transaction, bool>> predicate);
    Task<Transaction?> FirstOrDefaultAsync(Expression<Func<Transaction, bool>> predicate);
    Task<List<Transaction>> ListAsync(Expression<Func<Transaction, bool>> predicate, string? sort, int? page, int? pageSize);
    Task<int> CountAsync(Expression<Func<Transaction, bool>> predicate);
}
