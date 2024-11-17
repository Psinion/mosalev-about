using MOS.Application.Data.Repositories.Resumes;
using MOS.Data.EF.Access.Contexts;
using MOS.Domain.Entities.Resumes;

namespace MOS.Data.EF.Access.Repositories.Resumes;

public class ResumesRepository : GenericRepository<Resume>, IResumesRepository
{
    public ResumesRepository(MainDbContext dbLocalContext) : base(dbLocalContext)
    {
    }
}