namespace AuthServices.Models.DTOs
{
    public class TokensPair
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public TokensPair(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
