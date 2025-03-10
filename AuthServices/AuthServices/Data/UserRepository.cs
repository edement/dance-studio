using AuthServices.Interfaces;
using AuthServices.Models;

namespace AuthServices.Interfaces
{
    public class UserRepository : IUserRepository
    {
        private readonly HttpClient _httpClient;

        public UserRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<User?> GetByLoginAsync(string login)
        {
            var response = await _httpClient.GetFromJsonAsync<User?>($"http://localhost:5271/user/{login}");
            return response;
        }
    }
}
