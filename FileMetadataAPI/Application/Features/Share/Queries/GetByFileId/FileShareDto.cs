namespace FileMetadataAPI.Application.Features.Share.Queries.GetByFileId;

public class FileShareDto
{
    public int Id { get; set; }
    public int FileId { get; set; }
    public int UserId { get; set; }
    public string Permission { get; set; }
}
