using System.ComponentModel.DataAnnotations;

namespace BackendAPI.DTOs
{
    public class RegisterDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
