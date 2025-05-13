using MOS.Application.Data.Repositories;
using MOS.Application.Data.Repositories.Files;
using MOS.Application.Modules.Files.Queries;
using MOS.Application.Modules.Files.Queries.Handlers;
using MOS.Application.OperationResults;

namespace MOS.Data.EF.Access.Handlers.Files;

public class DeleteFileHandler : IDeleteFileHandler
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IUploadedFilesRepository uploadedFilesRepository;
    
    public DeleteFileHandler(IUnitOfWork unitOfWork, IUploadedFilesRepository uploadedFilesRepository)
    {
        this.unitOfWork = unitOfWork;
        this.uploadedFilesRepository = uploadedFilesRepository;
    }

    public async Task<OperationResult<string?>> Handle(DeleteFileCommand request, CancellationToken cancellationToken = default)
    {
        var file = await uploadedFilesRepository.GetByIdAsync(request.Id, cancellationToken);
        if (file == null)
        {
            return OperationError.NotFound();
        }

        await uploadedFilesRepository.DeleteAsync(file.Id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return file.Url;
    }
}