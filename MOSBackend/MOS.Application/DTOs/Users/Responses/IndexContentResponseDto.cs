namespace MOS.Application.DTOs.Users.Responses;

public class IndexContentResponseDto
{
    public string Content { get; set; }
    
    public IndexContentResponseDto(string content)
    {
        Content = content;
    }
}