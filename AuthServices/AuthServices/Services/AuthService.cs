using AuthServices.DTOs;
using AuthServices.Interfaces;
using AuthServices.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AuthServices.Services
{
    public class AuthService(IUserRepository _userRepository, 
        IRedisCacheService _cache, 
        ITokenService _tokenService,
        ITokenRepository _tokenRepository
        ) : IAuthService
    {
        public async Task<User> GetUserByLogin(string login) // убрать из интерфейса + сделать private
        {
            var cachingKey = $"users:{login}";

            var user = _cache.GetData<User>(cachingKey);
            if (user != null) { return user; }

            user = await _userRepository.GetByLoginAsync(login);
            if(user == null) { throw new Exception("User Not Found"); }

            _cache.SetData(cachingKey, user, 5 * 60);

            return user;
        }

        public async Task<TokensResponse> Login(UserDto request)
        {
            // проверка пользователя
            var user = await GetUserByLogin(request.Login);
            if (user == null) { return default; }

            if (BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.HashedPassword, BCrypt.Net.HashType.SHA256)) { throw new Exception("Wrong Login or Password"); }

            // генерация новых токенов
            var tokens = _tokenService.GenerateTokens(user);

            // проверка refresh токена
            var cachingKey = $"refresh:{user.Login}";
            var refreshToken = _tokenRepository.GetRefreshToken(cachingKey);
            if(refreshToken != null) { _tokenRepository.DelRefreshToken(cachingKey); }
            _tokenRepository.SaveRefreshToken(cachingKey, tokens.RefreshToken);

            return tokens;
        }
    }
}
