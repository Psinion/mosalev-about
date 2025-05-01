using MOS.Application.Data;
using MOS.Application.DTOs.Projects.Responses;
using MOS.Application.OperationResults;

namespace MOS.Application.Modules.Projects.Commands.Handlers;

public interface IUpdateArticleHandler : ICommandHandler<UpdateArticleCommand, OperationResult<ArticleDto>>
{
}