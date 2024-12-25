using CloudFileStorageMVC.Models;
using CloudFileStorageMVC.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace CloudFileStorageMVC.Controllers;

[Authorize]
public class FileController(IHttpClientFactory httpClientFactory,ITokenService tokenService) : Controller
{
    private readonly HttpClient httpClient = httpClientFactory.CreateClient("GetewayApiClient");

    public async Task<IActionResult> Files()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var endpoint = User.IsInRole("Admin") ? "/api/FileMetadata" : $"/api/FileMetadata/{userId}";

        var response = await httpClient.GetAsync(endpoint);
        if (response.IsSuccessStatusCode)
        {
            var files = await response.Content.ReadFromJsonAsync<List<FileViewModel>>();
            return View(files);
        }

        return View("Error");
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

        var token = tokenService.GetAccessToken();
        httpClient.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", token);

        using var content = new MultipartFormDataContent();
        content.Add(new StringContent(model.Description), "Description");
        content.Add(new StringContent(model.Permission), "Permission");
        content.Add(new StreamContent(file.OpenReadStream()), "File", file.FileName);

        var response = await httpClient.PostAsync("/api/FileStorage/upload", content);
        if (!response.IsSuccessStatusCode)
        {
            return RedirectToAction("Files");
        }

        return View("Error");
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var response = await httpClient.GetAsync($"/api/FileMetadata/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return View("Error");
        }

        var file = await response.Content.ReadFromJsonAsync<FileViewModel>();
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
            return View("Error");
        }

        return RedirectToAction(nameof(Files));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await httpClient.DeleteAsync($"/api/FileMetadata/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return View("Error");
        }

        return RedirectToAction("Files");
    }
}
