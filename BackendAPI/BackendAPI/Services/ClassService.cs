using BackendAPI.Data;
using BackendAPI.DTOs;
using BackendAPI.Models;
using BackendAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BackendAPI.Services
{
    public class ClassService(IRepository<Class> classRepository) : IClassService
    {
        public async Task CreateAsync(ClassDTO request)
        {
            var classEntity = new Class()
            {
                Id = new Guid(),
                Date = request.Date,
                CoachId = request.CoachId
            };

            await classRepository.CreateAsync(classEntity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await classRepository.DeleteAsync(id);
        }

        public async Task<List<Class>> GetAllAsync()
        {
            return await classRepository.GetAllAsync();
        }

        public async Task<Class?> GetByIdAsync(Guid id)
        {
            return await classRepository.GetByIdAsync(id);
        }
        
        public async Task UpdateAsync(Guid id, [FromBody] JsonElement request)
        {
            var classNew = JsonSerializer.Deserialize<Class>(request.GetRawText());
            var classEntity = await classRepository.GetByIdAsync(id);

            if(classNew.Date != DateTime.MinValue)
            {
                classEntity.Date = classNew.Date;
            }

            if (classNew.CoachId != Guid.Empty)
            {
                classEntity.CoachId = classNew.CoachId;
            }

            await classRepository.UpdateAsync(classEntity);
        }
    }
}
