using MOS.Application.SQRS.Base;

namespace MOS.Application.SQRS;

public interface IHandlerFactory
{
    IRequestHandler<TCommand, TResult> GetHandler<TCommand, TResult>();
}