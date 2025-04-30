using MOS.Application.Data;
using MOS.Application.DTOs.Users.Responses;
using MOS.Application.Modules.Users;
using MOS.Application.Modules.Users.Queries;
using MOS.Application.Modules.Users.Queries.Handlers;
using MOS.Application.OperationResults;
using MOS.Application.OperationResults.Enums;
using MOS.Data.EF.Access.Repositories.Users;
using MOS.Domain.Entities.Users;
using MOS.Identity.Handlers;
using MOS.Identity.Helpers;
using MOS.UnitTestsX.Data;

namespace MOS.UnitTestsX.Services;

public class UsersServiceTests
    {
        private readonly AuthSettings authSettings;
        private readonly IAuthenticateHandler authenticateHandler;

        public UsersServiceTests()
        {
            authSettings = new AuthSettings()
            {
                AuthSalt = "Salt", 
                JwtSecretKey = "super-secret-key-with-minimum-128-bits-length-1234567890", 
                TokenDurationMinutes = 60
            };
            
            var context = TestDataHelper.MainDbContext;
            
            var usersRepository = new UsersRepository(context);
            authenticateHandler = new AuthenticateHandler(authSettings, usersRepository);
        }
        
        [Fact]
        public async Task AuthenticateAsync_ValidCredentials_ReturnsToken()
        {
            var request = new AuthenticateQuery
            { 
                UserName = "John Doe", 
                Password = "password" 
            };

            var expectedUser = new User
            {
                Id = 1,
                UserName = "John Doe",
            };
            
            var result = await authenticateHandler.Handle(request);
            
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value?.Token);
            Assert.Equal(expectedUser.Id, result.Value.User.Id);
        }

        [Fact]
        public async Task AuthenticateAsync_InvalidCredentials_Returns401()
        {
            var request = new AuthenticateQuery 
            { 
                UserName = "wrong", 
                Password = "wrong" 
            };
            
            var result = await authenticateHandler.Handle(request);
            
            Assert.True(!result.IsSuccess);
            Assert.Equal(ErrorType.Unauthorized, result.Error.ErrorType);
        }
    }