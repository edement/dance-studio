using BackendAPI.DTOs;
using BackendAPI.Models;

namespace BackendAPI.Interfaces
{
    public interface IClassRepository : IRepository<Class>
    {
        Task<bool> JoinClass(EnrollmentDTO enrollment);
    }
}
