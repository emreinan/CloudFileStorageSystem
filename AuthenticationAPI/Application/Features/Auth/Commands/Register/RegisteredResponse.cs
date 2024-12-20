namespace AuthenticationAPI.Application.Features.Auth.Commands.Register;

public class RegisteredResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
