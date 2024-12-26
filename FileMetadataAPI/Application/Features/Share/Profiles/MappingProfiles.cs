using AutoMapper;
using FileMetadataAPI.Application.Common.Maping;
using FileMetadataAPI.Application.Features.Share.Commands.Create;
using FileMetadataAPI.Application.Features.Share.Queries.GetByFileId;
using FileMetadataAPI.Domain.Enums;
using FileShare = FileMetadataAPI.Domain.Entities.FileShare;

namespace FileMetadataAPI.Application.Features.Share.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<string, Permission>().ConvertUsing<StringToPermissionConverter>();
        CreateMap<CreateFileShareCommand, FileShare>();

        CreateMap<FileShare, FileShareDto>()
            .ForMember(dest => dest.Permission, opt => opt.MapFrom(src => src.Permission.ToString()));
    }
}
