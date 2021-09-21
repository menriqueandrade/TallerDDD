using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Domain.Base;

namespace Domain.Entities
{
    public class CDT : Entity<int>, ICuenta
    {
        public string NumeroCuenta { get; set; }
        public string NombreCuenta { get; set; }
        public double SaldoCuenta { get; set; }
        public int plazo { get; set; }
        public DateTime FechaApertura { get; set; }
        public DateTime FechaCierre { get; set; }
        public List<Retiro> retiros { get; set; }
        public Consignacion consignacion { get; set; }
        public List<MovimientoFinanciero> movimientoFinancieros { get; set; }
      
        public CDT()
        {
            retiros = new List<Retiro>();
            consignacion = null;
            movimientoFinancieros = new List<MovimientoFinanciero>();
        }

        public void Consignar(double valor, string ciudad)
        {
            if (valor <= 0)
            {
                throw new InvalidOperationException("La consignacion debe de ser mayor a 0");

            }
            else
            {
                if (valor < 1000000)
                {
                    throw new InvalidOperationException("El valor minimo de la consignacion es de 1.000.000");
                }
                else
                {
                    if (consignacion != null)
                    {
                        throw new InvalidOperationException("solo se puede realizar una consignacion");

                    }
                    else
                    {
                        consignacion = new Consignacion();
                        consignacion.Cuenta = NumeroCuenta;
                        consignacion.FechaMovimiento = DateTime.Today.ToString();
                        consignacion.ValorConsignacion = valor;

                        SaldoCuenta = SaldoCuenta + valor;
                        GuardarMovimieto("Consignacion CDT", consignacion.ValorConsignacion, 0, ciudad);
                    }
                }
            }
        }

        public void Retirar(double valor, string ciudad)
        {
            
        }

        public void GuardarMovimieto(string tipo, double valorRetiro, double valorConsignacion, string ciudad)
        {
            MovimientoFinanciero movimiento = new MovimientoFinanciero();
            movimiento.City = ciudad;
            movimiento.FechaMovimiento = DateTime.Today;
            movimiento.TipoMovimiento = tipo;
            movimiento.ValorConsignacion = valorConsignacion;
            movimiento.ValorRetiro = valorRetiro;

            this.movimientoFinancieros.Add(movimiento);
        }
    }
}
