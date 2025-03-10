using BackendAPI.DTOs;
using BackendAPI.Models;
using BackendAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BackendAPI.Controllers
{
    [ApiController]
    public class ClassController(IClassService classService) : ControllerBase
    {
        [HttpGet]
        [Route("classes")]
        public async Task<List<Class>?> GetAllAsync()
        {
            return await classService.GetAllAsync();
        }
        [HttpGet]
        [Route("classes/${id}")]
        public async Task<Class?> GetByIdAsync(Guid id)
        {
            return await classService.GetByIdAsync(id);
        }
        [HttpPost]
        [Route("class")]
        public async Task<IActionResult> CreateAsync(ClassDTO request)
        {
            await classService.CreateAsync(request);
            return Created();
        }
        [HttpDelete]
        [Route("class/${id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await classService.DeleteAsync(id);
            return Ok(Content("Successful Delete"));
        }
        [HttpPatch]
        [Route("class/${id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] JsonElement request)
        {
            if (request.ToString() == String.Empty) { return new StatusCodeResult(400); }
            await classService.UpdateAsync(id, request);
            return Ok(Content("Successful Editing"));
        }
    }
}
