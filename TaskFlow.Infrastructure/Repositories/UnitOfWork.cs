using System.Collections.Concurrent;
using TaskFlow.Domain.Interfaces;
using TaskFlow.Infrastructure.Data;

namespace TaskFlow.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;
    private readonly ConcurrentDictionary<Type, object> _repositories = new();

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IRepository<T> Repository<T>() where T : class
    {
        return (IRepository<T>)_repositories.GetOrAdd(typeof(T),
            _ => new GenericRepository<T>(_dbContext));
    }

    public async Task<int> CompleteAsync() => await _dbContext.SaveChangesAsync();

    public void Dispose() => _dbContext.Dispose();
}