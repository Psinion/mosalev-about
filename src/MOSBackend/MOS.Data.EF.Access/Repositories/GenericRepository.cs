using Microsoft.EntityFrameworkCore;
using MOS.Application.Data.Repositories;
using MOS.Data.EF.Access.Contexts;
using MOS.Domain.Entities.Base;

namespace MOS.Data.EF.Access.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> 
    where TEntity : class, IEntity, new()
{
    protected readonly MainDbContext LocalContext;
    protected readonly DbSet<TEntity> LocalSet;

    public GenericRepository(MainDbContext dbLocalContext) {
        LocalContext = dbLocalContext;
        LocalSet = dbLocalContext.Set<TEntity>();
    }

    public virtual IQueryable<TEntity> GetAll() => LocalSet.AsQueryable();
    
    public virtual async Task<bool> ExistsAsync(long id, CancellationToken cancellationToken = default) 
        => await LocalSet.FindAsync(id) != null;

    public virtual async Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        => await LocalSet.FindAsync(new object[] { id }, cancellationToken);

    public virtual async Task<TEntity> CreateAsync(TEntity item, CancellationToken cancellationToken = default)
    {
        if (item is null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        await LocalContext.AddAsync(item, cancellationToken);
        await LocalContext.SaveChangesAsync(cancellationToken);

        return item;
    }

    public virtual async Task<TEntity> CreateOrUpdateAsync(TEntity item, CancellationToken cancellationToken = default)
    {
        if (item is null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        LocalContext.Entry(item).State = item.Id == 0 
            ? EntityState.Added : EntityState.Modified;

        await LocalContext.SaveChangesAsync(cancellationToken);

        return item;
    }

    public virtual async Task UpdateAsync(TEntity item, CancellationToken cancellationToken = default)
    {
        if (item is null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        LocalContext.Update(item);
        await LocalContext.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        LocalContext.Remove(new TEntity { Id = id });
        await LocalContext.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        LocalContext.Dispose();
    }
}