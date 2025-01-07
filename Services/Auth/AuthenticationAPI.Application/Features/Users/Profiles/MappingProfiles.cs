using AuthenticationAPI.Domain.Entities;
using AutoMapper;
using AuthenticationAPI.Application.Features.Users.Queries.GetList;

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
