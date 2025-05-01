using MOS.Application.Data.Repositories.Resumes;
using MOS.Data.EF.Access.Contexts;
using MOS.Domain.Entities.Resumes;

namespace MOS.Data.EF.Access.Repositories.Resumes;

public class ResumeSkillsRepository : GenericRepository<ResumeSkill, int>, IResumeSkillsRepository
{
    public ResumeSkillsRepository(MainDbContext dbLocalContext) : base(dbLocalContext)
    {
    }
}