namespace AuthServices.Services
{
    public class JwtOptions
    {
        public string Key { get; set; } = string.Empty;
        public int ExpiresHours { get; set; }
    }
}