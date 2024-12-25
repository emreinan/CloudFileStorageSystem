using AutoMapper;
using FileMetadataAPI.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FileMetadataAPI.Application.Features.Queries.GetList;

public class GetListFileQuery : IRequest<IEnumerable<GetListFileQueryDto>>
{
    class GetListFileQueryHandler(FileMetaDataDbContext fileMetaDataDbContext, IMapper mapper) : IRequestHandler<GetListFileQuery, IEnumerable<GetListFileQueryDto>>
    {
        public async Task<IEnumerable<GetListFileQueryDto>> Handle(GetListFileQuery request, CancellationToken cancellationToken)
        {
            var files = await fileMetaDataDbContext.Files.Include(f=>f.FileShares).ToListAsync();
            return mapper.Map<IEnumerable<GetListFileQueryDto>>(files);
        }
    }

}
