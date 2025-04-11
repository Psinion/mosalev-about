namespace MOS.Domain.Entities;

public class Entity<TKey> : IEntity<TKey> where TKey : struct
{
    public TKey Id { get; set; }
}