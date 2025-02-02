﻿using CloudFileStorageMVC.Dtos.File;
using CloudFileStorageMVC.Models;
using CloudFileStorageMVC.Services.Token;
using CloudFileStorageMVC.Util.ExceptionHandling;

namespace CloudFileStorageMVC.Services.File;

public class FileApiService(IHttpClientFactory httpClientFactory, ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
    : BaseService(httpClientFactory, tokenService, httpContextAccessor), IFileApiService
{
    public async Task AddFileMetadataAsync(AddFileMetadataRequestDto model)
    {
        var response = await httpClient.PostAsJsonAsync("/api/FileMetadata", model);
        if(!response.IsSuccessStatusCode)
        {
            await DeleteFileStorageAsync(model.Name);
            var apiError = await response.Content.ReadFromJsonAsync<ApiError>();
            throw new Exception(apiError?.Detail ?? "An error occurred while processing the request.");
        }
    }

    public async Task DeleteFileMetadataAsync(int id)
    {
        var response = await httpClient.DeleteAsync($"/api/FileMetadata/{id}");
        await response.EnsureSuccessStatusCodeWithApiError();
    }

    public async Task DeleteFileStorageAsync(string fileName)
    {
        var response = await httpClient.DeleteAsync($"api/FileStorage/delete/{fileName}");
        await response.EnsureSuccessStatusCodeWithApiError();
    }

    public async Task EditFileAsync(int id, EditFileRequestDto model)
    {
        var response = await httpClient.PutAsJsonAsync($"/api/FileMetadata/{id}", model);
        await response.EnsureSuccessStatusCodeWithApiError();
    }

    public async Task<EditViewModel> GetFileAsync(int fileId)
    {
        var response = await httpClient.GetAsync($"/api/FileMetadata/{fileId}");
        await response.EnsureSuccessStatusCodeWithApiError();
        return await response.Content.ReadFromJsonAsync<EditViewModel>();
    }

    public async Task<List<FileViewModel>> GetFilesAsync()
    {
        GatewayClientGetToken();
        var response = await httpClient.GetAsync("/api/FileMetadata");
        await response.EnsureSuccessStatusCodeWithApiError();
        return await response.Content.ReadFromJsonAsync<List<FileViewModel>>();
    }

    public async Task<Stream> GetFileStreamAsync(string fileName)
    {
        var response = await httpClient.GetAsync($"/api/FileStorage/download/{fileName}");
        await response.EnsureSuccessStatusCodeWithApiError();
        return await response.Content.ReadAsStreamAsync();
    }

    public async Task<FileStorageResponseModel> UploadFileStorageAsync(IFormFile file, string description)
    {
        GatewayClientGetToken();
        using var content = new MultipartFormDataContent();
        content.Add(new StringContent(description), "Description");
        content.Add(new StreamContent(file.OpenReadStream()), "File", file.FileName);

        var response = await httpClient.PostAsync("/api/FileStorage/upload", content);
        await response.EnsureSuccessStatusCodeWithApiError();
        return await response.Content.ReadFromJsonAsync<FileStorageResponseModel>();
    }
}
