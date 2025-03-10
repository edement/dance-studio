using AuthServices.Models;

namespace AuthServices.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByLoginAsync(string login);
    }
}
