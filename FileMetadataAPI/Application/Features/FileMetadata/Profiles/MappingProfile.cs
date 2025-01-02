using AutoMapper;
using FileMetadataAPI.Application.Features.FileMetadata.Commands.Add;
using FileMetadataAPI.Application.Features.FileMetadata.Queries.GetById;
using FileMetadataAPI.Application.Features.FileMetadata.Queries.GetList;
using File = FileMetadataAPI.Domain.Entities.File;

namespace FileMetadataAPI.Application.Features.FileMetadata.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        CreateMap<File, GetListFileQueryDto>().ForMember(dest => dest.Permission, opt => opt.MapFrom(src =>
                src.FileShares.FirstOrDefault().PermissionLevel.ToString()));
        CreateMap<File, GetByIdFileQueryDto>().ForMember(dest => dest.Permission, opt => opt.MapFrom(src =>
                src.FileShares.FirstOrDefault().PermissionLevel.ToString()));
        CreateMap<File, AddFileMetadataResponse>();
    }
}
