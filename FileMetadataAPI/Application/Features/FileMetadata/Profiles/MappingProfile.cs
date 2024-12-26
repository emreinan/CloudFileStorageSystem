using AutoMapper;
using FileMetadataAPI.Application.Common.Maping;
using FileMetadataAPI.Application.Features.FileMetadata.Commands.Add;
using FileMetadataAPI.Application.Features.FileMetadata.Queries.GetById;
using FileMetadataAPI.Application.Features.FileMetadata.Queries.GetList;
using FileMetadataAPI.Domain.Enums;
using File = FileMetadataAPI.Domain.Entities.File;

namespace FileMetadataAPI.Application.Features.FileMetadata.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<string, Permission>().ConvertUsing<StringToPermissionConverter>();

        CreateMap<File, GetListFileQueryDto>();

        CreateMap<File, GetByIdFileQueryDto>();

        CreateMap<AddFileMetadataCommand, File>()
        .ForSourceMember(src => src.Permission, opt => opt.DoNotValidate());

        CreateMap<File, AddFileMetadataResponse>();


    }
}
