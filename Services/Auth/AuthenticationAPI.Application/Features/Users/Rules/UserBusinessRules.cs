using AuthenticationAPI.Application.Features.Users.Constants;
using AuthenticationAPI.Core.Exceptions.Types;
using AuthenticationAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.Application.Features.Users.Rules
{
    public class UserBusinessRules
    {
        public void UserIsExist(User user)
        {
            if (user is null)
            {
                throw new NotFoundException(UserErrorMessage.UserNotFound);
            }
        }
    }
}
