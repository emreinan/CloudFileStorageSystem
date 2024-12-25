using FileStorageAPI.Application.Features.Rules;
using MediatR;
using System.Security.Claims;

namespace FileStorageAPI.Application.Features.Commands.Upload;

public class UploadFileCommand : IRequest<FileStorageResult>
{
    public UploadFileDto Upload { get; set; }

    internal class UploadFileCommandHandler(
        FileStorageBusinessRules fileStorageBusinessRules,
        IHttpClientFactory httpClientFactory
        ) : IRequestHandler<UploadFileCommand, FileStorageResult>
    {
        private const string UploadPath = "wwwroot/uploads";
        private readonly HttpClient httpClient = httpClientFactory.CreateClient("FileMetadataApiClient");
        public async Task<FileStorageResult> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var userId = fileStorageBusinessRules.GetUserIdClaim();
            var file = request.Upload.File;

            fileStorageBusinessRules.UploadPathIsExists(UploadPath);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), UploadPath, file.FileName);
            fileStorageBusinessRules.FileIsExists(filePath);

            var fileMetadata = new AddFileMetadataRequest
            {
                Name = file.FileName,
                Description = request.Upload.Description,
                UploadDate = DateTime.UtcNow,
                OwnerId = userId,
                Permission = request.Upload.Permission

            };

            var response = await httpClient.PostAsJsonAsync("api/FileMetadata", fileMetadata, cancellationToken);
            response.EnsureSuccessStatusCode();


            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream, cancellationToken);
            }

            return new FileStorageResult
            {
                Name = request.Upload.File.FileName,
                Description = request.Upload.Description,
                UploadDate = DateTime.Now,
                OwnerId = userId
            };
        }
    }
}

