namespace MOS.Application.Data;

public interface IHandlerFactory
{
    T GetHandler<T>() where T : class; 
}