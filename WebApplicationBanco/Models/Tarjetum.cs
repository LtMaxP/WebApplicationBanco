﻿using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplicationBanco.Models
{
    public partial class Tarjetum
    {
        public Tarjetum()
        {
            Cuenta = new HashSet<Cuentum>();
        }

        public int IdTarjeta { get; set; }
        public long? NumeroTarjeta { get; set; }
        public int? Pin { get; set; }

        public virtual ICollection<Cuentum> Cuenta { get; set; }
    }
}
