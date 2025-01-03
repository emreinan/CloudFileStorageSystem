namespace FileMetadataAPI.Application.Features.FileMetadata.Queries.GetList
{
    public class GetListFileQueryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SharingType { get; set; }
        public string PermissionLevel { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
