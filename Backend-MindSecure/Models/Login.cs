using System.ComponentModel.DataAnnotations;

namespace Backend_MindSecure.Models
{
    public class Login
    {
        public class UserLoginRequest
        {
            [Required, EmailAddress]
            public string Email { get; set; } = string.Empty;
            [Required]
            public string Clave { get; set; } = string.Empty;
        }
    }
}
