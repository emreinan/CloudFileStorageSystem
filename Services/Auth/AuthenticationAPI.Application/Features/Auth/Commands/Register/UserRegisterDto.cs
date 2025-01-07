namespace AuthenticationAPI.Application.Features.Auth.Commands.Register;

public class UserRegisterDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
