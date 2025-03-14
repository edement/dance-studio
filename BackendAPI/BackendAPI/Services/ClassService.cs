using BackendAPI.Data;
using BackendAPI.DTOs;
using BackendAPI.Models;
using BackendAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BackendAPI.Services
{
    public class ClassService(IClassRepository _classRepository) : IClassService
    {
        public async Task CreateAsync(ClassDTO request)
        {
            var classEntity = new Class()
            {
                Id = new Guid(),
                Date = request.Date,
                CoachId = request.CoachId
            };

            await _classRepository.CreateAsync(classEntity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _classRepository.DeleteAsync(id);
        }

        public async Task<List<Class>> GetAllAsync()
        {
            return await _classRepository.GetAllAsync();
        }

        public async Task<Class?> GetByIdAsync(Guid id)
        {
            return await _classRepository.GetByIdAsync(id);
        }
        
        public async Task UpdateAsync(Guid id, [FromBody] JsonElement request)
        {
            var classNew = JsonSerializer.Deserialize<Class>(request.GetRawText());
            var classEntity = await _classRepository.GetByIdAsync(id);

            if(classNew.Date != DateTime.MinValue)
            {
                classEntity.Date = classNew.Date;
            }

            if (classNew.CoachId != Guid.Empty)
            {
                classEntity.CoachId = classNew.CoachId;
            }

            await _classRepository.UpdateAsync(classEntity);
        }
        public async Task<bool> JoinClass(EnrollmentDTO enrollment)
        {
            var result = await _classRepository.JoinClass(enrollment);
            return result;
        }
    }
}
