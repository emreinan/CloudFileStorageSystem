using AuthenticationAPI.Domain.Enums;

namespace AuthenticationAPI.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public Role Role { get; set; }

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = default!;
    }
}
