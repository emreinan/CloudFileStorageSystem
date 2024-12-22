namespace FileStorageAPI.Application.Features.Commands.Upload;

public class UploadFileDto
{
    public IFormFile File { get; set; }
    public string Description { get; set; }
}
