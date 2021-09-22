using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Domain.test
{
    public class TestCDT
    {
        CDT cdt;
        [SetUp]
        public void Setup()
        {
            cdt = new CDT();

            cdt.NumeroCuenta = "1234";
            cdt.NombreCuenta = "manuel";
            cdt.SaldoCuenta = 0;
            cdt.FechaApertura = new DateTime(2021, 1, 1);
            cdt.FechaCierre = new DateTime(2021, 9, 1);
            cdt.plazo = 9;
            cdt.Interes = 0.06;
        }

        //HU 7. 
        [Test]
        public void ConsignacionNegativa()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => cdt.Consignar(-20000, "valledupar"));
            Assert.AreEqual(ex.Message, "La consignacion debe de ser mayor a 0");
        }

        [Test]
        public void ConsignacionInicialInCorrecta()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => cdt.Consignar(49900, "valledupar"));
            Assert.AreEqual(ex.Message, "El valor minimo de la consignacion es de 1.000.000");

        }

        [Test]
        public void ConsignacionInicialCorrecta()
        {
            cdt.Consignar(1000000, "valledupar");
            Assert.AreEqual(cdt.SaldoCuenta, 1000000);
        }

        [Test]
        public void ConsignacionInicialCorrecta2()
        {
            cdt.Consignar(2000000, "valledupar");
            Assert.AreEqual(cdt.SaldoCuenta, 2000000);
        }

        [Test]
        public void ConsignacionDosVeces()
        {
            cdt.Consignar(1000000, "valledupar");
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => cdt.Consignar(1000000, "valledupar"));
            Assert.AreEqual(ex.Message, "solo se puede realizar una consignacion");
        }
        //HU 8. 
        [Test]
        public void RetiroNegativo()
        {
            cdt.SaldoCuenta = 1000000;
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => cdt.Retirar(-20000, "valledupar"));
            Assert.AreEqual(ex.Message, "El retiro debe de ser mayor a 0");
        }

        [Test]
        public void RetiroCorrecto()
        {
            cdt.SaldoCuenta = 1000000;
            cdt.Retirar(100000, "valledupar");
            Assert.AreEqual(cdt.SaldoCuenta, 900000);
        }

        [Test]
        public void RetiroCorrecto2()
        {
            cdt.SaldoCuenta = 1000000;
            cdt.Retirar(100000, "valledupar");
            cdt.Retirar(200000, "valledupar");
            Assert.AreEqual(cdt.SaldoCuenta, 700000);
        }

        [Test]
        public void RetiroInCorrecto()
        {
            cdt.SaldoCuenta = 1000000;
            cdt.FechaCierre = new DateTime(2021, 12, 1);
            cdt.plazo = 12;
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => cdt.Retirar(40000, "valledupar"));
            Assert.AreEqual(ex.Message, "Aun no se ha cumplido el plazo del CDT");
        }

        [Test]
        public void RetiroInCorrecto2()
        {
            cdt.SaldoCuenta = 1000000;
            cdt.Retirar(900000, "valledupar");
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => cdt.Retirar(120000, "valledupar"));
            Assert.AreEqual(ex.Message, "La cantidad maxima a retirar es de 100000");
        }
    }
}
