using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Domain.test
{
    public class TestTarjetaCredito
    {
        TarjetaCredito tarjeta;
        [SetUp]
        public void Setup()
        {
            tarjeta = new TarjetaCredito();
            tarjeta.NumeroTarjeta = "1234567887654321";
            tarjeta.CVV = "545";
            tarjeta.FechaExpedicion = DateTime.Today.ToString();
            tarjeta.CupoTargeta = 0;
            tarjeta.SaldoTargeta = 2000000;
        }

        //HU 5. 
        [Test]
        public void AbonoNegativo()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => tarjeta.Abonar(-20000));
            Assert.AreEqual(ex.Message, "La consignacion debe de ser mayor a 0");
        }

        [Test]
        public void AbonoInicialInCorrecto()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => tarjeta.Abonar(3000000));
            Assert.AreEqual(ex.Message, "El valor maximo que puede abonar es 2000000");
        }

        [Test]
        public void AbonoInicialCorrecto()
        {
            tarjeta.Abonar(500000);
            Assert.IsTrue((tarjeta.SaldoTargeta == 1500000) && (tarjeta.CupoTargeta == 500000));
        }

        [Test]
        public void AbonoPosteriorInicialCorrecto()
        {
            tarjeta.Abonar(500000);
            tarjeta.Abonar(400000);
            Assert.IsTrue((tarjeta.SaldoTargeta == 1100000) && (tarjeta.CupoTargeta == 900000));
        }

        [Test]
        public void AbonoPosteriorInicialIncorrecto()
        {
            tarjeta.Abonar(500000);
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => tarjeta.Abonar(1600000));
            Assert.AreEqual(ex.Message, "El valor maximo que puede abonar es 1500000");
        }
        //HU 6. 
        [Test]
        public void AvanceNegativo()
        {
            tarjeta.CupoTargeta = 500000;
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => tarjeta.Avance(-20000, "valledupar"));
            Assert.AreEqual(ex.Message, "El avance debe de ser mayor a 0");
        }

        [Test]
        public void AvanceCorrecto()
        {
            tarjeta.CupoTargeta = 500000;
            tarjeta.Avance(200000, "valledupar");
            Assert.AreEqual(tarjeta.CupoTargeta, 300000);
        }

        [Test]
        public void AvanceCorrectoPosterior()
        {
            tarjeta.CupoTargeta = 500000;
            tarjeta.Avance(200000, "valledupar");
            tarjeta.Avance(200000, "valledupar");
            Assert.AreEqual(tarjeta.CupoTargeta, 100000);
        }

        [Test]
        public void RetiroInCorrecto()
        {
            tarjeta.CupoTargeta = 500000;
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => tarjeta.Avance(1000000, "valledupar"));
            Assert.AreEqual(ex.Message, "El valor maximo del avance es 500000");
        }

        [Test]
        public void RetiroInCorrectoPosterior()
        {
            tarjeta.CupoTargeta = 500000;
            tarjeta.Avance(200000, "valledupar");
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => tarjeta.Avance(301000, "valledupar"));
            Assert.AreEqual(ex.Message, "El valor maximo del avance es 300000");
        }
    }
}
