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
        #region Primer Parte
        /// <summary>
        /// Metodo de extension que se utiliza para reducir una fraccion.
        /// </summary>
        /// <param name="Fraccion"></param>
        /// <returns></returns>
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
                if (Denominador != 0)
                {
                    if (!(Num1 == (Num1 / Denominador) && Num2 == (Num2 / Denominador)))
                        return Resultado = $"{Num1 / Denominador}/{Num2 / Denominador}";
                    else
                        return Resultado = $"La fraccion ya esta simplificada => {Num1}/{Num2}";
                }
                else
                    AlertarError(0, "No se puede reducir un fraccion con 0");

                Resultado = "Error";
            }
            else
            {
                AlertarError(0, "Error al obtener los valores de la fraccion seleccionada");
                Console.ReadKey();
                Environment.Exit(0);
            }

            return Resultado;
        }

        /// <summary>
        /// Metodo que obtiene la informacion de la fraccion cargada de un string.
        /// </summary>
        /// <param name="Fraccion"></param>
        /// <returns></returns>
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
                AlertarError(0, "Error al ingresar la fraccion");
                AlertarError(0, "RECUERDE la estructura de la fraccion a cargar tiene que tener la siguiente: <Numero>/<Numero>");
                Console.ReadKey();
                Environment.Exit(0);
                return null;
            }
        }

        /// <summary>
        /// Metodo que recibe dos numeros y devuelve el minimo comun denominador que tienen en comun.
        /// </summary>
        /// <param name="N1"></param>
        /// <param name="N2"></param>
        /// <returns></returns>
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
        #endregion

        #region Segunda Parte
        /// <summary>
        /// Metodo de Extension que se utiliza para consultar si un nombre es valido o no.
        /// </summary>
        /// <param name="NombreCompleto"></param>
        /// <returns></returns>
        public static bool ValidarNombre(this string NombreCompleto)
        {
            NombreUsuario NombreValidar = new NombreUsuario()
            {
                Iniciales = new List<char>(),
                CantidadMayusculas = NombreCompleto.Count(c => char.IsUpper(c)),
                CantidadDeEspacios = NombreCompleto.Count(Char.IsWhiteSpace)
            };
            NombreCompleto = NombreCompleto.Trim();
            char ValorApellido = NombreCompleto.Where(c => char.IsUpper(c)).Last();
            int InicioApellido = NombreCompleto.IndexOf(ValorApellido);
            NombreValidar.Apellido = NombreCompleto.Substring(InicioApellido);
            NombreValidar.NombreSimple = NombreCompleto.Substring(0, InicioApellido);
            string NombrePalabra = "";
            if (NombreValidar.NombreSimple.Length <= 1)
                NombreValidar.CantidadErrores = AlertarError(NombreValidar.CantidadErrores, "El nombre inicial esta vacio");
            else
            {
                bool EsInicialMayusucla = char.IsUpper(NombreValidar.NombreSimple.First());
                int FinNombre = NombreValidar.NombreSimple.IndexOf(' ');
                int InicioNombre = NombreValidar.NombreSimple.IndexOf(NombreValidar.NombreSimple[0]);

                if (!EsInicialMayusucla)
                    NombreValidar.CantidadErrores = AlertarError(NombreValidar.CantidadErrores, "Nombre_Capitalizado");
                else
                {
                    if (FinNombre == -1)
                        NombreValidar.CantidadErrores = AlertarError(NombreValidar.CantidadErrores, "Nombre_Sin_Espacio");
                    else
                        NombrePalabra = NombreValidar.NombreSimple.Substring(InicioNombre, FinNombre);
                }
            }
            bool NombreInicialPalabra = NombrePalabra.Length > 1 ? true : false;

            if (char.IsUpper(NombreCompleto[0]))
            {
                int i = 0;
                char CaracterSolicitado = '.';
                int IndiceCaracter = NombreCompleto.IndexOf(CaracterSolicitado);
                if(IndiceCaracter != -1)
                {
                    int CantidadPuntos = NombreCompleto.Count(c => c == NombreCompleto[IndiceCaracter]);
                    int CantidadPalabras = NombreValidar.CantidadDeEspacios + 1;
                    bool ErrrorEnPuntos = CantidadPuntos == CantidadPalabras - 1 || NombreInicialPalabra ? false : true;

                    if (CantidadPalabras >2)
                    {

                        char CaraterSegundoNombre = NombreValidar.NombreSimple.Where(c => char.IsUpper(c)).Last();
                        int InicioSegundoNombre = NombreValidar.NombreSimple.IndexOf(CaraterSegundoNombre);
                        char FinSegundoNombre = NombreValidar.NombreSimple.Where(c => c == ' ').Last();
                        int IndiceFinSegundoNombre = NombreValidar.NombreSimple.IndexOf(CaraterSegundoNombre);
                        string SegundoNombre = NombreValidar.NombreSimple.Substring(InicioSegundoNombre, IndiceFinSegundoNombre);
                        if (SegundoNombre.Length > 1)
                            NombreValidar.CantidadErrores = AlertarError(NombreValidar.CantidadErrores, "Segundo_Nombre_Palabra");
                    }
                    if (CantidadPuntos > 2)
                        NombreValidar.CantidadErrores = AlertarError(NombreValidar.CantidadErrores, "Termino_Mayor");

                    if (ErrrorEnPuntos)
                        NombreValidar.CantidadErrores = AlertarError(NombreValidar.CantidadErrores, "Inicial_Sin_Punto");

                    if (NombreValidar.CantidadMayusculas != CantidadPalabras)
                        NombreValidar.CantidadErrores = AlertarError(NombreValidar.CantidadErrores, "Nombre_Minuscula");
                }
                else
                    NombreValidar.CantidadErrores = AlertarError(NombreValidar.CantidadErrores, "Sin_caracter");


                if(!NombreInicialPalabra)
                {
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
                                char ApellidoInicio = NombreCompleto[IndiceSiguienteMayuscula];

                                if (IndiceSiguienteMayuscula == -1)
                                    NombreValidar.CantidadErrores = AlertarError(NombreValidar.CantidadErrores, "Nombre_vacio");

                                if (Char.IsLower(ApellidoInicio))
                                    NombreValidar.CantidadErrores = AlertarError(NombreValidar.CantidadErrores, "Iniciales_No_Capitalizadas");

                                if (!char.IsUpper(NombreCompleto[i - 1]))
                                    NombreValidar.CantidadErrores = AlertarError(NombreValidar.CantidadErrores, "Iniciales_No_Capitalizadas");

                                NombreValidar.Iniciales.Add(NombreCompleto[i - 1]);
                            }
                            else
                                NombreValidar.CantidadErrores = AlertarError(NombreValidar.CantidadErrores, "Nombre_Inicial_Nombre");
                        }
                        i++;
                    }
                }
                else
                {
                    NombreValidar.NombreSimple = NombrePalabra;
                }
               
                NombreValidar.CantidadIniciales = NombreValidar.Iniciales.Count();

                if (NombreValidar.Apellido.Length == 1)
                    NombreValidar.CantidadErrores = AlertarError(NombreValidar.CantidadErrores, "Apellido_Caracter");
            }
            else
                NombreValidar.CantidadErrores = AlertarError(NombreValidar.CantidadErrores, "Primer_Caracter_Min");

            NombreValidar.EsNombreValido = NombreValidar.CantidadErrores == 0 ? true : false;

            if (NombreValidar.EsNombreValido)
                MostarContenido(NombreValidar);
            return NombreValidar.EsNombreValido;
        }

        /// <summary>
        /// Metodo que se utiliza para loguear Errores por pantalla y aumentar la cantidad de errrores en el caso del metodo ValidarNombre.
        /// </summary>
        /// <param name="CantidadErrores"></param>
        /// <param name="MensajeSaliente"></param>
        /// <returns></returns>
        public static int AlertarError(int CantidadErrores, string CodigoError)
        {
            switch(CodigoError)
            {
                case "Primer_Caracter_Min":
                    Console.WriteLine($" \nError: La primer letra tiene que ser mayuscula. \n");
                    break;
                case "Apellido_Caracter":
                    Console.WriteLine($" \nError: El apellido debe ser una palabra no un caracter. \n");
                    break;
                case "Nombre_Inicial_Nombre":
                    Console.WriteLine($" \nError: El nombre tiene que ser una inicial y no un nombre. \n");
                    break;
                case "Iniciales_No_Capitalizadas":
                    Console.WriteLine($" \nError: Todas Las iniciales de los nombres tienen que ser capitalizadas. \n");
                    break;
                case "Apellido_No_Capitalizado":
                    Console.WriteLine($" \nError: El apellido tiene que ser capitalizado. \n");
                    break;
                case "Nombre_vacio":
                    Console.WriteLine($" \nError: El nombre inicial esta vacio. \n");
                    break;
                case "Nombre_apitalizado":
                    Console.WriteLine($" \nError: El nombre debe estar capitalizado. \n");
                    break;
                case "Nombre_Sin_Espacio":
                    Console.WriteLine($" \nError: El nombre debe contener un espacio al final \n");
                    break;
                case "Segundo_Nombre_Palabra":
                    Console.WriteLine($" \nError: El segundo nombre tiene que ser inicial y terminar en punto. \n");
                    break;
                case "Termino_Mayor":
                    Console.WriteLine($" \nError: Solo puede haber de 2 a 3 terminos. \n");
                    break;
                case "Inicial_Sin_Punto":
                    Console.WriteLine($" \nError: Luego de Las iniciales tiene que ir un punto. \n");
                    break;
                case "Nombre_Minuscula":
                    Console.WriteLine($" \nError: Todo nombre debe contener Mayuscula. \n");
                    break;
                case "Sin_caracter":
                    Console.WriteLine($" \nError: No se encontro el caracter =>. , luego de una inicial se debe incluir un punto. \n");
                    break;
            }
            return CantidadErrores = CantidadErrores + 1;
        }

        /// <summary>
        /// Metodo que Muestra por consola toda la informacion que tiene el modelo NombreUsuario cargado en el metodo ValidarNombre.
        /// </summary>
        /// <param name="NombreAMostrar"></param>
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

        #endregion
    }
}
