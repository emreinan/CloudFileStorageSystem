using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.Application.Features.Auth.Constants;

internal static class AuthErrorMessages
{
    internal const string UserNotFound = "User not found.";
    internal const string InvalidPassword = "Invalid password";
    internal const string EmailInUse = "Email address is already in use";
    internal const string RefreshTokenNotFound = "Refresh token not found";
    internal const string InvalidRefreshToken = "Invalid refresh token";
}
