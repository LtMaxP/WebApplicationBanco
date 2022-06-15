using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplicationBanco.Models
{
    public partial class Registro
    {
        public int IdRegistro { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public string Accion { get; set; }
        public int IdUsuario { get; set; }

        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
