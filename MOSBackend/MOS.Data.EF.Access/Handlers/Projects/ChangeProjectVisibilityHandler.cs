using MOS.Application.Data.Repositories;
using MOS.Application.Data.Repositories.Projects;
using MOS.Application.DTOs.Projects.Responses;
using MOS.Application.Mappings.Projects;
using MOS.Application.Modules.Projects.Commands;
using MOS.Application.Modules.Projects.Commands.Handlers;
using MOS.Application.OperationResults;

namespace MOS.Data.EF.Access.Handlers.Projects;

public class ChangeProjectVisibilityHandler : IChangeProjectVisibilityHandler
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IProjectsRepository projectsRepository;

    public ChangeProjectVisibilityHandler(IUnitOfWork unitOfWork, IProjectsRepository projectsRepository)
    {
        this.unitOfWork = unitOfWork;
        this.projectsRepository = projectsRepository;
    }
    
    public async Task<OperationResult<bool>> Handle(ChangeProjectVisibilityCommand request, CancellationToken cancellationToken = default)
    {
        var project = await projectsRepository.GetByIdAsync(request.Id, cancellationToken);
        if (project == null)
        {
            return OperationError.NotFound();
        }

        project.Visible = request.Visible;

        await projectsRepository.UpdateAsync(project, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}