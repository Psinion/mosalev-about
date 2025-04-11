namespace MOS.Domain.Entities;

public interface IAuditableEntity<TKey> : IEntity<TKey>
    where TKey : struct
{
    long CreatedBy { get; set; }
    DateTime CreatedAt { get; set; }
    
    long? UpdatedBy { get; set; }
    DateTime? UpdatedAt { get; set; }
    
    long? DeletedBy { get; set; }
    DateTime? DeletedAt { get; set; }
    
    bool IsDeleted { get; set; }
}