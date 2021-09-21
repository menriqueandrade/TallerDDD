using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Domain.Base;

namespace Domain.Entities
{
    public class TarjetaCredito : Entity<int>
    {
        public string NumeroTarjeta { get; set; }
        public string CVV { get; set; }
        public string FechaExpedicion { get; set; }
        public double CupoTargeta { get; set; }
        public double SaldoTargeta { get; set; }

        public List<Avance> avances { get; set; }
        public List<Abono> abonos { get; set; }
        public List<MovimientoFinanciero> movimientoFinancieros { get; set; }

        public TarjetaCredito()
        {
            avances = new List<Avance>();
            abonos = new List<Abono>();
            movimientoFinancieros = new List<MovimientoFinanciero>();
        }

        public void Abonar(double valor)
        {
            if (valor <= 0)
            {
                throw new InvalidOperationException("La consignacion debe de ser mayor a 0");
            }
            else
            {
                if (valor > SaldoTargeta)
                {
                    throw new InvalidOperationException("El valor maximo que puede abonar es "+SaldoTargeta);
                }
                else
                {
                    CupoTargeta = CupoTargeta + valor;
                    SaldoTargeta = SaldoTargeta - valor;

                    Abono abono = new Abono();
                    abono.FechaMovimiento = DateTime.Today.ToString();
                    abono.Numerotrajeta = NumeroTarjeta;
                    abono.ValorAbonado = valor;

                    abonos.Add(abono);

                    GuardarMovimieto("Abono tarjeta de credito", abono.ValorAbonado, 0, null);

                }
            }
        }

        public void Avance(double valor,string ciudad)
        {
            if (valor <= 0)
            {
                throw new InvalidOperationException("El avance debe de ser mayor a 0");
            }
            else
            {
                if (valor > CupoTargeta)
                {
                    throw new InvalidOperationException("El valor maximo del avance es " + CupoTargeta);
                }
                else
                {
                    CupoTargeta = CupoTargeta - valor;
                    SaldoTargeta = SaldoTargeta + valor;

                    Avance avance = new Avance();
                    avance.FechaMovimiento = DateTime.Today;
                    avance.año = DateTime.Today.Year.ToString();
                    avance.mes = DateTime.Today.Month.ToString();
                    avance.ValorAvance = valor;

                    this.avances.Add(avance);
                    GuardarMovimieto("Avance tarjeta de credito", avance.ValorAvance, 0, ciudad);

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
