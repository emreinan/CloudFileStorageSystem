namespace CloudFileStorageMVC.Models;

public class CreateFileShareRequestModel
{
    public int FileId { get; set; }
    public int UserId { get; set; }
    public string Permission { get; set; }
}
