using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Domain.Base;

namespace Domain.Entities
{
    public class CreditoPreAprobado : Entity<int>
    {
        public double CupoSobregiro { get; set; }
        public string Cuenta { get; set; }
        public DateTime FechaAprobacion { get; set; }

        public CreditoPreAprobado()
        {

        }
    }
}
