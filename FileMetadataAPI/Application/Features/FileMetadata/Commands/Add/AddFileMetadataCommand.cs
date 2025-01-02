using FileMetadataAPI.Application.Features.FileMetadata.Rules;
using FileMetadataAPI.Domain.Enums;
using FileMetadataAPI.Infrastructure.Context;
using MediatR;
using File = FileMetadataAPI.Domain.Entities.File;
using FileShare = FileMetadataAPI.Domain.Entities.FileShare;

namespace FileMetadataAPI.Application.Features.FileMetadata.Commands.Add;

public class AddFileMetadataCommand : IRequest<AddFileMetadataResponse>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string SharingType { get; set; }
    public string? PermissionLevel { get; set; }
    public List<int>? SharedWithUserIds { get; set; }

    class AddFileMetadataCommandHandler(
        FileMetaDataDbContext dbContext,
        FileMetadataBusinessRules fileMetadataBusinessRules
        ) : IRequestHandler<AddFileMetadataCommand, AddFileMetadataResponse>
    {
        public async Task<AddFileMetadataResponse> Handle(AddFileMetadataCommand request, CancellationToken cancellationToken)
        {
            var sharingType = FileMetadataBusinessRules.ConvertToEnum<SharingType>(request.SharingType);

            var entity = new File
            {
                Name = request.Name,
                Description = request.Description,
                UploadDate = DateTime.UtcNow,
                SharingType = sharingType,
                OwnerId = sharingType == Domain.Enums.SharingType.Public ? null : fileMetadataBusinessRules.GetUserIdClaim() //*
            };

            if (sharingType == Domain.Enums.SharingType.SharedWithSpecificUsers && request.SharedWithUserIds is { Count: > 0 })
            {
                var permissionLevel = FileMetadataBusinessRules.ConvertToEnum<PermissionLevel>(request.PermissionLevel!);
                foreach (var targetUserId in request.SharedWithUserIds)
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
