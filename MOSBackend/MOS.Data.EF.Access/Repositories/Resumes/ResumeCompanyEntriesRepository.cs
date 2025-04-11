using Microsoft.EntityFrameworkCore;
using MOS.Application.Data.Repositories.Resumes;
using MOS.Application.Data.Services.Users;
using MOS.Data.EF.Access.Contexts;
using MOS.Domain.Entities.Resumes;
using MOS.Domain.Enums;

namespace MOS.Data.EF.Access.Repositories.Resumes;

public class ResumeCompanyEntriesRepository : GenericRepository<ResumeCompanyEntry, long>, IResumeCompanyEntriesRepository
{
    public ResumeCompanyEntriesRepository(MainDbContext dbLocalContext) : base(dbLocalContext)
    {
    }
}