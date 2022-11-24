using System;
using System.Collections.Generic;

namespace Backend_MindSecure.Models
{
    public partial class Comunidad
    {
        public int Idcomunidad { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Tipo { get; set; } = null!;
    }
}
