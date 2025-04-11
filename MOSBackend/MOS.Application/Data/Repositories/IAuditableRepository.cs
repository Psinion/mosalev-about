using MOS.Domain.Entities;

namespace MOS.Application.Data.Repositories;

public interface IAuditableRepository<TEntity, TKey> : IDisposable
    where TEntity : IAuditableEntity<TKey>
    where TKey : struct
{
    IQueryable<TEntity> GetAll();
    Task<bool> ExistsAsync(long id, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<TEntity> CreateAsync(TEntity item, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity item, CancellationToken cancellationToken = default);
    Task<TEntity> CreateOrUpdateAsync(TEntity item, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity item, CancellationToken cancellationToken = default);
}