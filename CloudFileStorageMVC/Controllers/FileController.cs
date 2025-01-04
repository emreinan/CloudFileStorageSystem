using CloudFileStorageMVC.Dtos.User;
using CloudFileStorageMVC.Models;
using CloudFileStorageMVC.Services.File;
using CloudFileStorageMVC.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CloudFileStorageMVC.Controllers;

[Authorize]
public class FileController(IFileApiService fileApiService, IUserService userService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Files()
    {
        var userId = GetCurrentUserId();
        ViewBag.UserId = userId;

        var fileViewModels = await fileApiService.GetFilesAsync();
        return View(fileViewModels);
    }

    [HttpGet]
    public async Task<IActionResult> Upload()
    {
        var model = await PrepareUploadViewModel();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file, [FromForm]UploadViewModel model)
    {
        if (file is null || file.Length == 0)
        {
            TempData["ErrorMessage"] = "Please select a file to upload.";
            return View();
        }

        if (!ModelState.IsValid)
        {
            model = await PrepareUploadViewModel();
            return View(model);
        }

     
        var fileStorageResponse = await fileApiService.UploadFileStorageAsync(file, model.Description);

        var fileMetadataRequest = new AddFileMetadataRequestModel
        (fileStorageResponse.Name, model.Description, model.SharingType, model.PermissionLevel);

        if (model.SharedWithUserIds != null)
        {
            fileMetadataRequest.SharedWithUserIds = model.SharedWithUserIds
                .Select(id => int.Parse(id.ToString())) 
                .ToList();
        }

        await fileApiService.AddFileMetadataAsync(fileMetadataRequest);

        TempData["SuccessMessage"] = "File uploaded successfully!";
        return RedirectToAction("Files");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var users = await GetUsers(userService);
        ViewBag.Users = users;

        var file = await fileApiService.GetFileAsync(id);
        return View(file);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, EditViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var users = await GetUsers(userService);
            ViewBag.Users = users;

            return View(model);
        }

        await fileApiService.EditFileAsync(id, model);

        TempData["SuccessMessage"] = "File updated successfully!";
        return RedirectToAction(nameof(Files));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await fileApiService.DeleteFileMetadataAsync(id);

        TempData["SuccessMessage"] = "File deleted successfully!";
        return RedirectToAction("Files");
    }

    [HttpGet]
    public async Task<IActionResult> GetFileStream(string fileName)
    {
        var stream = await fileApiService.GetFileStreamAsync(fileName);

        return File(stream, "application/octet-stream", fileName);
    }

    private async Task<List<UserDto>> GetUsers(IUserService userService)
    {
        var users = await userService.GetUsersAsync();
        return users;
    }

    private int GetCurrentUserId()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        return userId;
    }

    private async Task<UploadViewModel> PrepareUploadViewModel()
    {
        var users = await GetUsers(userService);
        var userId = GetCurrentUserId();
        var filteredUsers = users.Where(user => user.Id != userId).ToList();

        return new UploadViewModel
        {
            SharedWithUsers = filteredUsers
        };
    }

}
