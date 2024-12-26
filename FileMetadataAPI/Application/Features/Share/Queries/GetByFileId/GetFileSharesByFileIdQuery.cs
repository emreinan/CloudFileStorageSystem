using AutoMapper;
using FileMetadataAPI.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FileMetadataAPI.Application.Features.Share.Queries.GetByFileId;

public record GetFileSharesByFileIdQuery(int FileId) : IRequest<List<FileShareDto>>;

public class GetFileSharesByFileIdQueryHandler(FileMetaDataDbContext dbContext,IMapper mapper)
    : IRequestHandler<GetFileSharesByFileIdQuery, List<FileShareDto>>
{

    public async Task<List<FileShareDto>> Handle(GetFileSharesByFileIdQuery request, CancellationToken cancellationToken)
    {
        var shares = await dbContext.FileShares.Where(x => x.FileId == request.FileId).ToListAsync(cancellationToken);
        return mapper.Map<List<FileShareDto>>(shares);
    }
}
