using MOS.Application.Data.Repositories;
using MOS.Application.Data.Repositories.Projects;
using MOS.Application.DTOs.Projects.Responses;
using MOS.Application.Mappings.Projects;
using MOS.Application.Modules.Projects.Commands;
using MOS.Application.Modules.Projects.Commands.Handlers;
using MOS.Application.OperationResults;
using MOS.Domain.Entities.Projects;

namespace MOS.Data.EF.Access.Handlers.Projects;

public class CreateProjectHandler : ICreateProjectHandler
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IProjectsRepository projectsRepository;
    
    public CreateProjectHandler(IUnitOfWork unitOfWork, IProjectsRepository projectsRepository)
    {
        this.unitOfWork = unitOfWork;
        this.projectsRepository = projectsRepository;
    }

    public async Task<OperationResult<ProjectResponseDto>> Handle(CreateProjectCommand request, CancellationToken cancellationToken = default)
    {
        var project = new Project()
        {
            Title = request.Title,
            Description = request.Description
        };

        var newProject = await projectsRepository.CreateAsync(project, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return newProject.ToDto();
    }
}