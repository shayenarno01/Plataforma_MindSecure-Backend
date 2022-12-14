using MessagePack;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_MindSecure.Models
{
    public partial class Usuario
    {
        
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public string Usuario1 { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Rol { get; set; } = null!;
        public string Status { get; set; } = null!;
    }
}
