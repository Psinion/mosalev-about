namespace MOS.Application.Data;

public interface IQueryHandler<in TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    Task<TResult> Handle(TQuery request, CancellationToken cancellationToken = default);
}