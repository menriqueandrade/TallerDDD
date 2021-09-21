using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Domain.Test
{
    class TestCuentaAhorro
    {
        
        CuentaAhorro cuenta ;
        [SetUp]
        public void Setup()
        {
            cuenta = new CuentaAhorro();

            cuenta.NumeroCuenta = "1234";
            cuenta.NombreCuenta = "fabian";
            cuenta.Ciudad = "valledupar";
            cuenta.SaldoCuenta = 0;
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
            Assert.AreEqual(ex.Message, "La consignacion inicial debe ser igual o mayor a 50.000");

        }

        [Test]
        public void ConsignacionInicialCorrecta()
        {
            cuenta.Consignar(50000, "valledupar");
            Assert.AreEqual(cuenta.SaldoCuenta, 50000);
        }

        [Test]
        public void ConsignacionPosteriorInicialCorrecta()
        {
            cuenta.Consignar(50000, "valledupar");
            cuenta.Consignar(49950, "valledupar");
            Assert.AreEqual(cuenta.SaldoCuenta, 99950);
        }

        [Test]
        public void ConsignacionPosteriorInicialCorrecta2()
        {
            cuenta.Consignar(50000, "valledupar");
            cuenta.Consignar(49950, "Bogota");
            Assert.AreEqual(cuenta.SaldoCuenta, 89950);
        }

        [Test]
        public void RetiroNegativo()
        {
            cuenta.SaldoCuenta = 50000;
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => cuenta.Retirar(-20000, "valledupar"));
            Assert.AreEqual(ex.Message, "El retiro debe de ser mayor a 0");
        }

        [Test]
        public void RetiroCorrecto()
        {
            cuenta.SaldoCuenta = 50000;
            cuenta.Retirar(20000, "valledupar");
            Assert.AreEqual(cuenta.SaldoCuenta, 30000);
        }

        [Test]
        public void RetiroCorrecto2()
        {
            cuenta.SaldoCuenta = 100000;
            cuenta.Retirar(20000, "valledupar");
            cuenta.Retirar(20000, "valledupar");
            cuenta.Retirar(20000, "valledupar");
            cuenta.Retirar(5000, "valledupar");
            Assert.AreEqual(cuenta.SaldoCuenta, 30000);
        }

        [Test]
        public void RetiroInCorrecto()
        {
            cuenta.SaldoCuenta = 50000;
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => cuenta.Retirar(40000, "valledupar"));
            Assert.AreEqual(ex.Message, "No se puede retirar esa cantidad de dinero");
        }

        [Test]
        public void RetiroInCorrecto2()
        {
            cuenta.SaldoCuenta = 100000;
            cuenta.Retirar(20000, "valledupar");
            cuenta.Retirar(20000, "valledupar");
            cuenta.Retirar(20000, "valledupar");
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => cuenta.Retirar(20000, "valledupar"));
            Assert.AreEqual(ex.Message, "No se puede retirar esa cantidad de dinero");
        }

    }
}
