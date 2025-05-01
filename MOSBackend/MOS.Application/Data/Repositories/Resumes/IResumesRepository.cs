using MOS.Domain.Entities.Resumes;
using MOS.Domain.Enums;

namespace MOS.Application.Data.Repositories.Resumes;

public interface IResumesRepository : IAuditableRepository<Resume, int>
{
    Task<Resume?> GetByIdWithRelationsAsync(int id, CancellationToken cancellationToken = default);
    
    Task UnpinAllByLocaleAsync(Locale locale);
    
    Task<Resume?> GetPinnedResumeAsync(Locale locale);
    
    Task PinResumeAsync(int resumeId, Locale? locale);
}