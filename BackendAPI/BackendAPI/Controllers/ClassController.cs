using BackendAPI.DTOs;
using BackendAPI.Models;
using BackendAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Security.Permissions;

namespace BackendAPI.Controllers
{
    [ApiController]
    [Authorize]
    public class ClassController(IClassService _classService) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet("classes")]
        public async Task<List<Class>?> GetAllAsync()
        {
            return await _classService.GetAllAsync();
        }
        [AllowAnonymous]
        [HttpGet("classes/{classId}")]
        public async Task<Class?> GetByIdAsync(Guid classId)
        {
            return await _classService.GetByIdAsync(classId);
        }
        [HttpPost("class")]
        public async Task<IActionResult> CreateAsync([FromBody] JsonElement request)
        {
            var coachId = User.FindFirst("userId")?.Value;
            if(coachId == null) { return Unauthorized("Please Login"); }

            var date = request.GetProperty("date").GetString();

            await _classService.CreateAsync(DateTime.Parse(date), Guid.Parse(coachId));
            return Created();
        }
        [HttpDelete("class/{classId}")]
        public async Task<IActionResult> DeleteAsync(Guid classId)
        {
            await _classService.DeleteAsync(classId);
            return Ok(Content("Successful Delete"));
        }
        [HttpPatch("class/{classId}")]
        public async Task<IActionResult> UpdateAsync(Guid classId, [FromBody] JsonElement request)
        {
            if (request.ToString() == String.Empty) { return new StatusCodeResult(400); }
            await _classService.UpdateAsync(classId, request);
            return Ok(Content("Successful Editing"));
        }
        [HttpPost("class/join/{classId}")]
        public async Task<IActionResult> JoinClass(Guid classId)
        {
            var userId = User.FindFirst("userId")?.Value;
            var enrollment = new EnrollmentDTO()
            {
                UserId = Guid.Parse(userId),
                ClassId = classId
            };

            var result = await _classService.JoinClass(enrollment);

            if(result) { return Ok("User successfully enrolled to the class."); }

            return BadRequest("Enrollment failed. Either the user or the class doesn't exist, or the user is already enrolled.");
        }
    }
}
