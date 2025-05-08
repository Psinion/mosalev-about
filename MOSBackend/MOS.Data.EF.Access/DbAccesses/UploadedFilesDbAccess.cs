using MOS.Application.Data.DbAccesses;
using MOS.Data.EF.Access.Contexts;
using MOS.Domain.Entities.Files;

namespace MOS.Data.EF.Access.DbAccesses;

public class UploadedFilesDbAccess : IUploadedFilesDbAccess
{
    private readonly MainDbContext context;

    public UploadedFilesDbAccess(MainDbContext context)
    {
        this.context = context;
    }
    
    public IQueryable<UploadedFile> GetUploadedFiles()
    {
        return context.UploadedFiles;
    }
}