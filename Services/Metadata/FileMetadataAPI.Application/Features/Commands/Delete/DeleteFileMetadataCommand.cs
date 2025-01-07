using FileMetadataAPI.Application.Features.Rules;
using FileMetadataAPI.Persistence.Context;
using MediatR;

namespace FileMetadataAPI.Application.Features.Commands.Delete;

public class DeleteFileMetadataCommand : IRequest
{
    public int Id { get; set; }

    class DeleteFileMetadataCommandHandler(
        FileMetaDataDbContext fileDbContext,
        FileMetadataBusinessRules fileBusinessRules
        ) : IRequestHandler<DeleteFileMetadataCommand>
    {
        async Task IRequestHandler<DeleteFileMetadataCommand>.Handle(DeleteFileMetadataCommand request, CancellationToken cancellationToken)
        {
            var fileMetadata = await fileDbContext.Files.FindAsync(request.Id);
            fileBusinessRules.FileIsExists(fileMetadata);
            fileDbContext.Files.Remove(fileMetadata!);
            await fileDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

