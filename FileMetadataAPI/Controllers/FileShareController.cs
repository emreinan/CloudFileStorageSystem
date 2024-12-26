using FileMetadataAPI.Application.Features.Share.Commands.Create;
using FileMetadataAPI.Application.Features.Share.Commands.Delete;
using FileMetadataAPI.Application.Features.Share.Commands.Update;
using FileMetadataAPI.Application.Features.Share.Queries.GetByFileId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FileMetadataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileShareController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetShares([FromQuery] int fileId)
    {
        var query = new GetFileSharesByFileIdQuery(fileId);
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateShare([FromBody] CreateFileShareDto dto)
    {
        var command = new CreateFileShareCommand(dto.FileId, dto.UserId, dto.Permission);
        var createdId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetShares), new { fileId = dto.FileId }, new { Id = createdId });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateShare(int id, [FromBody] UpdateFileShareDto dto)
    {
        var command = new UpdateFileShareCommand(id, dto.Permission);
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShare(int id)
    {
        var command = new DeleteFileShareCommand(id);
        await mediator.Send(command);
        return NoContent();
    }
}
