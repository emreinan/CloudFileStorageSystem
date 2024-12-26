using FileMetadataAPI.Application.Features.FileMetadata.Rules;
using FileMetadataAPI.Domain.Enums;
using FileMetadataAPI.Infrastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace FileMetadataAPI.Application.Features.FileMetadata.Commands.Update;

public class UpdateFileMetadataCommand : IRequest
{
    public int Id { get; set; }
    public UpdateFileMetadataRequest Request { get; set; }

    class UpdateFileMetadataCommandHandler(FileMetaDataDbContext fileMetaDataDbContext, FileBusinessRules fileBusinessRules) : IRequestHandler<UpdateFileMetadataCommand>
    {

        async Task IRequestHandler<UpdateFileMetadataCommand>.Handle(UpdateFileMetadataCommand request, CancellationToken cancellationToken)
        {
            var file = await fileMetaDataDbContext.Files.Include(f => f.FileShares).FirstOrDefaultAsync(f => f.Id == request.Id);

            fileBusinessRules.FileIsExists(file);

            file!.Description = request.Request.Description;
            foreach (var fileShare in file.FileShares)
            {
                fileShare.Permission = Enum.Parse<Permission>(request.Request.Permission, true);
            }

            await fileMetaDataDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
