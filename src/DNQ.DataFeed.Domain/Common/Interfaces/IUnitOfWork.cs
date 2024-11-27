namespace DNQ.DataFeed.Domain.Common.Interfaces;

public interface IUnitOfWork
{
    Task CommitChangesAsync(CancellationToken cancellationToken);
}
