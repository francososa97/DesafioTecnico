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

        /// <summary>
        /// Metodo que se utiliza para loguear Errores por pantalla y aumentar la cantidad de errrores en el caso del metodo ValidarNombre.
        /// </summary>
        /// <param name="CantidadErrores"></param>
        /// <param name="MensajeSaliente"></param>
        /// <returns></returns>
        public static void AlertarError(int nombreUsuario, string CodigoError)
        {
            switch (CodigoError)
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
                default:
                    Console.WriteLine(CodigoError);
                    break;
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

            var NombreValidar = BuildNombreUsuarioValidar(NombreCompleto);

            var cantidadErrores= ContarErroresValidacion(NombreValidar);

            NombreValidar.EsNombreValido = cantidadErrores == 0;
            return NombreValidar.EsNombreValido;

        }
        public static NombreUsuario BuildNombreUsuarioValidar(string nombre)
        {

            char ValorApellido = nombre.Where(c => char.IsUpper(c)).Last();
            int InicioApellido = nombre.IndexOf(ValorApellido);
            int CantidadMayusculas = nombre.Count(c => char.IsUpper(c));
            int CantidadDeEspacios = nombre.Count(Char.IsWhiteSpace);
            string Apellido = nombre.Substring(InicioApellido);
            string NombreSimple = nombre.Substring(0, InicioApellido);

            NombreUsuario NombreValidar = new NombreUsuario(CantidadMayusculas, CantidadDeEspacios, Apellido, NombreSimple, nombre.Trim());

            return NombreValidar;

        }

        public static int ContarErroresValidacion(NombreUsuario nombre)
        {
            bool TienenombreInicial= TieneNombrePrincipal(nombre);
            char inicialNombre = nombre.NombreCompleto.FirstOrDefault();

            if (char.IsUpper(inicialNombre))
            {
                VerificarCaracteresNombre(ref nombre, inicialNombre, TienenombreInicial);

                if (!TienenombreInicial)
                {
                    int i = 0;
                    foreach (var Caracter in nombre.NombreCompleto)
                    {
                        if (Caracter == '.')
                        {
                            int IndiceNombre = nombre.NombreCompleto.IndexOf(nombre.NombreCompleto[i]);
                            var InicialNombre = nombre.NombreCompleto.Substring(0, IndiceNombre);
                            bool NombreInicial = InicialNombre.Length == 1 ? true : false;
                            if (NombreInicial)
                            {
                                string RestoNombre = nombre.NombreCompleto.Substring(IndiceNombre);
                                int IndiceSiguienteMayuscula = nombre.NombreCompleto.IndexOf(RestoNombre.Where(c => char.IsUpper(c)).First());
                                char ApellidoInicio = nombre.NombreCompleto[IndiceSiguienteMayuscula];

                                if (IndiceSiguienteMayuscula == -1)
                                    AlertarError(ref nombre, "Nombre_vacio");

                                if (Char.IsLower(ApellidoInicio))
                                    AlertarError(ref nombre, "Iniciales_No_Capitalizadas");

                                if (!char.IsUpper(nombre.NombreCompleto[i - 1]))
                                    AlertarError(ref nombre, "Iniciales_No_Capitalizadas");

                                nombre.Iniciales.Add(nombre.NombreCompleto[i - 1]);
                            }
                            else
                                AlertarError(ref nombre, "Nombre_Inicial_Nombre");
                        }
                        i++;
                    }
                }
                if (nombre.Apellido.Length == 1)
                    AlertarError(ref nombre, "Apellido_Caracter");
            }
            else
                AlertarError(ref nombre, "Primer_Caracter_Min");

            return nombre.CantidadErrores;
        }

        public static bool TieneNombrePrincipal(NombreUsuario nombre)
        {
            string nombreSimple = "";
            if (nombre.NombreSimple.Length <= 1)
                AlertarError(ref nombre, "El nombre inicial esta vacio");
            else
            {
                bool EsInicialMayusucla = char.IsUpper(nombre.NombreSimple.First());
                int FinNombre = nombre.NombreSimple.IndexOf(' ');
                int InicioNombre = nombre.NombreSimple.IndexOf(nombre.NombreSimple[0]);

                if (!EsInicialMayusucla)
                    AlertarError(ref nombre, "Nombre_Capitalizado");
                else
                {
                    if (FinNombre == -1)
                        AlertarError(ref nombre, "Nombre_Sin_Espacio");
                    else
                        nombreSimple = nombre.NombreSimple.Substring(InicioNombre, FinNombre);
                }
            }
            return nombreSimple.Length > 0;
        }


        /// <summary>
        /// Metodo que se utiliza para loguear Errores por pantalla y aumentar la cantidad de errrores en el caso del metodo ValidarNombre.
        /// </summary>
        /// <param name="nombreUsuario"></param>
        /// <param name="CodigoError"></param>
        /// <returns></returns>
        public static void AlertarError(ref NombreUsuario nombreUsuario, string CodigoError)
        {
            switch (CodigoError)
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
                default:
                    Console.WriteLine(CodigoError);
                    break;
            }
            nombreUsuario.CantidadErrores++;
        }

        public static void VerificarCaracteresNombre(ref NombreUsuario nombre, char inicialNombre, bool TienenombreInicial)
        {
            char CaracterSolicitado = '.';
            int IndiceCaracter = nombre.NombreCompleto.IndexOf(CaracterSolicitado);
            if (IndiceCaracter != -1)
            {
                int CantidadPuntos = nombre.NombreCompleto.Count(c => c == CaracterSolicitado);
                int CantidadPalabras = nombre.CantidadDeEspacios + 1;
                bool ErrrorEnPuntos = CantidadPuntos == CantidadPalabras - 1 || TienenombreInicial ? false : true;

                if (CantidadPalabras > 2)
                {
                    char CaraterSegundoNombre = nombre.NombreSimple.Where(c => char.IsUpper(c)).Last();
                    int InicioSegundoNombre = nombre.NombreSimple.IndexOf(CaraterSegundoNombre);
                    char FinSegundoNombre = nombre.NombreSimple.Where(c => c == ' ').Last();
                    int IndiceFinSegundoNombre = nombre.NombreSimple.IndexOf(CaraterSegundoNombre);
                    string SegundoNombre = nombre.NombreSimple.Substring(InicioSegundoNombre, IndiceFinSegundoNombre);
                    if (SegundoNombre.Length > 1)
                        AlertarError(ref nombre, "Segundo_Nombre_Palabra");
                }
                if (CantidadPuntos > 2)
                    AlertarError(ref nombre, "Termino_Mayor");

                if (ErrrorEnPuntos)
                    AlertarError(ref nombre, "Inicial_Sin_Punto");

                if (nombre.CantidadMayusculas != CantidadPalabras)
                    AlertarError(ref nombre, "Nombre_Minuscula");
            }
            else
                AlertarError(ref nombre, "Sin_caracter");
        }

        #endregion
    }
}
