using FileMetadataAPI.Application.Features.Commands.Add;
using FileMetadataAPI.Application.Features.Commands.Delete;
using FileMetadataAPI.Application.Features.Commands.Update;
using FileMetadataAPI.Application.Features.Queries.GetById;
using FileMetadataAPI.Application.Features.Queries.GetList;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> GetFileById(int id)
        {
            var response = await mediator.Send(new GetByIdFileQuery { Id = id });
            return Ok(response);
        }

        [HttpPost]
        //[ApiExplorerSettings(IgnoreApi = true)] // Swagger will ignore this endpoint
        public async Task<IActionResult> AddFile([FromBody] AddFileMetadataCommand command)
        {
            var result = await mediator.Send(command);
            return CreatedAtAction(nameof(GetFileById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFile(int id, [FromBody] UpdateFileMetadataRequest request)
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
