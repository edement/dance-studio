using AuthServices.DTOs;
using AuthServices.Models;

namespace AuthServices.Interfaces
{
    public interface IAuthService
    {
        Task<User> GetUserByLogin(string login);
        Task<TokensResponse> Login(UserDto request);
    }
}
