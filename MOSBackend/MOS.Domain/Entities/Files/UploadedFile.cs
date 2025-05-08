using System.ComponentModel.DataAnnotations;
using MOS.Domain.Enums;

namespace MOS.Domain.Entities.Files;

public class UploadedFile : Entity<int>
{
    [Required]
    [MaxLength(255)]
    public string OriginalName { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(255)]
    public string StoredName { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(500)]
    public string Url { get; set; } = string.Empty;
    
    public long Size { get; set; }
    public DateTime UploadDate { get; set; } = DateTime.UtcNow;

    [Required] public string Type { get; set; } = string.Empty;
    [Required] public FileKind Kind { get; set; } = FileKind.Other;
}