namespace FileMetadataAPI.Application.Features.Commands.Add
{
    public class AddFileMetadataRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string SharingType { get; set; }
        public string? PermissionLevel { get; set; }
        public List<int>? SharedWithUserIds { get; set; }
    }
}
