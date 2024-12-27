using AutoMapper;
using FileMetadataAPI.Application.Features.Share.Rules;
using FileMetadataAPI.Domain.Enums;
using FileMetadataAPI.Infrastructure.Context;
using MediatR;
using System.Security.Claims;
using FileShare = FileMetadataAPI.Domain.Entities.FileShare;

namespace FileMetadataAPI.Application.Features.Share.Commands.Create;

public record CreateFileShareCommand(int FileId, int UserId, string Permission) : IRequest<int>;

public class CreateFileShareCommandHandler(
    FileMetaDataDbContext dbContext, 
    IMapper mapper,
    FileShareBusinessRules fileShareBusinessRules,
    IHttpContextAccessor httpContextAccessor
    ) : IRequestHandler<CreateFileShareCommand, int>
{
    public async Task<int> Handle(CreateFileShareCommand request, CancellationToken cancellationToken)
    {
        var file = await dbContext.Files.FindAsync(request.FileId);
        fileShareBusinessRules.FileIsExist(file);

        var userIdClaim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        var userId = fileShareBusinessRules.ClaimIsNull(userIdClaim);
        fileShareBusinessRules.IsMatchedUserId(userId, request.UserId);

        var fileShare = mapper.Map<FileShare>(request);

        await dbContext.FileShares.AddAsync(fileShare);
        await dbContext.SaveChangesAsync(cancellationToken);
        return fileShare.Id;
    }
}
