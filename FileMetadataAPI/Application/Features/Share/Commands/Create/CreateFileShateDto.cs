namespace FileMetadataAPI.Application.Features.Share.Commands.Create;

public class CreateFileShareDto
{
    public int FileId { get; set; }
    public int UserId { get; set; }
    public string Permission { get; set; }
}
