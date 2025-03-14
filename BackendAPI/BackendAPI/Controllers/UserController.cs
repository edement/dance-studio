using BackendAPI.DTOs;
using BackendAPI.Models;
using BackendAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [ApiController]
    public class UserController(IUserService _userService) : ControllerBase
    {
        /*[HttpGet("users")]
        public async Task<List<User>> GetAllAsync()
        {
            return await _userService.GetAllAsync();
        }*/
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
        [HttpGet("enrollments")]
        public async Task<List<Class>> GetMyEnrollmentsAsync()
        {
            var userId = User.FindFirst("userId")?.Value;
            var enrollments = await _userService.GetMyEnrollmentsAsync(Guid.Parse(userId));
            return enrollments;
        }
    }
}
