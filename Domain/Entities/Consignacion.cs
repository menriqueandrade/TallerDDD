using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Consignacion
    {
        public string Cuenta { get; set; }
        public double ValorConsignacion { get; set; }
        public string FechaMovimiento { get; set; }

        public Consignacion()
        {

        }
    }
}
