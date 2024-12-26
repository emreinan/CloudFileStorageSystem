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
        CreateMap<File, GetListFileQueryDto>()
            .ForMember(dest => dest.Permission, opt => opt.MapFrom(src =>
                src.FileShares.FirstOrDefault().Permission.ToString() ?? "No Permission"));

        CreateMap<File, GetByIdFileQueryDto>().ForMember(dest => dest.Permission, opt => opt.MapFrom(src =>
                src.FileShares.FirstOrDefault().Permission.ToString() ?? "No Permission"));

        CreateMap<AddFileMetadataCommand, File>()
        .ForSourceMember(src => src.Permission, opt => opt.DoNotValidate());

        CreateMap<File, AddFileMetadataResponse>();


    }
}
