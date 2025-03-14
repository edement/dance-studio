using AuthServices.Data.Interfaces;
using AuthServices.Models;
using AuthServices.Models.DTOs;
using AuthServices.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthServices.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly ICacheService _cache;
        public TokenService(IConfiguration config, ICacheService cache) { 
            _config = config;
            _cache = cache;
        }

        public string GenerateAccessToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("Login", user.Login),
                new Claim("userId", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config.GetValue<string>("AppSettings:JwtSecretKey")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _config.GetValue<string>("AppSettings:Issuer"),
                audience: _config.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(3),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
        public string GenerateRefreshToken(User user)
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            var refreshToken = Convert.ToBase64String(randomNumber);

            SaveRefreshToken(user.Login, refreshToken);

            return refreshToken;
        }
        public bool ValidateAccess(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config.GetValue<string>("AppSettings:JwtSecretKey")!);

            try
            {
                tokenHandler.ValidateToken(accessToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out _);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public TokensPair? RefreshTokens(TokensPair tokens)
        {
            var login = GetLoginClaimFromToken(tokens.AccessToken);
            var refreshKey = $"refresh_{login}";
            var oldRefresh = _cache.GetData<string>(refreshKey);

            if(tokens.RefreshToken == oldRefresh)
            {
                var userKey = $"users_{login}";
                var user = _cache.GetData<User>(userKey);
                var newTokens = new TokensPair(GenerateAccessToken(user), GenerateRefreshToken(user));
                return newTokens;
            }
            return null;
        }
        private void SaveRefreshToken(string login, string refreshToken)
        {
            var key = $"refresh_{login}";
            var oldRefresh = _cache.GetData<string>(key);
            if(oldRefresh == null) 
            {
                _cache.SetData(key, refreshToken, 7);
                return;
            }
            _cache.RemoveData(key);
            _cache.SetData(key, refreshToken, 7);
        }
        private string? GetLoginClaimFromToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(accessToken);

            return jwtToken.Claims.FirstOrDefault(c => c.Type == "Login")?.Value;
        }
    }
}
