using FileMetadataAPI.Application.Features.FileMetadata.Commands.Add;
using FileMetadataAPI.Application.Features.FileMetadata.Commands.Delete;
using FileMetadataAPI.Application.Features.FileMetadata.Commands.Update;
using FileMetadataAPI.Application.Features.FileMetadata.Queries.GetById;
using FileMetadataAPI.Application.Features.FileMetadata.Queries.GetList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FileMetadataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileMetadataController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetFiles()
        {
            var response = await mediator.Send(new GetListFileQuery());
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFileById([FromRoute]int id)
        {
            var response = await mediator.Send(new GetByIdFileQuery { Id = id });
            return Ok(response);
        }

        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)] // Swagger'da action gözükmesin
        public async Task<IActionResult> AddFile([FromBody] AddFileMetadataCommand command)
        {
            // Yanlızca FileStorageAPI tarafından istek yapılabilsin, elle veri eklenemesin
            if (!Request.Headers.TryGetValue("x-source", out var source) || source != "FileStorageAPI")
            {
                return Forbid("Unauthorized source");
            }
            var result = await mediator.Send(command);
            return Ok(result.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFile([FromRoute]int id, [FromBody] UpdateFileMetadataRequest request)
        {
            await mediator.Send(new UpdateFileMetadataCommand { Id = id, Request = request });
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            await mediator.Send(new DeleteFileMetadataCommand { Id = id });
            return NoContent();
        }
    }
}
