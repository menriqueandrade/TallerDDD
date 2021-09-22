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
        public double Interes { get; set; }
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

                if (DateTime.Compare(FechaCierre, fechaActual) > 0)
                {
                    throw new InvalidOperationException("Aun no se ha cumplido el plazo del CDT");
                }
                else
                {
                    //numero de retiros que llevo
                    int numeroRetiros = 0;
                    foreach (var item in retiros)
                    {
                        numeroRetiros += 1;
                    }

                    if (valor > SaldoCuenta)
                    {
                        throw new InvalidOperationException("La cantidad maxima a retirar es de "+SaldoCuenta);
                    }
                    else{
                        // si no hay retiros en el primer retiro liquido intereses
                        if (numeroRetiros == 0)
                        {
                            //sumo los intereses al saldo si no hay retiros
                            double Intereses = SaldoCuenta + ((SaldoCuenta * Interes) / 12) * plazo;
                            SaldoCuenta = SaldoCuenta + Intereses;
                            //retiro el valor, mas los intereses
                            valor = valor + Intereses;
                            SaldoCuenta = SaldoCuenta - valor;
                        }
                        else {
                            SaldoCuenta = SaldoCuenta - valor;
                        }

                        retiro.ValorRetiro = valor;
                        retiros.Add(retiro);
                        GuardarMovimieto("Retiro CDT", retiro.ValorRetiro, 0, ciudad);
                    }
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
    }
}
