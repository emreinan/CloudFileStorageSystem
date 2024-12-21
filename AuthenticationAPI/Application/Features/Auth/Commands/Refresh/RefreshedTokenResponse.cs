namespace AuthenticationAPI.Application.Features.Auth.Commands.RefreshToken;

public partial class RefreshTokenCommand
{
    public class RefreshedTokenResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
