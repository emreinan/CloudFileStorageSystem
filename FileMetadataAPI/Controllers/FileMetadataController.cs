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

    }
}
