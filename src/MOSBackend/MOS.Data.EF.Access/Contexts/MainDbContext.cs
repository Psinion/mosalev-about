using Microsoft.EntityFrameworkCore;
using MOS.Domain.Entities.Users;

namespace MOS.Data.EF.Access.Contexts;

public class MainDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    public MainDbContext(DbContextOptions<MainDbContext> options)
        : base(options)
    {
    }
}