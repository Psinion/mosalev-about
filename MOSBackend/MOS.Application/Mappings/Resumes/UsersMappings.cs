using MOS.Application.DTOs.Users.Responses;
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
    
    public static List<UserResponseDto> ToDtoList(this IList<User> entities)
    {
        var dtoList = new List<UserResponseDto>(entities.Count);
        foreach (var entity in entities)
        {
            dtoList.Add(entity.ToDto());
        }
        return dtoList;
    }
}