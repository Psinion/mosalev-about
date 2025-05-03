using MOS.Application.Data.Repositories;
using MOS.Application.Data.Repositories.Files;
using MOS.Application.DTOs.Files.Responses;
using MOS.Application.Mappings.Files;
using MOS.Application.Modules.Files.Commands;
using MOS.Application.Modules.Files.Commands.Handlers;
using MOS.Application.OperationResults;

namespace MOS.Data.EF.Access.Handlers.Files;

public class CreateFileHandler : ICreateFileHandler
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IUploadedFilesRepository uploadedFilesRepository;
    
    public CreateFileHandler(
        IUnitOfWork unitOfWork, 
        IUploadedFilesRepository uploadedFilesRepository
    )
    {
        this.unitOfWork = unitOfWork;
        this.uploadedFilesRepository = uploadedFilesRepository;
    }
    
    public async Task<OperationResult<UploadedFileDto>> Handle(CreateFileCommand request, CancellationToken cancellationToken = default)
    {
        var uploadedFile = request.UploadedFile;
        uploadedFile.UploadDate = DateTime.UtcNow;

        var newUploadedFile = await uploadedFilesRepository.CreateAsync(uploadedFile, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return newUploadedFile.ToDto();
    }
}