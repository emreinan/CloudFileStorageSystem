namespace FileStorageAPI.Application.Features.Commands.Download;

public class DownloadFileResult
{
    public Stream Stream { get; set; }       
    public string ContentType { get; set; } 
    public string FileName { get; set; }  
}
