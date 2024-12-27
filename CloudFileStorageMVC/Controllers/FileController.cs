using CloudFileStorageMVC.Models;
using CloudFileStorageMVC.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudFileStorageMVC.Controllers;

[Authorize]
public class FileController(IHttpClientFactory httpClientFactory, ITokenService tokenService) : BaseController(tokenService, httpClientFactory)
{
    [HttpGet]
    public async Task<IActionResult> Files()
    {
        var endpoint = User.IsInRole("Admin")? "/api/FileMetadata": $"/api/FileMetadata/{GetUserId()}";

        var response = await httpClient.GetAsync(endpoint);
        if (!response.IsSuccessStatusCode)
        {
            TempData["ErrorMessage"] = "An error occurred while fetching the files.";
            return BadRequest(response);
        }

        var files = await response.Content.ReadFromJsonAsync<List<FileViewModel>>();
        return View(files);
    }

    [HttpGet]
    public IActionResult Upload()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file, FileRequestModel model)
    {
        if (file == null || file.Length == 0)
        {
            ModelState.AddModelError("", "Please select a valid file.");
            return View();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        GatewayClientGetToken();
        using var content = new MultipartFormDataContent();
        content.Add(new StringContent(model.Description), "Description");
        content.Add(new StreamContent(file.OpenReadStream()), "File", file.FileName);

        var response = await httpClient.PostAsync("/api/FileStorage/upload", content);
        if (!response.IsSuccessStatusCode)
        {
            TempData["ErrorMessage"] = "File uploaded is failed.";
            return View();
        }

        var fileStorageResponse = await response.Content.ReadFromJsonAsync<FileStorageResponseModel>();

        //var fileShareRequestDto = new CreateFileShareRequestModel ;
        var responseShare = await httpClient.PostAsJsonAsync("/api/FileShare", new { FileId = fileStorageResponse!.Id, UserId = GetUserId(), Permission = model.Permission });
        responseShare.EnsureSuccessStatusCode();
        //if (!responseShare.IsSuccessStatusCode)
        //{
        //    TempData["ErrorMessage"] = "There is a mistake when uploading FileShare";
        //    return View();
        //}

        TempData["SuccessMessage"] = "File uploaded successfully!";
        return RedirectToAction("Files");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var response = await httpClient.GetAsync($"/api/FileMetadata/{id}");
        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError("", "An error occurred while uploading the file.");
            return View();
        }

        var file = await response.Content.ReadFromJsonAsync<FileRequestModel>();
        return View(file);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, FileRequestModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var response = await httpClient.PutAsJsonAsync($"/api/FileMetadata/{id}", model);
        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError("", "An error occurred while updating the file.");
            return View(model);
        }

        TempData["SuccessMessage"] = "File updated successfully!";
        return RedirectToAction(nameof(Files));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await httpClient.DeleteAsync($"/api/FileMetadata/{id}");
        if (!response.IsSuccessStatusCode)
        {
            TempData["ErrorMessage"] = "An error occurred while deleting the file.";
            return RedirectToAction("Files");
        }

        TempData["SuccessMessage"] = "File deleted successfully!";
        return RedirectToAction("Files");
    }

    [HttpGet]
    public async Task<IActionResult> GetFile(string fileName)
    {
        var response = await httpClient.GetAsync($"/api/FileStorage/download/{fileName}");
        if (!response.IsSuccessStatusCode)
        {
            TempData["ErrorMessage"] = "An error occurred while downloading the file.";
            return RedirectToAction("Files");
        }

        var stream = await response.Content.ReadAsStreamAsync();
        return File(stream, "application/octet-stream", fileName);
    }
}
