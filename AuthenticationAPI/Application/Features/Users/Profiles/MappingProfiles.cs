using AuthenticationAPI.Application.Features.Users.Queries;
using AuthenticationAPI.Domain.Entities;
using AutoMapper;

namespace AuthenticationAPI.Application.Features.Users.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, GetUsersListDto>();
        }
    }
}
