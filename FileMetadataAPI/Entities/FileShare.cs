namespace FileMetadataAPI.Entities;

public class FileShare
{
    public int FileId { get; set; }
    public int UserId { get; set; }
    public string Permission { get; set; }
}
