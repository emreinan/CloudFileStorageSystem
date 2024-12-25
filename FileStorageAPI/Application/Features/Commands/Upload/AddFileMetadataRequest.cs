namespace FileStorageAPI.Application.Features.Commands.Upload;

public class AddFileMetadataRequest
{
    public int OwnerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime UploadDate { get; set; }
    public string Permission { get; set; }
}
