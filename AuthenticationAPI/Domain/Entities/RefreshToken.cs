namespace AuthenticationAPI.Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime? RevokedDate { get; set; }
        public DateTime CreatedaAt { get; set; }


        public virtual User User { get; set; } = default!;
    }
}
