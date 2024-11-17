using MOS.Domain.Entities.Index;

namespace MOS.Application.Data.Repositories.Index;

public interface IIndexContentsRepository : IGenericRepository<IndexContent>
{
    Task<IndexContent?> GetActualIndexContentAsync();
}