using AuthServices.DTOs;
using AuthServices.Models;

namespace AuthServices.Interfaces
{
    public interface IJwtService
    {
        public string GenerateAccess(User user);
    }
}
