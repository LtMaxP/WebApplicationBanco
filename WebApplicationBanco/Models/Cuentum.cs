using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplicationBanco.Models
{
    public partial class Cuentum
    {
        public Cuentum()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int IdCuenta { get; set; }
        public decimal? Monto { get; set; }
        public int? IdTarjeta { get; set; }

        public virtual Tarjetum IdTarjetaNavigation { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
