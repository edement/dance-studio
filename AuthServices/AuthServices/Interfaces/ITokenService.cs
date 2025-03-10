using AuthServices.DTOs;
using AuthServices.Models;

namespace AuthServices.Interfaces
{
    public interface ITokenService
    {
        TokensResponse GenerateTokens(User user);
    }
}
