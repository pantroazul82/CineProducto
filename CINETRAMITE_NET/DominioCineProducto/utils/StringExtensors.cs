using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;
using System.Threading;

namespace DominioCineProducto.utils
{   
    public static class StringExtensors
    {
        /// <summary>

            /// Devuelve una copia de este objeto String en formato Título aplicando

           /// las reglas de la cultura actual

           /// </summary>

           public static string ToNombrePropio(this System.String texto)

           {
            if (texto != null)
                return texto.ToNombrePropio(Thread.CurrentThread.CurrentCulture).Replace("  ", " ");

            else
                return null;

           }

    

           /// <summary>

           /// Devuelve una copia de este objeto String en formato título aplicando
           /// las reglas de la cultura especificada

           /// </summary>

           /// <param name="culture">Objeto System.Globalization.CultureInfo que proporciona

          /// reglas de Titulo</param>

           public static string ToNombrePropio(this System.String texto, CultureInfo culture)
           {
                if (texto == null)
                    return null;

                TextInfo ti = culture.TextInfo;
                return ti.ToTitleCase(texto.ToLower());

           }
    }
}
