namespace FileMetadataAPI.Application.Features.Queries.GetList
{
    public class GetListFileQueryDto
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Permission { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
