using Microsoft.EntityFrameworkCore;
using MOS.Application.Data.Repositories.Resumes;
using MOS.Application.Data.Services.Users;
using MOS.Data.EF.Access.Contexts;
using MOS.Domain.Entities.Resumes;
using MOS.Domain.Enums;

namespace MOS.Data.EF.Access.Repositories.Resumes;

public class ResumesRepository : LoggedGenericRepository<Resume>, IResumesRepository
{
    public ResumesRepository(MainDbContext dbLocalContext, ICredentialsService credentialsService) 
        : base(dbLocalContext, credentialsService)
    {
    }

    public async Task<Resume?> GetByIdWithRelationsAsync(long id, CancellationToken cancellationToken = default)
    {
        return await LocalContext.Resumes
            .Include(x => x.Skills)
            .Include(x => x.CompanyEntries)
                .ThenInclude(companyEntry => companyEntry.ResumePosts)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
            ;
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
            .Include(x => x.Skills)
            .FirstOrDefaultAsync(x => x.PinnedToLocale == locale)
            ;
    }

    public async Task PinResumeAsync(long resumeId, Locale? locale)
    {
        await LocalSet
            .Where(x => x.Id == resumeId)
            .ExecuteUpdateAsync(resume => 
                resume.SetProperty(x => x.PinnedToLocale, locale));
    }
}