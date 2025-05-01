namespace MOS.Domain.Entities;

public interface IAuditableEntity<TKey> : IEntity<TKey>
    where TKey : struct
{
    int CreatedBy { get; set; }
    DateTime CreatedAt { get; set; }
    
    int? UpdatedBy { get; set; }
    DateTime? UpdatedAt { get; set; }
    
    int? DeletedBy { get; set; }
    DateTime? DeletedAt { get; set; }
    
    bool IsDeleted { get; set; }
}