using System;
using System.Collections.Generic;

namespace Backend_MindSecure.Models
{
    public partial class Grupo
    {
        public int Idgrupo { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public string Comunidad { get; set; } = null!;
    }
}
