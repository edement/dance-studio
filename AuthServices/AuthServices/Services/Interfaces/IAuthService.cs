using AuthServices.Models.DTOs;

namespace AuthServices.Services.Interfaces
{
    public interface IAuthService
    {
        Task<TokensPair?> Login(AuthRequest request);
        bool ValidateAccess(string token);
        TokensPair? RefreshTokens(TokensPair tokens);
    }
}
