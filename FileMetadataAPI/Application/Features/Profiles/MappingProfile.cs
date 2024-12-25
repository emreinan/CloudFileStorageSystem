using AutoMapper;
using FileMetadataAPI.Application.Features.Commands.Add;
using FileMetadataAPI.Application.Features.Queries.GetById;
using FileMetadataAPI.Application.Features.Queries.GetList;
using File = FileMetadataAPI.Domain.Entities.File;

namespace FileMetadataAPI.Application.Features.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<File, GetListFileQueryDto>();
        CreateMap<File, GetByIdFileQueryDto>();

        CreateMap<AddFileMetadataCommand, File>()
        .ForSourceMember(src => src.Permission, opt => opt.DoNotValidate());

        CreateMap<File, AddFileMetadataResponse>();


    }
}
