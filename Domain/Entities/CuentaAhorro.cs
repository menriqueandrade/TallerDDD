using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Domain.Base;

namespace Domain.Entities
{
    public class CuentaAhorro : Entity<int>, ICuenta
    {
        public string NumeroCuenta { get; set ; }
        public string NombreCuenta { get ; set ; }
        public double SaldoCuenta { get; set; }
        public string Ciudad { get; set; }

        public List<Retiro> retiros { get; set; }
        public List<Consignacion> consignaciones { get; set; }
        public List<MovimientoFinanciero> movimientoFinancieros { get; set; }

        public CuentaAhorro()
        {
            retiros = new List<Retiro>();
            consignaciones = new List<Consignacion>();
            movimientoFinancieros = new List<MovimientoFinanciero>();
        }

        public void Consignar(double valor, string ciudad)
        {

            if (ciudad != Ciudad)
            {
                valor = valor - 10000;
            }

            Consignacion consignacion = new Consignacion();
            consignacion.Cuenta = NumeroCuenta;
            consignacion.FechaMovimiento = DateTime.Today.ToString();
            consignacion.ValorConsignacion = valor;

            if (valor <= 0)
            {
                throw new InvalidOperationException("La consignacion debe de ser mayor a 0");

            }
            else
            {
                //veo cuantas consignaciones llevo
                int NumeroConsignasiones = 0;
                foreach (var item in consignaciones)
                {
                    NumeroConsignasiones += 1;
                }

                if (NumeroConsignasiones == 0)
                {
                    if (valor >= 50000)
                    {
                        consignaciones.Add(consignacion);
                        SaldoCuenta = SaldoCuenta + valor;
                        GuardarMovimieto("Consignacion cuenta de ahorro", consignacion.ValorConsignacion, 0, ciudad);
                    }
                    else
                    {
                        throw new InvalidOperationException("La consignacion inicial debe ser igual o mayor a 50.000");
                    }
                }
                else
                { 
                    consignaciones.Add(consignacion);
                    SaldoCuenta = SaldoCuenta + valor;
                    GuardarMovimieto("Consignacion cuenta de ahorro",consignacion.ValorConsignacion,0,ciudad);
                }
            }
        }

        public void GuardarMovimieto(string tipo, double valorRetiro, double valorConsignacion,string ciudad)
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
            if (valor < 0)
            {
                throw new InvalidOperationException("El retiro debe de ser mayor a 0");

            }
            else
            {
                DateTime fechaActual = DateTime.Today;
                Retiro retiro = new Retiro();
                retiro.año = fechaActual.Year.ToString();
                retiro.mes = fechaActual.Month.ToString();
                retiro.Cuenta = NumeroCuenta;
                retiro.FechaMovimiento = fechaActual;

                //veo si van mas de 3 retiros
                int numeroRetiros = 0;
                foreach (var item in this.retiros)
                {
                    if (item.mes.Equals(retiro.mes) && item.año.Equals(retiro.año))
                    {
                        numeroRetiros += 1;
                    }
                }

                //si tengo mas de 3 retiros sumo 5000
                if (numeroRetiros >= 3)
                {
                    valor = valor + 5000;
                }

                if ((SaldoCuenta - valor) <= 20000)
                {
                    throw new InvalidOperationException("No se puede retirar esa cantidad de dinero");
                }
                else
                {
                    SaldoCuenta = SaldoCuenta - valor;
                    this.retiros.Add(retiro);
                    GuardarMovimieto("Retiro cuenta de ahorro", 0, retiro.ValorRetiro, ciudad);
                }
            }
        }
    }
}
