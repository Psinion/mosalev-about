namespace MOS.Application.DTOs.Errors;

public class ErrorResponseDto
{
    public string Message { get; set; }

    public ErrorResponseDto(string message)
    {
        Message = message;
    }
    
    public static implicit operator ErrorResponseDto(string message)
        => new(message);
}