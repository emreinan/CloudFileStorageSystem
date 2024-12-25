using CloudFileStorageMVC.Models.Enums;

namespace CloudFileStorageMVC.Models;

public class FileViewModel
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    public Permission Permission { get; set; }
}