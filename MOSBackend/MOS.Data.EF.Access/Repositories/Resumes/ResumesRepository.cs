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

    public Task UnpinAllByLocaleAsync(Locale locale)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> HasPinnedResumeAsync(Locale locale)
    {
        return await LocalSet
            .FirstOrDefaultAsync(x => x.PinnedToLocale == locale) != null;
    }

    public async Task PinResumeAsync(long resumeId, Locale? locale)
    {
        await LocalSet
            .Where(x => x.Id == resumeId)
            .ExecuteUpdateAsync(resume => 
                resume.SetProperty(x => x.PinnedToLocale, locale));
    }
}