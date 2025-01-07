using AuthenticationAPI.Domain.Enums;
using AuthenticationAPI.Persistence.Context;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAPI.Application.Features.Users.Queries.GetList;

public class GetUsersListQuery : IRequest<List<GetUsersListDto>>
{
    public class GetUsersListQueryHandler(AuthDbContext authDbContext, IMapper mapper) : IRequestHandler<GetUsersListQuery, List<GetUsersListDto>>
    {
        public async Task<List<GetUsersListDto>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            var users = await authDbContext.Users.Where(u => u.Role == Role.User).ToListAsync(cancellationToken);
            return mapper.Map<List<GetUsersListDto>>(users);
        }
    }
}
