using FileMetadataAPI.Application.Features.FileMetadata.Rules;
using FileMetadataAPI.Infrastructure.Context;
using MediatR;

namespace FileMetadataAPI.Application.Features.FileMetadata.Commands.Delete;

public class DeleteFileMetadataCommand : IRequest
{
    public int Id { get; set; }

    class DeleteFileMetadataCommandHandler(
        FileMetaDataDbContext fileDbContext,
        FileMetadataBusinessRules fileBusinessRules,
        IHttpClientFactory httpClientFactory
        ) : IRequestHandler<DeleteFileMetadataCommand>
    {
        private readonly HttpClient httpClient = httpClientFactory.CreateClient("FileStorageApiClient");
        async Task IRequestHandler<DeleteFileMetadataCommand>.Handle(DeleteFileMetadataCommand request, CancellationToken cancellationToken)
        {
            var fileMetadata = await fileDbContext.Files.FindAsync(request.Id);
            fileBusinessRules.FileIsExists(fileMetadata);

            //var response = await httpClient.DeleteAsync($"api/FileStorage/delete/{fileMetadata!.Name}");

            //if (!response.IsSuccessStatusCode)
            //{
            //    throw new Exception("File not found in FileStorageAPI");
            //}

            fileDbContext.Files.Remove(fileMetadata!);
            await fileDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

