namespace FileMetadataAPI.Domain.Entities;

public class FileShare
{
    public int FileId { get; set; }
    public int UserId { get; set; }
    public string Permission { get; set; }

    public File File { get; set; }
}
