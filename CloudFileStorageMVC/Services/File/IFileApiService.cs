using CloudFileStorageMVC.Models;

namespace CloudFileStorageMVC.Services.File
{
    public interface IFileApiService
    {
        Task<List<FileViewModel>> GetFilesAsync();
        Task<FileRequestModel> GetFileAsync(int fileId);
        Task<Stream> GetFileStreamAsync(string fileName);
        Task<FileStorageResponseModel> UploadFileStorageAsync(IFormFile file, string description);
        Task EditFileAsync(int id, FileRequestModel model);
        Task DeleteFileMetadataAsync(int id);
        Task DeleteFileStorageAsync(string fileName);
        Task AddFileShare(int fileId, string permission);
        Task AddFileMetadataAsync(AddFileMetadataRequestModel model);
    }
}