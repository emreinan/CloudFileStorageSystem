using AutoMapper;
using FileMetadataAPI.Application.Common.Mapping;
using FileMetadataAPI.Application.Features.FileMetadata.Commands.Add;
using FileMetadataAPI.Application.Features.FileMetadata.Queries.GetById;
using FileMetadataAPI.Application.Features.FileMetadata.Queries.GetList;
using FileMetadataAPI.Domain.Enums;
using System.Security;
using File = FileMetadataAPI.Domain.Entities.File;

namespace FileMetadataAPI.Application.Features.FileMetadata.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<SharingType, string>().ConvertUsing<PermissionToStringConverter>();

        CreateMap<File, GetListFileQueryDto>()
            .ForMember(dest => dest.PermissionLevel, opt => opt.MapFrom(src =>
                src.FileShares.FirstOrDefault()!.PermissionLevel.ToString() ?? string.Empty));

        CreateMap<File, GetByFileIdFileQueryDto>()
            .ForMember(dest => dest.PermissionLevel, opt => opt.MapFrom(src =>
                src.FileShares.FirstOrDefault()!.PermissionLevel.ToString() ?? string.Empty));
    }
}
