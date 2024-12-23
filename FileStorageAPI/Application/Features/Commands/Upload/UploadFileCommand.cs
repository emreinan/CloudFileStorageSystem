﻿using FileStorageAPI.Application.Features.Rules;
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
            var userId = fileStorageBusinessRules.GetUserIdClaim();
            var file = request.Upload.File;

            if (!Directory.Exists(UploadPath))
            {
                Directory.CreateDirectory(UploadPath);
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), UploadPath, file.FileName);
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

