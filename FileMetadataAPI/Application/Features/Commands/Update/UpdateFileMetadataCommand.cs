using FileMetadataAPI.Application.Features.Rules;
using FileMetadataAPI.Infrastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FileMetadataAPI.Application.Features.Commands.Update;

public class UpdateFileMetadataCommand : IRequest
{
    public int Id { get; set; }
    public UpdateFileMetadataRequest Request { get; set; }

    class UpdateFileMetadataCommandHandler(FileMetaDataDbContext fileMetaDataDbContext,FileBusinessRules fileBusinessRules) : IRequestHandler<UpdateFileMetadataCommand>
    {

        async Task IRequestHandler<UpdateFileMetadataCommand>.Handle(UpdateFileMetadataCommand request, CancellationToken cancellationToken)
        {
            var file = await fileMetaDataDbContext.Files.FindAsync(request.Id);

            fileBusinessRules.FileIsExists(file);

            file.Description = request.Request.Description;

            await fileMetaDataDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
