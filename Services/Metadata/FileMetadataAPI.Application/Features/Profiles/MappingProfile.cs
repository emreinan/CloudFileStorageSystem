using AutoMapper;
using FileMetadataAPI.Application.Features.Queries.GetById;
using FileMetadataAPI.Application.Features.Queries.GetList;
using File = FileMetadataAPI.Domain.Entities.File;

namespace FileMetadataAPI.Application.Features.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        CreateMap<File, GetListFileQueryDto>()
            .ForMember(dest => dest.SharingType, opt => opt.MapFrom(src => src.SharingType))
            .ForMember(dest => dest.PermissionLevel, opt => opt.MapFrom(src =>
                src.FileShares != null && src.FileShares.Count > 0
                    ? src.FileShares.First().PermissionLevel.ToString()
                    : string.Empty
            ));

        CreateMap<File, GetByFileIdFileQueryDto>()
            .ForMember(dest => dest.SharingType, opt => opt.MapFrom(src => src.SharingType))
            .ForMember(dest => dest.PermissionLevel, opt => opt.MapFrom(src =>
                src.FileShares != null && src.FileShares.Count > 0
                    ? src.FileShares.First().PermissionLevel.ToString()
                    : string.Empty
            ));
    }
}
