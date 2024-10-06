using MOS.Domain.Entities.Users;

namespace MOS.Application.DTOs.Users.Responses;

public class UserResponseDto {
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public string UserName { get; set; }
    
    public UserResponseDto(User user)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Patronymic = user.Patronymic;
        UserName = user.UserName;
    }
}