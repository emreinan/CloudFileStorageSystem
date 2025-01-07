using AuthenticationAPI.Application.Features.Users.Rules;
using AuthenticationAPI.Persistence.Context;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<GetUserByIdResponse>
    {
        public int Id { get; set; }

        class GetUserByIdQueryHandler(AuthDbContext authDbContext,IMapper mapper,UserBusinessRules userBusinessRules) : IRequestHandler<GetUserByIdQuery, GetUserByIdResponse>
        {
            public async Task<GetUserByIdResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await authDbContext.Users.FirstOrDefaultAsync(u => u.Id == request.Id);
                userBusinessRules.UserIsExist(user);
                var userDto = mapper.Map<GetUserByIdResponse>(user);
                return userDto;
            }
        }

    }
}
