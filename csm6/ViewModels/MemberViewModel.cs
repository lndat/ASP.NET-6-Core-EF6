namespace csm6.ViewModels
{
    public class MemberViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string PasswordSalt { get; set; } = String.Empty;
    }
}
