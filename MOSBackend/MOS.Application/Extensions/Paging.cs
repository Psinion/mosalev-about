namespace MOS.Application.Extensions;

public static class Paging
{
    public static IQueryable<T> Page<T>(this IQueryable<T> query, int limit, int offset)
    {
        if (limit == 0)
        {
            return query;
        }
        
        query = query.Skip(offset);
        
        return query.Take(limit);
    }
}