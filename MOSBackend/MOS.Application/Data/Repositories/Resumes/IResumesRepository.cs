using MOS.Domain.Entities.Resumes;
using MOS.Domain.Enums;

namespace MOS.Application.Data.Repositories.Resumes;

public interface IResumesRepository : IGenericRepository<Resume>
{
    Task UnpinAllByLocaleAsync(Locale locale);
    Task UnpinResumeAsync(long resumeId, Locale locale);
    Task PinResumeAsync(long resumeId, Locale locale);
}