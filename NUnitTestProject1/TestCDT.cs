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
            cdt.NombreCuenta = "fabian";
            cdt.SaldoCuenta = 0;
            cdt.FechaApertura = new DateTime(2021, 1, 1);
            cdt.FechaCierre = new DateTime(2021, 9, 1);
            cdt.plazo = 9;
        }


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
    }
}
