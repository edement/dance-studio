using AuthServices.DTOs;
using AuthServices.Interfaces;
using AuthServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthServices.Controllers
{
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpGet("/user")]
        public async Task<IActionResult> GetUser([FromBody] UserDto request)
        {
            var user = await authService.GetUserByLogin(request.Login);
            return Ok(user);
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] UserDto request)
        {
            var tokens = await authService.Login(request);

            return Ok(tokens);
        }

        [HttpGet("/validate")]
        public async Task<IActionResult> ValidateToken([FromHeader] string Authorization)
        {
            if (string.IsNullOrEmpty(Authorization) || !Authorization.StartsWith("Bearer "))
                return Unauthorized(new { message = "Missing or invalid token" });

            var token = Authorization["Bearer ".Length..]; // Убираем "Bearer "

            var principal = _jwtService.ValidateJwtToken(token);
            if (principal == null)
                return Unauthorized(new { message = "Invalid token" });

            return Ok(new { isValid = true });
        }


        [HttpGet("/ping")]
        public string Ping()
        {
            return "Pong";
        }
    }
}
