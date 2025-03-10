namespace AuthServices.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string HashedPassword { get; set; } = string.Empty;
    }
}
