using Microsoft.EntityFrameworkCore;
using MOS.Data.EF.Access.Contexts;
using MOS.Domain.Entities.Users;

namespace MOS.UnitTestsX.Data;

public static class TestDataHelper
{
    private static MainDbContext? mainDbContext;
    public static MainDbContext MainDbContext {
        get
        {
            if (mainDbContext != null)
            {
                return mainDbContext;
            }
        
            var options = new DbContextOptionsBuilder<MainDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new MainDbContext(options);

            var users = new User[]
            {
                new()
                {
                    Id = 1,
                    UserName = "John Doe",
                    Password = "3ae69effc40010a1edd07ff48de3503559ecc2ee8fcb00cc8f3c01e3612dcf2d", // password
                },
                new()
                {
                    Id = 2,
                    UserName = "Jane Doe",
                    Password = "9dd838c068557de7525bc104312b4d4cd92e72e4746ca43e370db9905d16b4be", // Jane Doe
                }
            };
        
            context.Users.AddRange(users);
            context.SaveChanges();
 
            return context;
        }
    }
}