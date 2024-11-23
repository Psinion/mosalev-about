using MOS.Domain.Entities.Resumes;
using MOS.Domain.Enums;

namespace MOS.Application.Data.Repositories.Resumes;

public interface IResumesRepository : IGenericRepository<Resume>
{
    Task UnpinAllByLocaleAsync(Locale locale);
    Task<bool> HasPinnedResumeAsync(Locale locale);
    Task PinResumeAsync(long resumeId, Locale? locale);
}