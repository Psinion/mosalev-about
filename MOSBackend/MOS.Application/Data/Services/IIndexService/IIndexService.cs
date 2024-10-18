using MOS.Application.DTOs.Users.Requests;
using MOS.Application.DTOs.Users.Responses;
using MOS.Application.OperationResults;

namespace MOS.Application.Data.Services.IIndexService;

public interface IIndexService : IDisposable
{
    Task<OperationResult<IndexContentResponseDto>> GetActualIndexContentAsync();
}