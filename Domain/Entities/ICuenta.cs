using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    interface ICuenta
    {
        string NumeroCuenta { get; set; }
        string NombreCuenta { get; set; }
        double SaldoCuenta { get; set; }
       

        void Consignar(double valor, string ciudad);
        void Retirar(double valor, string ciudad);
        void GuardarMovimieto(string tipo, double valorRetiro, double valorConsignacion, string ciudad);


    }
}
