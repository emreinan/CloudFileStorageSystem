using AutoMapper;
using CloudFileStorageMVC.Dtos.Auth;
using CloudFileStorageMVC.Models;

namespace CloudFileStorageMVC.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LoginDto, LoginViewModel>().ReverseMap();
        CreateMap<RegisterDto, RegisterViewModel>().ReverseMap();
    }
}
