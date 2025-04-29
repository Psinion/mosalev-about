using MOS.Application.SQRS;
using MOS.Application.SQRS.Base;

namespace MOS.WebApi.SQRS;

public class HandlerFactory : IHandlerFactory
{
    private readonly IServiceProvider provider;

    public HandlerFactory(IServiceProvider provider)
    {
        this.provider = provider;
    }

    public IRequestHandler<TCommand, TResult> GetHandler<TCommand, TResult>()
    {
        return provider.GetRequiredService<IRequestHandler<TCommand, TResult>>();
    }
}