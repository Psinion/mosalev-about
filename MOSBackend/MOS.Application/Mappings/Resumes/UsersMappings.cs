using MOS.Application.DTOs.Resumes.Responses;
using MOS.Application.DTOs.Users.Responses;
using MOS.Domain.Entities.Resumes;
using MOS.Domain.Entities.Users;

namespace MOS.Application.Mappings.Resumes;


public static class UsersMappings
{
    public static UserResponseDto ToDto(this User entity)
    {
        return new UserResponseDto()
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Patronymic = entity.Patronymic,
            UserName = entity.UserName
        };
    }
}