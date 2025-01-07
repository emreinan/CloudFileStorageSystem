using FileStorageAPI.Application.Features.Rules;
using MediatR;
using System.Security.Claims;

namespace FileStorageAPI.Application.Features.Commands.Upload;

public class UploadFileCommand : IRequest<FileStorageResult>
{
    public UploadFileDto Upload { get; set; }

    internal class UploadFileCommandHandler(
        FileStorageBusinessRules fileStorageBusinessRules
        ) : IRequestHandler<UploadFileCommand, FileStorageResult>
    {
        private const string UploadPath = "wwwroot/uploads";
        public async Task<FileStorageResult> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var file = request.Upload.File;

            fileStorageBusinessRules.FileSizeIsValid(file);
            fileStorageBusinessRules.UploadPathIsExists(UploadPath);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), UploadPath, file.FileName);
            fileStorageBusinessRules.FileIsExists(filePath);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream, cancellationToken);
            }

            return new FileStorageResult
            {
                Name = file.FileName,
                Description = request.Upload.Description,
            };
        }
    }
}

