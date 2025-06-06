﻿using Microsoft.EntityFrameworkCore;
using MOS.Application.Data.Repositories.Resumes;
using MOS.Application.Data.Services.Users;
using MOS.Data.EF.Access.Contexts;
using MOS.Domain.Entities.Resumes;
using MOS.Domain.Enums;

namespace MOS.Data.EF.Access.Repositories.Resumes;

public class ResumesRepository : AuditableRepository<Resume, int>, IResumesRepository
{
    public ResumesRepository(MainDbContext dbLocalContext, ICredentialsService credentialsService) 
        : base(dbLocalContext, credentialsService)
    {
    }

    public async Task<Resume?> GetByIdWithRelationsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await localContext.Resumes
            .Include(x => x.Skills)
            .Include(x => x.CompanyEntries)
                .ThenInclude(companyEntry => companyEntry.ResumePosts)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
            ;
    }

    public async Task UnpinAllByLocaleAsync(Locale locale)
    {
        await localSet
            .Where(x => x.PinnedToLocale == locale)
            .ExecuteUpdateAsync(resume => 
                resume.SetProperty(x => x.PinnedToLocale, (Locale?)null));
    }

    public async Task<Resume?> GetPinnedResumeAsync(Locale locale)
    {
        return await localSet
            .Include(x => x.Skills)
            .FirstOrDefaultAsync(x => x.PinnedToLocale == locale)
            ;
    }

    public async Task PinResumeAsync(int resumeId, Locale? locale)
    {
        await localSet
            .Where(x => x.Id == resumeId)
            .ExecuteUpdateAsync(resume => 
                resume.SetProperty(x => x.PinnedToLocale, locale));
    }
}