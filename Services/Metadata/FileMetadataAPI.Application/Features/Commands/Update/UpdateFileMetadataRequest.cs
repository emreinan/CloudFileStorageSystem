namespace FileMetadataAPI.Application.Features.Commands.Update;

public class UpdateFileMetadataRequest
{
    public string Description { get; set; }
    public string SharingType { get; set; }
    public string? PermissionLevel { get; set; }
    public List<int>? SharedWithUserIds { get; set; }
}
