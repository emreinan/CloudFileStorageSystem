using CloudFileStorageMVC.Dtos.File;
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
        foreach (var file in fileViewModels)
        {
            if (file.OwnerId == userId)
            {
                file.OwnerName = "Myself";
            }
            else
            {
                var user = await userService.GetUserById(file.OwnerId);
                file.OwnerName = user.Name ?? "Unknown";
            }
        }
        return View(fileViewModels);
    }

    [HttpGet]
    public async Task<IActionResult> Upload()
    {
        var model = await PrepareUploadViewModel();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file, [FromForm] UploadViewModel model)
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

        var fileMetadataRequest = new AddFileMetadataRequestDto
        (fileStorageResponse.Name, model.Description, model.SharingType, model.PermissionLevel);

        if (model.SharedWithUserIds is not null)
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
        var fileResponse = await fileApiService.GetFileAsync(id);
        var file = await PrepareEditViewModel(fileResponse);

        return View(file);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, [FromForm] EditViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var users = await GetUsers(userService);
            model.SharedWithUsers = users.Where(user => user.Id != GetCurrentUserId()).ToList();
            return View(model);
        }

        var editRequest = new EditFileRequestDto(model.Description, model.SharingType, model.PermissionLevel!);
        if (model.SharedWithUserIds is not null || model.SharedWithUserIds!.Count>0)
        {
            editRequest.SharedWithUserIds = model.SharedWithUserIds
                .Select(id => int.Parse(id.ToString())).ToList();
        }

        await fileApiService.EditFileAsync(id, editRequest);

        TempData["SuccessMessage"] = "File updated successfully!";
        return RedirectToAction(nameof(Files));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id, string fileName)
    {
        await fileApiService.DeleteFileMetadataAsync(id);
        await fileApiService.DeleteFileStorageAsync(fileName);

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

    private async Task<EditViewModel> PrepareEditViewModel(EditViewModel file)
    {
        var users = await GetUsers(userService);
        var userId = GetCurrentUserId();
        var filteredUsers = users.Where(user => user.Id != userId).ToList();

        var model = new EditViewModel
        {
            Description = file.Description,
            SharingType = file.SharingType,
            PermissionLevel = file.PermissionLevel,
            SharedWithUserIds = file.SharedWithUserIds ?? new (),
            SharedWithUsers = filteredUsers
        };
        return model;
    }
}
