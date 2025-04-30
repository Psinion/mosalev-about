namespace MOS.Application.Data;

public interface IRequestHandler<TCommand, TResult>
{
    public Task<TResult> Handle(TCommand request, CancellationToken cancellationToken = default);
}