﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class TarjetaCredito
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
                }
            }
        }
    }
}
