using System.ComponentModel.DataAnnotations;

namespace Backend_MindSecure.Models
{
    public class UserRegister
    {
        public class UserRegisterRequest
        {
            [Required, EmailAddress]
            public string Email { get; set; } = string.Empty;
            [Required, MinLength(6, ErrorMessage = "Please enter at least 6 characters, dude!")]
            public string Clave { get; set; } = string.Empty;
            [Required, Compare("Clave")]
            public string ConfirmarClave { get; set; } = string.Empty;            
            public string Usuario1 { get; set; } = string.Empty;
            [Required, Phone]
            public string Telefono { get; set; } = string.Empty;
            public string Rol { get; set; } = string.Empty;
            public string Status { get; set; } = string.Empty;
        }
    }
}
