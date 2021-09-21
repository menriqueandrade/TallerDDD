using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Domain.test
{
    class TestCuentaCorriente
    {
        CuentaCorriente cuenta;
        [SetUp]
        public void Setup()
        {
            cuenta = new CuentaCorriente();
            cuenta.NumeroCuenta = "1234";
            cuenta.NombreCuenta = "fabian";
            cuenta.Ciudad = "valledupar";
            cuenta.SaldoCuenta = 0;

            CreditoPreAprobado credito = new CreditoPreAprobado();
            credito.Cuenta = cuenta.NumeroCuenta;
            credito.CupoSobregiro = 250000;
            credito.FechaAprobacion = DateTime.Today;

            cuenta.credito = credito;
        }


        [Test]
        public void ConsignacionNegativa()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => cuenta.Consignar(-20000, "valledupar"));
            Assert.AreEqual(ex.Message, "La consignacion debe de ser mayor a 0");
        }

        [Test]
        public void ConsignacionInicialInCorrecta()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => cuenta.Consignar(49900, "valledupar"));
            Assert.AreEqual(ex.Message, "La consignacion inicial debe ser igual o mayor a 100.000");

        }

        [Test]
        public void ConsignacionInicialCorrecta()
        {
            cuenta.Consignar(100000, "valledupar");
            Assert.AreEqual(cuenta.SaldoCuenta, 100000);
        }

        [Test]
        public void ConsignacionPosteriorInicialCorrecta()
        {
            cuenta.Consignar(100000, "valledupar");
            cuenta.Consignar(49950, "valledupar");
            Assert.AreEqual(cuenta.SaldoCuenta, 149950);
        }
    }
}
