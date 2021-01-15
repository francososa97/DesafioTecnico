using DesafioTecnico.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MetodosDeExtension
{
    public static class MiExtension
    {
        public static string Simplificar(this string Fraccion)
        {
            int[] NumerosSimplificar = ObtenerValores(Fraccion);
            int Num1;
            int Num2;
            string Resultado = "";

            if (NumerosSimplificar.Any())
            {
                Num1 = NumerosSimplificar[0];
                Num2 = NumerosSimplificar[1];
                int Denominador = MinimoComunDenominador(Num1, Num2);
                if(Denominador != 0)
                {
                    Resultado = $"{Num1 / Denominador}/{Num2 / Denominador}";

                    if (Num1 == (Num1 / Denominador) && Num2 == (Num2 / Denominador))
                    {
                        return Resultado = $"\n La fraccion ya esta simplificada => {Num1}/{Num2}";
                    }
                }
                else
                    return Resultado = $"\n No se puede reducir un fraccion con 0";
               

            }
            else
            {
                Console.WriteLine("\n Error al obtener los valores de la fraccion seleccionada");
                Console.ReadKey();
                Environment.Exit(0);
            }

            return Resultado;
        }

        public static int[] ObtenerValores(string Fraccion)
        {
            string Separador = "/";
            int IndiceFraccion = Fraccion.IndexOf(Separador);

            if (IndiceFraccion != -1)
            {
                string Num1 = Fraccion.Substring(0, IndiceFraccion);
                string Num2 = Fraccion.Substring(IndiceFraccion + 1);
                int[] ElementosFraccion = new int[] { Int32.Parse(Num1), Int32.Parse(Num2) };
                return ElementosFraccion;
            }
            else
            {
                Console.WriteLine("Error al ingresar la fraccion");
                Console.WriteLine("RECUERDE la estructura de la fraccion a cargar tiene que tener la siguiente: <Numero>/<Numero>");
                Console.ReadKey();
                Environment.Exit(0);
                return null;
            }
        }

        public static int MinimoComunDenominador(int N1, int N2)
        {
            if ( N2 != 0)
            {
                int Denominador = N1;
                while (N2 != 0)
                {
                    Denominador = N2;
                    N2 = N1 % N2;
                    N1 = Denominador;
                }
                return Denominador;
            }
            else
            {
                return 0;
            }

        }

        public static bool ValidarNombre(this string NombreCompleto)
        {
            NombreUsuario NombreValidar = new NombreUsuario()
            {
                Iniciales = new List<char>(),
                CantidadMayusculas = NombreCompleto.Count(c => char.IsUpper(c)),
                CantidadDeEspacios = NombreCompleto.Count(Char.IsWhiteSpace)
            };
            NombreCompleto = NombreCompleto.Trim();
            if (char.IsUpper(NombreCompleto[0]))
            {
                int i = 0;
                char CaracterSolicitado = '.';
                int IndiceCaracter = NombreCompleto.IndexOf(CaracterSolicitado);
                if(IndiceCaracter != -1)
                {
                    int CantidadPuntos = NombreCompleto.Count(c => c == NombreCompleto[IndiceCaracter]);
                    int CantidadPalabras = NombreValidar.CantidadDeEspacios + 1;
                    bool ErrrorEnPuntos = CantidadPuntos == CantidadPalabras - 1 ? false : true;

                    if (CantidadPuntos > 2)
                        NombreValidar.CantidadErrores = AlertarError(NombreValidar.CantidadErrores, "Solo puede haber de 2 a 3 terminos");

                    if (ErrrorEnPuntos)
                        NombreValidar.CantidadErrores = AlertarError(NombreValidar.CantidadErrores, "Luego de Las iniciales tiene que ir un punto");

                    if (NombreValidar.CantidadMayusculas != CantidadPalabras)
                        NombreValidar.CantidadErrores = AlertarError(NombreValidar.CantidadErrores, "Todo nombre debe contener Mayuscula");
                }
                else
                    NombreValidar.CantidadErrores = AlertarError(NombreValidar.CantidadErrores, "No se encontro el caracter =>. , luego de una inicial se debe incluir un punto");

                char ValorApellido = NombreCompleto.Where(c => char.IsUpper(c)).Last();
                int InicioApellido = NombreCompleto.IndexOf(ValorApellido);
                int FinApellido = NombreCompleto.IndexOf(NombreCompleto.Last());
                NombreValidar.Apellido = NombreCompleto.Substring(InicioApellido, FinApellido);
                NombreValidar.NombreSimple = NombreCompleto.Substring(0, InicioApellido);

                foreach (var Caracter in NombreCompleto)
                {
                    if (CaracterSolicitado == Caracter)
                    {
                        int IndiceNombre = NombreCompleto.IndexOf(NombreCompleto[i]);
                        var InicialNombre = NombreCompleto.Substring(0, IndiceNombre);
                        bool NombreInicial = InicialNombre.Length == 1 ? true : false;
                        if (NombreInicial)
                        {
                            string RestoNombre = NombreCompleto.Substring(IndiceNombre);
                            int IndiceSiguienteMayuscula = NombreCompleto.IndexOf(RestoNombre.Where(c => char.IsUpper(c)).First());
                            char SiguienteCaracter = NombreCompleto[IndiceSiguienteMayuscula + 1];.

                            if (Char.IsLower(SiguienteCaracter))
                                NombreValidar.CantidadErrores = AlertarError(NombreValidar.CantidadErrores, "Todos los nombres tienen que ser iniciales y el apellido como palabra");

                            if (!char.IsUpper(NombreCompleto[i - 1]))
                                NombreValidar.CantidadErrores = AlertarError(NombreValidar.CantidadErrores, "Todas Las iniciales de los nombres tienen que ser capitalizadas");

                            NombreValidar.Iniciales.Add(NombreCompleto[i - 1]);
                        }
                        else
                            NombreValidar.CantidadErrores = AlertarError(NombreValidar.CantidadErrores, "El nombre tiene que ser una inicial y no un nombre");
                    }
                    i++;
                }
                NombreValidar.CantidadIniciales = NombreValidar.Iniciales.Count();

                if (NombreValidar.Apellido.Length == 1)
                    NombreValidar.CantidadErrores = AlertarError(NombreValidar.CantidadErrores, "El apellido debe ser una palabra no un caracter");
            }
            else
                NombreValidar.CantidadErrores = AlertarError(NombreValidar.CantidadErrores, "La primer letra tiene que ser mayuscula");

            NombreValidar.EsNombreValido = NombreValidar.CantidadErrores == 0 ? true : false;

            if (NombreValidar.EsNombreValido)
                MostarContenido(NombreValidar);
            return NombreValidar.EsNombreValido;
        }

        public static int AlertarError(int CantidadErrores, string MensajeSaliente)
        {
            Console.WriteLine($" \nError: {MensajeSaliente}. \n");
            return CantidadErrores = CantidadErrores + 1;
        }

        public static void MostarContenido(NombreUsuario NombreAMostrar)
        {
            foreach (var Inicial in NombreAMostrar.Iniciales)
            {
                Console.WriteLine($"\n Inicial en el nombre del usuario {Inicial}");
            }
            Console.WriteLine($"\n Tiene una cantidad de {NombreAMostrar.CantidadIniciales} iniciales");
            Console.WriteLine($"\n El nombre ingresado fue: {NombreAMostrar.NombreSimple}");
            Console.WriteLine($"\n El apellido ingresado fue {NombreAMostrar.Apellido}");
            Console.WriteLine($"\n El nombre ingresado tiene una cantidad de {NombreAMostrar.CantidadMayusculas} mayusculas");
            Console.WriteLine($"\n El nombre ingresado tiene una cantidad de {NombreAMostrar.CantidadDeEspacios} espacios");
            Console.WriteLine($"\n El nombre ingresado tiene una cantidad de {NombreAMostrar.CantidadErrores} errores");
        }
    }
}
