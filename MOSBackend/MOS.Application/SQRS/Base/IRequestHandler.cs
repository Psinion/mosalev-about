namespace MOS.Application.SQRS.Base;

public interface IRequestHandler<TCommand, TResult>
{
    public Task<TResult> Handle(TCommand request, CancellationToken cancellationToken = default);
}