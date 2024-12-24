namespace FileMetadataAPI.Application.Features.Commands.Add
{
    public class AddFileMetadataResponse
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime UploadDate { get; set; }

    }
}
