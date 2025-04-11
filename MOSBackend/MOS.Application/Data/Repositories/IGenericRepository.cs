using MOS.Domain.Entities;

namespace MOS.Application.Data.Repositories;

public interface IGenericRepository<TEntity, TKey> : IDisposable
    where TEntity : class
    where TKey : struct
{
    IQueryable<TEntity> GetAll();
    Task<bool> ExistsAsync(TKey id, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
    Task<TEntity> CreateAsync(TEntity item, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity item, CancellationToken cancellationToken = default);
    Task<TEntity> CreateOrUpdateAsync(TEntity item, CancellationToken cancellationToken = default);
    Task DeleteAsync(TKey id, CancellationToken cancellationToken = default);
}