using MOS.Application.Data;

namespace MOS.WebApi.Factories;

public class HandlerFactory : IHandlerFactory
{
    private readonly IServiceProvider provider;

    public HandlerFactory(IServiceProvider provider)
    {
        this.provider = provider;
    }

    public T GetHandler<T>() where T : class
    {
        return provider.GetRequiredService<T>();
    }
}