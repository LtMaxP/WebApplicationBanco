using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplicationBanco.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Registros = new HashSet<Registro>();
        }

        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public int? IdCuenta { get; set; }

        public virtual Cuentum IdCuentaNavigation { get; set; }
        public virtual ICollection<Registro> Registros { get; set; }
    }
}
