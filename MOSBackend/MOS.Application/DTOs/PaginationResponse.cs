namespace MOS.Application.DTOs;

public record PaginationResponse<T>
{
    public List<T> Items { get; init; } = new();
    public int TotalCount { get; init; }
}