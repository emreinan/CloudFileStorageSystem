using FileStorageAPI.Application.Features.Commands.Upload;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileStorageAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileStorageController(IMediator mediator) : ControllerBase
{
    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] UploadFileDto uploadFileDto)
    {
        var command = new UploadFileCommand { Upload = uploadFileDto };
        var result = await mediator.Send(command);
        return Ok(result);
    }

    
}
