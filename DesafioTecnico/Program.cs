using System;
using System.Linq;
using System.Text.RegularExpressions;
using MetodosDeExtension;

namespace DesafioTecnico
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Desafío Técnico - E3 Ecommerce");
            Console.WriteLine("Nombre:Gonzalez Sosa Franco Damian");
            Console.WriteLine("Ingrese que desafio quiere realizar, Ingresando 1 o 2");
            Console.WriteLine("1. Simplificador de fracciones.");
            Console.WriteLine("2. Validador de nombres.");
            Console.WriteLine("0. Cerrar");
            Console.WriteLine("Ingrese para comenzar");
            var Operacion = Console.ReadLine();

            do
            {
                switch (Operacion)
                {
                    case "1":
                        ObtenerFraccionSimplificada();
                        break;
                    case "2":
                        ObtenerValidacionNombre();
                        break;
                    default:
                        Console.WriteLine("Dato invalido, opciones a elegir 1 para Simplificador de fracciones o 2  Validador de nombres.");
                        Console.WriteLine("Vuelva a ingresar");
                        Operacion = Console.ReadLine();
                        break;
                }
            } while (Operacion != "0");

            Console.WriteLine("Adios");
            Console.ReadKey();
        }

        public static void ObtenerFraccionSimplificada()
        {
            string FraccionPrueba1 = "2/4";
            string FraccionPrueba2 = "10/11";
            string FraccionPrueba3 = "100/400";
            string PruebaAlgoritmo1 = FraccionPrueba1.Simplificar();
            string PruebaAlgoritmo2 = FraccionPrueba2.Simplificar();
            string PruebaAlgoritmo3 = FraccionPrueba3.Simplificar();

            Console.WriteLine($"Una prueba del sistema: {FraccionPrueba1} => {PruebaAlgoritmo1}");
            Console.WriteLine($"Una prueba del sistema: {FraccionPrueba2} => {PruebaAlgoritmo2}");
            Console.WriteLine($"Una prueba del sistema: {FraccionPrueba3} => {PruebaAlgoritmo3}");

            Console.WriteLine("\n\nLa estructura de la fraccion a cargar tiene que tener la siguiente: <Numero>/<Numero>");
            Console.WriteLine("\nque fraccion quiere simplificar? ");
            string FraccionSeleccionada = Console.ReadLine();
            string FraccionSimplificada = FraccionSeleccionada.Simplificar();

            Console.WriteLine($"\n La simplificacion de la fraccion {FraccionSeleccionada} es {FraccionSimplificada}");
        }

        public static void ObtenerValidacionNombre()
        {
            var NPrueba8 = "E. Allan P.";
            bool NombreValido8 = NPrueba8.ValidarNombre();
            string Resultado8= NombreValido8 ? " valido" : " invalido";
            Console.WriteLine($"El nombre {NPrueba8} es {Resultado8} y tenia que ser invalido");

            Console.WriteLine("\nSeleccione el nombre a validar: ");
            string NomnreSeleccionado = Console.ReadLine();
            bool NombreValido = NomnreSeleccionado.ValidarNombre();
            string Resultado = NombreValido ? "El nombre ingresado es valido" : "El nombre ingresado es invalido";
            Console.WriteLine(Resultado);
        }

    }
}
