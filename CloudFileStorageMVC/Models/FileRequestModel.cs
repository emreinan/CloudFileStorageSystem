using System.ComponentModel.DataAnnotations;

namespace CloudFileStorageMVC.Models;

public class FileRequestModel
{
    [Required, MaxLength(500)]
    public string Description { get; set; }
    [Required]
    public string SharingType { get; set; }
    public string PermissionLevel { get; set; } = string.Empty;
    public List<int>? SharedWithUserIds { get; set; }
}
