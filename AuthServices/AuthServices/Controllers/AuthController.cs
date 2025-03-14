using AuthServices.Models.DTOs;
using AuthServices.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthServices.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService) { _authService = authService; }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthRequest request)
        {
            var token = await _authService.Login(request);
            if(token == null) { return Unauthorized("Wrong Login or Password"); }
            return Ok(token);
        }
        [HttpPost("validate")]
        public IActionResult ValidateAccess([FromBody] string token)
        {
            if (string.IsNullOrEmpty(token))
                return BadRequest(new { isValid = false });

            bool isValid = _authService.ValidateAccess(token);
            return Ok(new { isValid });
        }
        [HttpPost("refresh")]
        public IActionResult RefreshTokens([FromBody] TokensPair tokens)
        {
            var newTokens = _authService.RefreshTokens(tokens);
            if(newTokens == null)
            {
                return Unauthorized("Please Login");
            }
            return Ok(newTokens);
        }
    }
}
