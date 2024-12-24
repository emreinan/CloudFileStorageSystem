using FileStorageAPI.Application.Features.Commands.Delete;
using FileStorageAPI.Application.Features.Commands.Download;
using FileStorageAPI.Application.Features.Commands.Upload;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FileStorageAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileStorageController(IMediator mediator) : ControllerBase
{
    [Authorize]
    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] UploadFileDto uploadFileDto)
    {
        var command = new UploadFileCommand { Upload = uploadFileDto };
        var result = await mediator.Send(command);
        return Ok(result);
    }
    [HttpGet("download/{FileName}")]
    public async Task<IActionResult> Download([FromRoute] DownloadFileDto downloadFileDto)
    {
        var command = new DownloadFileCommand { Download = downloadFileDto };
        var result = await mediator.Send(command);

        if (result is null)
        {
            return NotFound("File not found.");
        }

        return File(result.Stream, result.ContentType, result.FileName);
    }

    [HttpDelete("delete/{FileName}")]
    public async Task<IActionResult> Delete([FromRoute] DeleteFileDto deleteFileDto)
    {
        var command = new DeleteFileCommand { Delete = deleteFileDto };
        var result = await mediator.Send(command);

        if (!result)
        {
            return NotFound("File not found.");
        }

        return NoContent();
    }

}
