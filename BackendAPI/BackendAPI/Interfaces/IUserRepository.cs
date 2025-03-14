﻿using BackendAPI.Models;

namespace BackendAPI.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByLoginAsync(string email);
        Task<List<Class>> GetMyEnrollmentsAsync(Guid id);
    }
}
