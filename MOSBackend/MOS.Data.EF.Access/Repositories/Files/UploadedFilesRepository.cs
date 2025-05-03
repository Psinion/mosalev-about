using MOS.Application.Data.Repositories.Files;
using MOS.Data.EF.Access.Contexts;
using MOS.Domain.Entities.Files;

namespace MOS.Data.EF.Access.Repositories.Files;

public class UploadedFilesRepository : GenericRepository<UploadedFile, int>, IUploadedFilesRepository
{
    public UploadedFilesRepository(MainDbContext dbLocalContext) : base(dbLocalContext)
    {
    }
}