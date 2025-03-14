using BackendAPI.DTOs;
using BackendAPI.Models;
using BackendAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [ApiController]
    public class UserController(IUserService _userService) : ControllerBase
    {
        [HttpGet("users")]
        public async Task<List<User>> GetAllAsync()
        {
            return await _userService.GetAllAsync();
        }
        [HttpGet("user/{login}")]
        public async Task<User?> GetByLoginAsync(string login)
        {
            var user = await _userService.GetByLoginAsync(login);
            return user;
        }
        [HttpPost("register")]
        public async Task<IActionResult> CreateAsync(RegisterDTO request)
        {
            await _userService.CreateAsync(request);
            return Created();
        }
        [HttpDelete("user/{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _userService.DeleteAsync(id);
            return Ok("Deleted");
        }
    }
}
