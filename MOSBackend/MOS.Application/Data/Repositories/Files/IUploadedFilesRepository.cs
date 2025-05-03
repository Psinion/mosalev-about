using MOS.Domain.Entities.Files;

namespace MOS.Application.Data.Repositories.Files;

public interface IUploadedFilesRepository : IGenericRepository<UploadedFile, int>
{
}