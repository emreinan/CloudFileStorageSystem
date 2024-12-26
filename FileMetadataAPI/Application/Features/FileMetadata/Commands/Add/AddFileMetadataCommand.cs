using AutoMapper;
using FileMetadataAPI.Domain.Enums;
using FileMetadataAPI.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using File = FileMetadataAPI.Domain.Entities.File;
using FileShare = FileMetadataAPI.Domain.Entities.FileShare;

namespace FileMetadataAPI.Application.Features.FileMetadata.Commands.Add;

public class AddFileMetadataCommand : IRequest<AddFileMetadataResponse>
{
    public int OwnerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime UploadDate { get; set; }
    public string Permission { get; set; }

    class AddFileMetadataCommandHandler(
        FileMetaDataDbContext dbContext,
        IMapper mapper
        ) : IRequestHandler<AddFileMetadataCommand, AddFileMetadataResponse>
    {
        public async Task<AddFileMetadataResponse> Handle(AddFileMetadataCommand request, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<File>(request);
            await dbContext.Files.AddAsync(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            var fileEntity = await dbContext.Files.FirstOrDefaultAsync(f => f.Name == request.Name);

            var fileShare = new FileShare
            {
                FileId = fileEntity!.Id,
                Permission = Enum.Parse<Permission>(request.Permission, true),
                UserId = request.OwnerId
            };
            await dbContext.FileShares.AddAsync(fileShare);
            await dbContext.SaveChangesAsync(cancellationToken);

            return mapper.Map<AddFileMetadataResponse>(entity);
        }
    }
}
