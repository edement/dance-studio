using AuthServices.Models;
using AuthServices.Models.DTOs;

namespace AuthServices.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken(User user);
        bool ValidateAccess(string accessToken);
        TokensPair? RefreshTokens(TokensPair tokens);
    }
}
