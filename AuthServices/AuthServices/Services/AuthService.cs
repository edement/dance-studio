using AuthServices.Data.Interfaces;
using AuthServices.Models;
using AuthServices.Models.DTOs;
using AuthServices.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;
using System.Text.Json;

namespace AuthServices.Services
{
    public class AuthService : IAuthService
    {
        private readonly ICacheService _cacheService;
        private readonly ITokenService _tokenService;
        private readonly HttpClient _httpClient;
        public AuthService(ICacheService cacheService, ITokenService tokenService, HttpClient httpClient)
        {
            _cacheService = cacheService;
            _tokenService = tokenService;
            _httpClient = httpClient;
        }

        public async Task<TokensPair?> Login(AuthRequest request)
        {
            TokensPair tokens;
            var user = await GetUser(request.Login);
            if(user != null) 
            {
                tokens = new TokensPair(_tokenService.GenerateAccessToken(user), _tokenService.GenerateRefreshToken(user));
                return tokens;
            }

            return null;
        }

        public bool ValidateAccess(string token)
        {
            var result = _tokenService.ValidateAccess(token);
            return result;
        }

        public TokensPair? RefreshTokens(TokensPair tokens)
        {
            var newTokens = _tokenService.RefreshTokens(tokens);
            return newTokens;
        }

        private async Task<User?> GetUser(string login)
        {
            try
            {
                // cache
                var searchKey = $"users_{login}";
                var user = _cacheService.GetData<User?>(searchKey);
                if (user != null) { return user; }

                // db
                string url = $"http://localhost:5271/user/{login}";
                user = await _httpClient.GetFromJsonAsync<User>(url);

                if (user == null) { return null; }

                _cacheService.SetData(searchKey, user, 24 * 60);

                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while user request: {e.Message}");
                return null;
            }
        }
    }
}