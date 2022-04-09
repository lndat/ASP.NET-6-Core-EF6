namespace csm6.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string PasswordHash { get; set; } = String.Empty;
        public string PasswordSalt { get; set; } = String.Empty;
        public Team? Team { get; set; }

    }
}
