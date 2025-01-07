using AuthenticationAPI.Application.Features.Users.Queries.GetUserById;
using AuthenticationAPI.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.Application.Features.Users.Queries.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, GetUserByIdResponse>();
        }
    }
}
