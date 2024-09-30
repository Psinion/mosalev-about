using MOS.Domain.Entities.Base;

namespace MOS.Application.Data.Repositories;

public interface IGenericRepository<TEntity> : IDisposable
    where TEntity : IEntity
{
    IQueryable<TEntity> GetAll();
    Task<bool> ExistsAsync(long id, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<TEntity> CreateAsync(TEntity item, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity item, CancellationToken cancellationToken = default);
    Task<TEntity> CreateOrUpdateAsync(TEntity item, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}