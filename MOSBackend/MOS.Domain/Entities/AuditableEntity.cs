using System.ComponentModel.DataAnnotations.Schema;
using MOS.Domain.Entities.Users;

namespace MOS.Domain.Entities;

public class AuditableEntity<TKey> : Entity<TKey>, IAuditableEntity<TKey> 
    where TKey : struct
{
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    [NotMapped]
    public virtual User? Creator { get; set; }

    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    [NotMapped]
    public virtual User? Updater { get; set; }
    
    public int? DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    [NotMapped]
    public virtual User? Deleter { get; set; }
    
    public bool IsDeleted { get; set; }
}