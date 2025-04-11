using MOS.Domain.Entities.Resumes;
using MOS.Domain.Enums;

namespace MOS.Application.Data.Repositories.Resumes;

public interface IResumesRepository : IAuditableRepository<Resume, long>
{
    Task<Resume?> GetByIdWithRelationsAsync(long id, CancellationToken cancellationToken = default);
    
    Task UnpinAllByLocaleAsync(Locale locale);
    
    Task<Resume?> GetPinnedResumeAsync(Locale locale);
    
    Task PinResumeAsync(long resumeId, Locale? locale);
}