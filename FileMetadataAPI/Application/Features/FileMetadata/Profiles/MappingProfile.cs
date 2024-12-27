using AutoMapper;
using FileMetadataAPI.Application.Common.Maping;
using FileMetadataAPI.Application.Features.FileMetadata.Commands.Add;
using FileMetadataAPI.Application.Features.FileMetadata.Queries.GetById;
using FileMetadataAPI.Application.Features.FileMetadata.Queries.GetList;
using FileMetadataAPI.Application.Features.Share.Commands.Create;
using FileMetadataAPI.Domain.Enums;
using File = FileMetadataAPI.Domain.Entities.File;
using FileShare = FileMetadataAPI.Domain.Entities.FileShare;

namespace FileMetadataAPI.Application.Features.FileMetadata.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        CreateMap<string, Permission>().ConvertUsing<StringToPermissionConverter>();
        CreateMap<Permission, string>().ConvertUsing<PermissionToStringConverter>();

        CreateMap<AddFileMetadataCommand, File>();
        CreateMap<CreateFileShareCommand, FileShare>();
        CreateMap<File, GetListFileQueryDto>().ForMember(dest => dest.Permission, opt => opt.MapFrom(src =>
                src.FileShares.FirstOrDefault().Permission.ToString()));
        CreateMap<File, GetByIdFileQueryDto>().ForMember(dest => dest.Permission, opt => opt.MapFrom(src =>
                src.FileShares.FirstOrDefault().Permission.ToString()));
        CreateMap<File, AddFileMetadataResponse>();

        
    }
}
