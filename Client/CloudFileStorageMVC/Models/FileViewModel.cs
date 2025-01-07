
namespace CloudFileStorageMVC.Models;

public class FileViewModel
{
    public int Id { get; set; }
    public int OwnerId { get; set; }
    public string OwnerName { get; set; } = string.Empty;
    public string Description { get; set; }
    public string Name { get; set; }
    public string PermissionLevel { get; set; }
    public string SharingType { get; set; }
    public DateTime UploadDate { get; set; }
}