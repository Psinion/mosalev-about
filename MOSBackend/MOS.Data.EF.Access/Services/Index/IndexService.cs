using MOS.Application.Data.Repositories.Index;
using MOS.Application.Data.Services.IIndexService;
using MOS.Application.DTOs.Users.Responses;
using MOS.Application.OperationResults;

namespace MOS.Data.EF.Access.Services.Index;

public class IndexService : IIndexService
{
    private readonly IIndexContentsRepository indexContentsRepository;
    
    public IndexService(IIndexContentsRepository indexContentsRepository)
    {
        this.indexContentsRepository = indexContentsRepository;
    }

    public void Dispose()
    {
        indexContentsRepository.Dispose();
    }

    public async Task<OperationResult<IndexContentResponseDto>> GetActualIndexContentAsync()
    {
        var indexContent = await indexContentsRepository.GetActualIndexContentAsync();
        if (indexContent == null)
        {
            return OperationError.NotFound("Not found", "Not found");
        }

        return new IndexContentResponseDto(indexContent.Content);
    }
}