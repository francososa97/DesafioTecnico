using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioTecnico.Modelo
{
    public class NombreUsuario
    {
        /// <summary>
        /// Lista de las iniciales de los nombres del usuario (No incluye Apellido)
        /// </summary>
        public List<char> Iniciales { get; set; }
        /// <summary>
        /// String donde se almacena el nombre del usuario
        /// </summary>
        public string NombreSimple { get; set; }
        /// <summary>
        /// String donde se almacena el apellido del usuario
        /// </summary>
        public string Apellido { get; set; }
        /// <summary>
        /// Cantidad de mayusuclas que tenga todo el nombre del usuario
        /// </summary>
        public int CantidadMayusculas { get; set; }
        /// <summary>
        /// Cantidad de espacios que tenga todo el nombre del usuario
        /// </summary>
        public int CantidadDeEspacios { get; set; }
        /// <summary>
        /// Numero de errores que se obtienen al validar el nombre, de ser 0 el nombre es valido
        /// </summary>
        public int CantidadErrores { get; set; }
        /// <summary>
        /// Flag que devuelve nuestro metodo de extension para validar el nombre del usuario
        /// </summary>
        public bool EsNombreValido { get; set; }
        /// <summary>
        /// String que contiene el nombre completo trimeado para validar
        /// </summary>
        public string NombreCompleto { get; set; }

        public NombreUsuario(int CantidadMayusculas,int CantidadDeEspacios, string Apellido, string NombreSimple, string NombreCompleto)
        {
            this.Iniciales = new List<char>();
            this.CantidadMayusculas = CantidadMayusculas;
            this.CantidadDeEspacios = CantidadDeEspacios;
            this.Apellido = Apellido;
            this.NombreSimple = NombreSimple;
            this.NombreCompleto = NombreCompleto;
        }

    }
}
