﻿using CloudFileStorageMVC.Models;

namespace CloudFileStorageMVC.Services.File
{
    public interface IFileApiService
    {
        Task<List<FileViewModel>> GetFilesAsync();
        Task<EditViewModel> GetFileAsync(int fileId);
        Task<Stream> GetFileStreamAsync(string fileName);
        Task<FileStorageResponseModel> UploadFileStorageAsync(IFormFile file, string description);
        Task EditFileAsync(int id, EditViewModel model);
        Task DeleteFileMetadataAsync(int id);
        Task DeleteFileStorageAsync(string fileName);
        Task AddFileMetadataAsync(AddFileMetadataRequestModel model);
    }
}