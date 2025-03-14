using BackendAPI.DTOs;
using BackendAPI.Services;
using System.Net.Http;
using System.Text.Json;

namespace BackendAPI.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HttpClient _httpClient;

        public AuthMiddleware(RequestDelegate next, HttpClient httpClient)
        {
            _next = next;
            _httpClient = httpClient;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if(token != null)
            {
                var isValid = await ValidateToken(token);
                if(!isValid)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }
            }

            await _next(context);
        }

        private async Task<bool> ValidateToken(string token)
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5214/api/auth/validate", token);
            if (!response.IsSuccessStatusCode) return false;

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<TokenValidationResponse>(json);
            return result?.isValid ?? false;
        }
    }
}
