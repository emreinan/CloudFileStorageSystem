using MediatR;
using Microsoft.AspNetCore.StaticFiles;

namespace FileStorageAPI.Application.Features.Commands.Download;

public class DownloadFileCommand : IRequest<DownloadFileResult>
{
    public DownloadFileDto Download { get; set; }

    internal class DownloadFileCommandHandler : IRequestHandler<DownloadFileCommand, DownloadFileResult>
    {
        private const string UploadPath = "wwwroot/Uploads";
        public async Task<DownloadFileResult> Handle(DownloadFileCommand request, CancellationToken cancellationToken)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), UploadPath, request.Download.FileName);
            if (!File.Exists(filePath))
            {
                return null;
            }
            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory, cancellationToken);
            }
            memory.Position = 0;

            var contentType = GetContentType(filePath);
            return new DownloadFileResult
            {
                Stream = memory,
                ContentType = contentType,
                FileName = request.Download.FileName
            };
        }

        private static string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(path, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}

