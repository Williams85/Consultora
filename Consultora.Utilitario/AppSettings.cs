using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Utilitario
{
    public class AppSettings
    {
        /// <summary>
        /// Descripcion: Obtiene el valor en cadena
        /// Creador: JELAF (WMC)
        /// Fecha Creación: 2016-09-04
        /// </summary>
        /// <param name="value">Valor de la Llave a obtener valor del archivo de configuración.</param>
        /// <returns></returns>
        public static string valueString(string value)
        {
            return ConfigurationManager.AppSettings[value];
        }

        /// <summary>
        /// Descripcion: Obtiene el valor en cadena
        /// Creador: JELAF (WMC)
        /// Fecha Creación: 2016-09-04
        /// </summary>
        /// <param name="value">Valor de la Llave a obtener valor del archivo de configuración.</param>
        /// <returns></returns>
        public static int valueInt(string value)
        {
            return int.Parse(valueString(value));
        }

        /// <summary>
        /// Descripcion: Obtiene el valor en booleano
        /// Creador: JELAF (WMC)
        /// Fecha Creación: 2016-09-04
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool valueBool(string value)
        {
            return bool.Parse(valueString(value));
        }
    }
}
