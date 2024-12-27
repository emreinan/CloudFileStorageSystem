using CloudFileStorageMVC.Models;
using CloudFileStorageMVC.Services.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudFileStorageMVC.Controllers;

[Authorize]
public class FileController(IFileApiService fileApiService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Files()
    {
        var fileViewModels = await fileApiService.GetFilesAsync();
        return View(fileViewModels);
    }

    [HttpGet]
    public IActionResult Upload()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file, FileRequestModel model)
    {
        if (file is null || file.Length == 0)
        {
            TempData["ErrorMessage"] = "Please select a file to upload.";
            return View();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var fileStorageResponse = await fileApiService.UploadFileAsync(file, model);

        await fileApiService.AddFileShare(fileStorageResponse.Id, model.Permission);

        TempData["SuccessMessage"] = "File uploaded successfully!";
        return RedirectToAction("Files");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var file = await fileApiService.GetFileAsync(id);
        return View(file);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, FileRequestModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await fileApiService.EditFileAsync(id, model);

        TempData["SuccessMessage"] = "File updated successfully!";
        return RedirectToAction(nameof(Files));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await fileApiService.DeleteFileAsync(id);

        TempData["SuccessMessage"] = "File deleted successfully!";
        return RedirectToAction("Files");
    }

    [HttpGet]
    public async Task<IActionResult> GetFileStream(string fileName)
    {
        var stream = await fileApiService.GetFileStreamAsync(fileName);

        return File(stream, "application/octet-stream", fileName);
    }
}
