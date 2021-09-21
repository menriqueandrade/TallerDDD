using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Avance
    {
        public string NumeroTarjeta { get; set; }
        public double ValorAvance { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public string mes { get; set; }
        public string año { get; set; }

        public Avance()
        {

        }
    }
}
