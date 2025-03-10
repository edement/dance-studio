using BackendAPI.Services;

namespace BackendAPI.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AuthValidationService _authValidation;

        public AuthMiddleware(RequestDelegate next, AuthValidationService authValidation)
        {
            _next = next;
            _authValidation = authValidation;
        }


    }
}
