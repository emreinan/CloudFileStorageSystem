using AutoMapper;
using FileMetadataAPI.Application.Features.Rules;
using FileMetadataAPI.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FileMetadataAPI.Application.Features.Queries.GetById;

public class GetByFileIdFileQuery : IRequest<GetByFileIdFileQueryDto>
{
    public int Id { get; set; }

    class GetByIdFileQueryHandler(FileMetaDataDbContext fileMetaDataDbContext, IMapper mapper, FileMetadataBusinessRules fileMetadataBusinessRules) : IRequestHandler<GetByFileIdFileQuery, GetByFileIdFileQueryDto>
    {
        public async Task<GetByFileIdFileQueryDto> Handle(GetByFileIdFileQuery request, CancellationToken cancellationToken)
        {

            var file = await fileMetaDataDbContext.Files
                .Include(f => f.FileShares)
                .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

            fileMetadataBusinessRules.FileIsExists(file);

            var fileDto = mapper.Map<GetByFileIdFileQueryDto>(file);
            return fileDto;

        }
    }

}
