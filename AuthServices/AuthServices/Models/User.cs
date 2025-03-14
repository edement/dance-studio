namespace AuthServices.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
    }
}
