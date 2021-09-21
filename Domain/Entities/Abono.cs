using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Abono
    {
        public string Numerotrajeta { get; set; }
        public double ValorAbonado { get; set; }
        public string FechaMovimiento { get; set; }

        public Abono()
        {

        }
    }
}
