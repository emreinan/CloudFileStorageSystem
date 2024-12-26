using FileMetadataAPI.Application.Features.Share.Rules;
using FileMetadataAPI.Infrastructure.Context;
using MediatR;

namespace FileMetadataAPI.Application.Features.Share.Commands.Delete;

public record DeleteFileShareCommand(int Id) : IRequest<Unit>;

public class DeleteFileShareCommandHandler(FileMetaDataDbContext dbContext,FileShareBusinessRules fileShareBusinessRules) : IRequestHandler<DeleteFileShareCommand, Unit>
{
    public async Task<Unit> Handle(DeleteFileShareCommand request, CancellationToken cancellationToken)
    {
        var fileShare = await dbContext.FileShares.FindAsync(request.Id);
        fileShareBusinessRules.FileShareIsNull(fileShare);

        dbContext.FileShares.Remove(fileShare);
        await dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}

