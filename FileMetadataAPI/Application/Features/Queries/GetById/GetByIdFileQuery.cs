using AutoMapper;
using FileMetadataAPI.Application.Features.Rules;
using FileMetadataAPI.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FileMetadataAPI.Application.Features.Queries.GetById;

public class GetByIdFileQuery : IRequest<GetByIdFileQueryDto>
{
    public int Id { get; set; }

    class GetByIdFileQueryHandler(FileMetaDataDbContext fileMetaDataDbContext, IMapper mapper, FileBusinessRules fileBusinessRules) : IRequestHandler<GetByIdFileQuery, GetByIdFileQueryDto>
    {
        public async Task<GetByIdFileQueryDto> Handle(GetByIdFileQuery request, CancellationToken cancellationToken)
        {
            var file = await fileMetaDataDbContext.Files.Include(f => f.FileShares).FirstOrDefaultAsync(f => f.Id == request.Id);
            fileBusinessRules.FileIsExists(file);
            return mapper.Map<GetByIdFileQueryDto>(file);
        }
    }

}
