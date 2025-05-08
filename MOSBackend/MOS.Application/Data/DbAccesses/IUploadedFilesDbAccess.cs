using MOS.Domain.Entities.Files;

namespace MOS.Application.Data.DbAccesses;

public interface IUploadedFilesDbAccess
{
    IQueryable<UploadedFile> GetUploadedFiles();
}