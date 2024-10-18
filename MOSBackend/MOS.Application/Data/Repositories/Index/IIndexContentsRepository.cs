using MOS.Domain.Entities.Index;
using MOS.Domain.Entities.Users;

namespace MOS.Application.Data.Repositories.Index;

public interface IIndexContentsRepository : IGenericRepository<IndexContent>
{
    Task<IndexContent?> GetActualIndexContentAsync();
}