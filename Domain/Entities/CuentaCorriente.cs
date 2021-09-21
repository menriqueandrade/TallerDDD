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
        public string Ciudad { get; set; }
        public CreditoPreAprobado credito { get; set; }

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
            if (valor <= 0)
            {
                throw new InvalidOperationException("La consignacion debe de ser mayor a 0");
            }
            else
            {
                //creo el bjeto consignacion
                Consignacion consignacion = new Consignacion();
                consignacion.Cuenta = NumeroCuenta;
                consignacion.FechaMovimiento = DateTime.Today.ToString();
                consignacion.ValorConsignacion = valor;

                //veo cuantas consignaciones llevo
                int NumeroConsignasiones = 0;
                foreach (var item in consignaciones)
                {
                    NumeroConsignasiones += 1;
                }

                if (NumeroConsignasiones == 0)
                {
                    if (valor >= 100000)
                    {
                        consignaciones.Add(consignacion);
                        SaldoCuenta = SaldoCuenta + valor;
                    }
                    else
                    {
                        throw new InvalidOperationException("La consignacion inicial debe ser igual o mayor a 100.000");
                    }
                }
                else
                {
                    consignaciones.Add(consignacion);
                    SaldoCuenta = SaldoCuenta + valor;
                    GuardarMovimieto("Consignacion cuenta corriente", consignacion.ValorConsignacion, 0, ciudad);
                }
            }
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
        public void Retirar(double valor, string ciudad)
        {
            throw new NotImplementedException();
        }
    }
}
