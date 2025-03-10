using System.Net.Http.Headers;

namespace BackendAPI.Services
{
    public class AuthValidationService
    {
        private readonly HttpClient _httpClient;

        public AuthValidationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ValidateTokenAsync(string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5214/validate");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
    }
}
