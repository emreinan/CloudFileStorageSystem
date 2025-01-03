using AutoMapper;
using FileMetadataAPI.Application.Features.FileMetadata.Rules;
using FileMetadataAPI.Domain.Enums;
using FileMetadataAPI.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using File = FileMetadataAPI.Domain.Entities.File;

namespace FileMetadataAPI.Application.Features.FileMetadata.Queries.GetList;

public class GetListFileQuery : IRequest<IEnumerable<GetListFileQueryDto>>
{
    class GetListFileQueryHandler(
        FileMetaDataDbContext fileMetaDataDbContext, 
        IMapper mapper,
        FileMetadataBusinessRules fileMetadataBusinessRules
        ) : IRequestHandler<GetListFileQuery, IEnumerable<GetListFileQueryDto>>
    {
        public async Task<IEnumerable<GetListFileQueryDto>> Handle(GetListFileQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<File> files;

            if (fileMetadataBusinessRules.IsUserAdmin())
            {
                files = await fileMetaDataDbContext.Files
                    .Include(f => f.FileShares).ToListAsync(cancellationToken);
            }
            else
            {
                var userId = fileMetadataBusinessRules.GetUserIdClaim();
                files = await fileMetaDataDbContext.Files
                    .Include(f => f.FileShares)
                    .Where(f =>
                        f.SharingType == SharingType.Public ||
                        (f.SharingType == SharingType.Private && f.OwnerId == userId) ||
                        (f.SharingType == SharingType.SharedWithSpecificUsers && f.FileShares.Any(fs => fs.UserId == userId))
                    ).ToListAsync(cancellationToken);
            }

            return mapper.Map<IEnumerable<GetListFileQueryDto>>(files);

        }
    }

}
