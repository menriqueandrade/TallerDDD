using System;
using Domain.Entities;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        public static void Main(string[] args)
        {
            menu();
        }


        public static void menu()
        {
            List<Credito> creditos = new List<Credito>();
            string opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("Seleccione una opcion:");
                Console.WriteLine("1) Solicitar prestamo");
                Console.WriteLine("2) creditos");
                Console.WriteLine("3) abonar");
                Console.WriteLine("4) mostrar abonos por cedula");
                Console.WriteLine("5) mostrar cuotas");
                Console.WriteLine("6) Exit");
                Console.Write("\r\nSelect an option: ");
                opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        leerCredito(creditos);
                        break;
                    case "2":
                        mostrarCreditos(creditos);
                        break;
                    case "3":
                        abonar(creditos);
                        break;
                    case "4":
                        consultarAbonos(creditos);
                        break;
                    case "5":
                        consultarCuotas(creditos);
                        break;
                    default:
                        break;
                }
            } while (opcion != "6");
        }


        public static void leerCredito(List<Credito> creditos)
        {
            Console.WriteLine("Cedula:");
            string cedula = Console.ReadLine();
            Console.WriteLine("Nombre:");
            string nombre = Console.ReadLine();
            Console.WriteLine("Valor del prestamo:");
            double vprestamo = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Salario:");
            double salario = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Plazo:");
            int plazo = Convert.ToInt32(Console.ReadLine());

            Credito NuevoCredito;
            NuevoCredito = new Credito
            {
                Cedula = cedula,
                Nombre = nombre,
                ValorPrestamo = vprestamo,
                Salario = salario,
                Fecha = DateTime.Now,
                PlazoPago = plazo,
            };
            try
            {
                creditos.Add(NuevoCredito);
                NuevoCredito.Validar(NuevoCredito.ValorPrestamo, NuevoCredito.PlazoPago);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }

        public static void mostrarCreditos(List<Credito> creditos)
        {
            foreach (var item in creditos)
            {
                Console.WriteLine("-----------------------------");
                Console.WriteLine("cedula: " + item.Cedula);
                Console.WriteLine("nombre: " + item.Nombre);
                Console.WriteLine("salario: " + item.Salario);
                Console.WriteLine("valor: " + item.ValorPrestamo);
                Console.WriteLine("plazo: " + item.PlazoPago);
                Console.WriteLine("estado: " + item.estado);  
            }
            Console.ReadLine();
        }

        public static void abonar(List<Credito> creditos)
        {
            Console.WriteLine("Cedula:");
            string cedula = Console.ReadLine();
            Console.WriteLine("Abono:");
            double abono = Convert.ToDouble(Console.ReadLine());

            Credito credito = null;

            foreach (var item in creditos)
            {
                if (item.Cedula == cedula)
                {
                    credito = item;
                    break;
                }
            }

            if (credito != null)
            {
                try
                {
                    credito.Abonar(cedula, abono);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex);
                }       
            }
            else
            {
                Console.WriteLine("credito no encontrado");
            }

            Console.ReadLine();
        }

        public static void consultarAbonos(List<Credito> creditos)
        {
            Console.WriteLine("Cedula:");
            string cedula = Console.ReadLine();
           
            Credito credito = null;

            foreach (var item in creditos)
            {
                if (item.Cedula == cedula)
                {
                    credito = item;
                    break;
                }
            }

            if (credito != null)
            { 
                var abonos = credito.ConsultarAbono(cedula);
                foreach (var item2 in abonos)
                {
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine("cedula: " + item2.Cedula);
                    Console.WriteLine("nombre: " + item2.ValorAbonado);
                    Console.WriteLine("salario: " + item2.FechaAbono);
                }
            }
            else
            {
                Console.WriteLine("credito no encontrado");
            }

            Console.ReadLine();
        }

        public static void consultarCuotas(List<Credito> creditos)
        {
            Console.WriteLine("Cedula:");
            string cedula = Console.ReadLine();

            Credito credito = null;

            foreach (var item in creditos)
            {
                if (item.Cedula == cedula)
                {
                    credito = item;
                    break;
                }
            }

            if (credito != null)
            {
                var cuotas = credito.ConsultarPorCedula(cedula);
                foreach (var item2 in cuotas)
                {
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine("#: " + item2.NumeroCuota);
                    Console.WriteLine("valor cuota: " + item2.ValorCuota);
                    Console.WriteLine("valor abonado: " + item2.ValorAbonado);
                    Console.WriteLine("valor pendiente: " + item2.ValorPendiente);
                }
            }
            else
            {
                Console.WriteLine("credito no encontrado");
            }

            Console.ReadLine();
        }
    }
}
