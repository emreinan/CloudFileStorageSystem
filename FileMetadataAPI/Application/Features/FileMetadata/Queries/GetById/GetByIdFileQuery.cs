using AutoMapper;
using FileMetadataAPI.Application.Features.FileMetadata.Rules;
using FileMetadataAPI.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FileMetadataAPI.Application.Features.FileMetadata.Queries.GetById;

public class GetByIdFileQuery : IRequest<IEnumerable<GetByIdFileQueryDto>>
{
    public int Id { get; set; }

    class GetByIdFileQueryHandler(FileMetaDataDbContext fileMetaDataDbContext, IMapper mapper) : IRequestHandler<GetByIdFileQuery, IEnumerable<GetByIdFileQueryDto>>
    {
        public async Task<IEnumerable<GetByIdFileQueryDto>> Handle(GetByIdFileQuery request, CancellationToken cancellationToken)
        {
            var files = await fileMetaDataDbContext.Files.Include(f => f.FileShares).Where(f => f.OwnerId == request.Id).ToListAsync();
            return mapper.Map<IEnumerable<GetByIdFileQueryDto>>(files);
        }
    }

}
