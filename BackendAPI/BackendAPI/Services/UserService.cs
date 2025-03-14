using BackendAPI.DTOs;
using BackendAPI.Interfaces;
using BackendAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BackendAPI.Services
{
    internal class UserService(IUserRepository _userRepository) : IUserService
    {
        public async Task CreateAsync(RegisterDTO request)
        {
            var user = new User
            {
                Id = new Guid(),
                Login = request.Login,
                HashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            await _userRepository.CreateAsync(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public Task<User?> GetByLoginAsync(string login)
        {
            var user = _userRepository.GetByLoginAsync(login);
            if(user == null) { throw new Exception("User Not Found"); }

            return user;
        }

        public async Task UpdateAsync(string login, [FromBody] JsonElement request)
        {
            var userNew = JsonSerializer.Deserialize<User>(request.GetRawText());
            if(userNew == null) { throw new Exception("No New Info"); }
            var userEntity = await _userRepository.GetByLoginAsync(login);

            if (userNew.HashedPassword != "")
            {
                userEntity.HashedPassword = BCrypt.Net.BCrypt.HashPassword(userNew.HashedPassword);
            }
            if (userNew.Login != "")
            {
                userEntity.Login = userNew.Login;
            }

            await _userRepository.UpdateAsync(userEntity);
        }
        public async Task<List<Class>> GetMyEnrollmentsAsync(Guid userId)
        {
            var enrollments = await _userRepository.GetMyEnrollmentsAsync(userId);
            return enrollments;
        }
    }
}
