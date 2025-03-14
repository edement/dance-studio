using BackendAPI.DTOs;
using BackendAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BackendAPI.Interfaces
{
    public interface IClassService
    {
        Task CreateAsync(ClassDTO request);
        Task<List<Class>> GetAllAsync();
        Task<Class?> GetByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Guid id, [FromBody] JsonElement request);
        Task<bool> JoinClass(EnrollmentDTO enrollment);
    }
}
