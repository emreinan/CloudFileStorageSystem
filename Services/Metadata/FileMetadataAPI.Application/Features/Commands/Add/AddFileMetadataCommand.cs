using FileMetadataAPI.Application.Features.Rules;
using FileMetadataAPI.Domain.Enums;
using FileMetadataAPI.Persistence.Context;
using MediatR;
using File = FileMetadataAPI.Domain.Entities.File;
using FileShare = FileMetadataAPI.Domain.Entities.FileShare;

namespace FileMetadataAPI.Application.Features.Commands.Add;

public class AddFileMetadataCommand : IRequest<AddFileMetadataResponse>
{
    public AddFileMetadataRequest Request { get; set; }

    class AddFileMetadataCommandHandler(
        FileMetaDataDbContext dbContext,
        FileMetadataBusinessRules fileMetadataBusinessRules
        ) : IRequestHandler<AddFileMetadataCommand, AddFileMetadataResponse>
    {
        public async Task<AddFileMetadataResponse> Handle(AddFileMetadataCommand request, CancellationToken cancellationToken)
        {
            var sharingType = FileMetadataBusinessRules.ConvertToEnum<SharingType>(request.Request.SharingType);
            var userId = fileMetadataBusinessRules.GetUserIdClaim();

            var entity = new File
            {
                Name = request.Request.Name,
                Description = request.Request.Description,
                UploadDate = DateTime.UtcNow,
                SharingType = sharingType,
                OwnerId = userId
            };

            if (sharingType == SharingType.SharedWithSpecificUsers && request.Request.SharedWithUserIds is { Count: > 0 })
            {
                var permissionLevel = FileMetadataBusinessRules.ConvertToEnum<PermissionLevel>(request.Request.PermissionLevel!);
                foreach (var targetUserId in request.Request.SharedWithUserIds)
                {
                    entity.FileShares.Add(new FileShare
                    {
                        UserId = targetUserId,
                        PermissionLevel = permissionLevel
                    });
                }
            }

            await dbContext.Files.AddAsync(entity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new AddFileMetadataResponse { Name = entity.Name, Description = entity.Description };
        }
    }
}
