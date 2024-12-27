using CloudFileStorageMVC.Models;

namespace CloudFileStorageMVC.Services.File
{
    public interface IFileApiService
    {
        Task<List<FileViewModel>> GetFilesAsync();
        Task<FileRequestModel> GetFileAsync(int userId);
        Task<Stream> GetFileStreamAsync(string fileName);
        Task<FileStorageResponseModel> UploadFileAsync(IFormFile file, FileRequestModel model);
        Task EditFileAsync(int id, FileRequestModel model);
        Task DeleteFileAsync(int id);
        Task AddFileShare(int fileId, string permission);

    }
}