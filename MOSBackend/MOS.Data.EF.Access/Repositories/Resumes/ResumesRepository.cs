using Microsoft.EntityFrameworkCore;
using MOS.Application.Data.Repositories.Resumes;
using MOS.Data.EF.Access.Contexts;
using MOS.Domain.Entities.Resumes;
using MOS.Domain.Enums;

namespace MOS.Data.EF.Access.Repositories.Resumes;

public class ResumesRepository : GenericRepository<Resume>, IResumesRepository
{
    public ResumesRepository(MainDbContext dbLocalContext) : base(dbLocalContext)
    {
    }

    public async Task UnpinAllByLocaleAsync(Locale locale)
    {
        await LocalSet
            .Where(x => x.PinnedToLocale == locale)
            .ExecuteUpdateAsync(resume => 
                resume.SetProperty(x => x.PinnedToLocale, (Locale?)null));
    }

    public async Task<Resume?> GetPinnedResumeAsync(Locale locale)
    {
        return await LocalSet
            .FirstOrDefaultAsync(x => x.PinnedToLocale == locale);
    }

    public async Task PinResumeAsync(long resumeId, Locale? locale)
    {
        await LocalSet
            .Where(x => x.Id == resumeId)
            .ExecuteUpdateAsync(resume => 
                resume.SetProperty(x => x.PinnedToLocale, locale));
    }
}