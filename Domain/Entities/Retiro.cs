using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Retiro
    {
        public string Cuenta { get; set; }
        public double ValorRetiro { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public string mes { get; set; }
        public string año { get; set; }

        public Retiro()
        {

        }
    }
}
