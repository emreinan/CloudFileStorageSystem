using FileMetadataAPI.Application.Features.Rules;
using FileMetadataAPI.Domain.Enums;
using FileMetadataAPI.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FileShare = FileMetadataAPI.Domain.Entities.FileShare;


namespace FileMetadataAPI.Application.Features.Commands.Update;

public class UpdateFileMetadataCommand : IRequest
{
    public int Id { get; set; }
    public UpdateFileMetadataRequest Request { get; set; }

    class UpdateFileMetadataCommandHandler(FileMetaDataDbContext fileMetaDataDbContext, FileMetadataBusinessRules fileBusinessRules) : IRequestHandler<UpdateFileMetadataCommand>
    {

        async Task IRequestHandler<UpdateFileMetadataCommand>.Handle(UpdateFileMetadataCommand request, CancellationToken cancellationToken)
        {
            var file = await fileMetaDataDbContext.Files.FirstOrDefaultAsync(f => f.Id == request.Id);
            fileBusinessRules.FileIsExists(file);

            var newSharingType = FileMetadataBusinessRules.ConvertToEnum<SharingType>(request.Request.SharingType);
            if (file!.SharingType != newSharingType)
            {
                file.SharingType = newSharingType;

                if (newSharingType == SharingType.Private)
                {
                    file.FileShares.Clear();
                }
                else if (newSharingType == SharingType.SharedWithSpecificUsers)
                {
                    file.FileShares.Clear();

                    if (request.Request.SharedWithUserIds is { Count: > 0 })
                    {
                        var permissionLevel = FileMetadataBusinessRules.ConvertToEnum<PermissionLevel>(request.Request.PermissionLevel!);
                        foreach (var targetUserId in request.Request.SharedWithUserIds)
                        {
                            file.FileShares.Add(new FileShare
                            {
                                UserId = targetUserId,
                                PermissionLevel = permissionLevel
                            });
                        }
                    }
                }
                else if (newSharingType == SharingType.Public)
                {
                    file.FileShares.Clear();
                }
            }
            file.Description = request.Request.Description;

            await fileMetaDataDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
