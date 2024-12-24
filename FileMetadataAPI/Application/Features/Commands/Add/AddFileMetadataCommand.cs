using AutoMapper;
using FileMetadataAPI.Infrastructure.Context;
using MediatR;
using File= FileMetadataAPI.Domain.Entities.File;

namespace FileMetadataAPI.Application.Features.Commands.Add;

public class AddFileMetadataCommand : IRequest<AddFileMetadataResponse>
{
    public int OwnerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime UploadDate { get; set; }

    class AddFileMetadataCommandHandler(
        FileMetaDataDbContext dbContext,
        IMapper mapper
        ) : IRequestHandler<AddFileMetadataCommand, AddFileMetadataResponse>
    {
        public async Task<AddFileMetadataResponse> Handle(AddFileMetadataCommand request, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<File>(request);
            await dbContext.Files.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return mapper.Map<AddFileMetadataResponse>(entity);
        }
    }
}
