using AuthServices.DTOs;
using AuthServices.Interfaces;
using AuthServices.Models;
using System.Security.Cryptography;

namespace AuthServices.Services
{
    public class TokenService(IJwtService _jwtService) : ITokenService
    {
        public TokensResponse GenerateTokens(User user)
        {
            // Access Token
            var accessToken = _jwtService.GenerateAccess(user);

            // Refresh Token
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            var refreshToken = Convert.ToBase64String(randomNumber);

            var tokens = new TokensResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return tokens;
        }
    }
}
