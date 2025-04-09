using Microsoft.AspNetCore.Mvc;
using Moq;
using MOS.Application.Data.Repositories.Users;
using MOS.Application.DTOs.Users.Requests;
using MOS.Application.DTOs.Users.Responses;
using MOS.Application.OperationResults;
using MOS.Application.OperationResults.Enums;
using MOS.Domain.Entities.Users;
using MOS.Identity.Helpers;
using MOS.Identity.Services;

namespace MOS.UnitTestsX.Services;

public class UsersServiceTests
    {
        private readonly Mock<IUsersRepository> mockUsersRepository;
        private readonly AuthSettings authSettings;
        private readonly UsersService usersService;

        public UsersServiceTests()
        {
            authSettings = new AuthSettings()
            {
                AuthSalt = "Salt", 
                JwtSecretKey = "super-secret-key-with-minimum-128-bits-length-1234567890", 
                TokenDurationMinutes = 60
            };
            mockUsersRepository = new Mock<IUsersRepository>();
            usersService = new UsersService(authSettings, mockUsersRepository.Object);
        }
        
        [Fact]
        public async Task AuthenticateAsync_ValidCredentials_ReturnsToken()
        {
            var request = new AuthenticateRequestDto 
            { 
                UserName = "test", 
                Password = "password" 
            };

            var expectedUser = new User
            {
                Id = 1,
                UserName = "test",
            };

            mockUsersRepository
                .Setup(r => r.GetByCredentialsAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(expectedUser);
            
            var result = await usersService.AuthenticateAsync(request);
            
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value?.Token);
            Assert.Equal(expectedUser.Id, result.Value.User.Id);
        }

        [Fact]
        public async Task AuthenticateAsync_InvalidCredentials_Returns401()
        {
            var request = new AuthenticateRequestDto 
            { 
                UserName = "wrong", 
                Password = "wrong" 
            };

            mockUsersRepository
                .Setup(r => r.GetByCredentialsAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((User?)null);
            
            var result = await usersService.AuthenticateAsync(request);
            
            Assert.True(!result.IsSuccess);
            Assert.Equal(ErrorType.Unauthorized, result.Error.ErrorType);
        }
    }