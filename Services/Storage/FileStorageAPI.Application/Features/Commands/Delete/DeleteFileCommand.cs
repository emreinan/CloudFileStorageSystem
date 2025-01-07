using MediatR;

namespace FileStorageAPI.Application.Features.Commands.Delete;

public class DeleteFileCommand : IRequest<bool>
{
    public DeleteFileDto Delete { get; set; }

    internal class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, bool>
    {
        private const string UploadPath = "wwwroot/Uploads";

        public async Task<bool> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), UploadPath, request.Delete.FileName);

            if (!File.Exists(filePath))
            {
                return false;
            }

            File.Delete(filePath);
            return true; 
        }
    }
}
