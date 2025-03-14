using BackendAPI.DTOs;
using BackendAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BackendAPI.Interfaces
{
    public interface IUserService
    {
        Task CreateAsync(RegisterDTO request);
        Task<List<User>> GetAllAsync();
        Task<User?> GetByLoginAsync(string login);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(string login, [FromBody] JsonElement request);
        Task<List<Class>> GetMyEnrollmentsAsync(Guid userId);
    }
}
