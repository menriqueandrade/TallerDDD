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
            cuenta.NombreCuenta = "manuel";
            cuenta.Ciudad = "valledupar";
            cuenta.SaldoCuenta = 0;
        }

        //El valor de la consignación debe ser mayor a 0
        [Test]
        public void ConsignacionNegativa()
        {         
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => cuenta.Consignar(-20000, "valledupar"));
            Assert.AreEqual(ex.Message, "La consignacion debe de ser mayor a 0");
        }

        // La consignación inicial debe ser mayor o igual a 50 mil pesos
        [Test]
        public void ConsignacionInicialInCorrecta()
        {         
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => cuenta.Consignar(49900, "valledupar"));
            Assert.AreEqual(ex.Message, "La consignacion inicial debe ser igual o mayor a 50.000");

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
            cuenta.Consignar(50000, "valledupar");
            cuenta.Consignar(49950, "valledupar");
            Assert.AreEqual(cuenta.SaldoCuenta, 99950);
        }

        //La consignación nacional (a una cuenta de otra ciudad) tendrá un costo de $10 mil pesos
        [Test]
        public void ConsignacionPosteriorInicialCorrecta2()
        {
            cuenta.Consignar(50000, "valledupar");
            cuenta.Consignar(49950, "Bogota");
            Assert.AreEqual(cuenta.SaldoCuenta, 89950);
        }

        //Historia 2 desde retiro en adelante
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

        //Del cuarto retiro en adelante del mes tendrán un valor de 5 mil peso
        [Test]
        public void RetiroCorrectoSumando5000()
        {
            cuenta.SaldoCuenta = 100000;
            cuenta.Retirar(20000, "valledupar");
            cuenta.Retirar(20000, "valledupar");
            cuenta.Retirar(20000, "valledupar");
            cuenta.Retirar(7000, "valledupar");
            Assert.AreEqual(cuenta.SaldoCuenta, 28000);
        }

        //El saldo mínimo de la cuenta deberá ser de 20 mil pesos. 
        [Test]
        public void RetiroInCorrecto()
        {
            cuenta.SaldoCuenta = 50000;
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => cuenta.Retirar(40000, "valledupar"));
            Assert.AreEqual(ex.Message, "No se puede retirar esa cantidad de dinero");
        }
        //El saldo mínimo de la cuenta deberá ser de 20 mil pesos. 
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
