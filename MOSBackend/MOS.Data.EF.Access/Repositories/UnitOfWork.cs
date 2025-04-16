using MOS.Application.Data.Repositories;
using MOS.Data.EF.Access.Contexts;

namespace MOS.Data.EF.Access.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly MainDbContext localContext;

    public UnitOfWork(MainDbContext localContext)
    {
        this.localContext = localContext;
    }


    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await localContext.SaveChangesAsync(cancellationToken);
    }
}