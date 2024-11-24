using MOS.Domain.Entities.Users;

namespace MOS.Domain.Entities;

public class LoggedEntity : Entity, ILoggedEntity
{
    public User? UserCreate { get; set; }
    
    public DateTime? DateCreate { get; set; }
    
    public User? UserUpdate { get; set; }
    
    public DateTime? DateUpdate { get; set; }
    
    public User? UserDelete { get; set; }
    
    public DateTime? DateDelete { get; set; }
}