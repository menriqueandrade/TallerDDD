using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Domain.Base;

namespace Domain.Entities
{
    public class CuentaCorriente : Entity<int>, ICuenta
    {
        public string NumeroCuenta { get; set ; }
        public string NombreCuenta { get; set ; }
        public double SaldoCuenta { get; set; }
        public double ValorSobregiro { get; set; }
        public double Ciudad { get; set; }


        public List<Retiro> retiros { get; set; }
        public List<Consignacion> consignaciones { get; set; }
        public List<MovimientoFinanciero> movimientoFinancieros { get; set; }

        public CuentaCorriente()
        {
            retiros = new List<Retiro>();
            consignaciones = new List<Consignacion>();
            movimientoFinancieros = new List<MovimientoFinanciero>();
        }

        public void Consignar(double valor, string ciudad)
        {
            throw new NotImplementedException();
        }

        public void Retirar(double valor, string ciudad)
        {
            throw new NotImplementedException();
        }
    }
}
