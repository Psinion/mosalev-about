using Microsoft.EntityFrameworkCore;
using MOS.Application.Data.Repositories.Index;
using MOS.Data.EF.Access.Contexts;
using MOS.Domain.Entities.Index;

namespace MOS.Data.EF.Access.Repositories.Index;

public class IndexContentsRepository : GenericRepository<IndexContent>, IIndexContentsRepository
{
    public IndexContentsRepository(MainDbContext dbLocalContext) : base(dbLocalContext)
    {
    }

    public async Task<IndexContent?> GetActualIndexContentAsync()
    {
        return await LocalContext.IndexContents
            .Where(x => x.DateDelete == null)
            .OrderBy(x => x.DateCreate)
            .ThenBy(x => x.DateUpdate)
            .Take(1)
            .FirstOrDefaultAsync();
    }
}