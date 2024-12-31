using FileMetadataAPI.Application.Features.Share.Rules;
using FileMetadataAPI.Domain.Enums;
using FileMetadataAPI.Infrastructure.Context;
using MediatR;

namespace FileMetadataAPI.Application.Features.Share.Commands.Update;

public record UpdateFileShareCommand(int Id, string Permission) : IRequest<Unit>;

public class UpdateFileShareCommandHandler(FileMetaDataDbContext dbContext,FileShareBusinessRules fileShareBusinessRules) : IRequestHandler<UpdateFileShareCommand, Unit>
{
    public async Task<Unit> Handle(UpdateFileShareCommand request, CancellationToken cancellationToken)
    {
        var fileShare = await dbContext.FileShares.FindAsync(request.Id);
        fileShareBusinessRules.FileShareIsNull(fileShare);

        fileShare.PermissionLevel = Enum.Parse<PermissionLevel>(request.Permission, true);
        dbContext.FileShares.Update(fileShare);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
