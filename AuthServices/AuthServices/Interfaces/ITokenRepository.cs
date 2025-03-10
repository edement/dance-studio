using AuthServices.Models;

namespace AuthServices.Interfaces
{
    public interface ITokenRepository
    {
        void SaveRefreshToken(string refreshToken, string data);
        string? GetRefreshToken(string key);
        void DelRefreshToken(string key);
    }
}
