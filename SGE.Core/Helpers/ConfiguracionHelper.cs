using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Core.Helpers
{
    public class ConfiguracionHelper
    {
        #region Propiedades

        public static string AccountFilesPath
        {
            get
            {
                return ConfiguracionHelper.ObtenerConfiguracion("PathArchivo", string.Empty);
            }
        }

        #endregion

        #region Manejo de los valores de configuración

        /// <summary>
        /// Devuelve el valor de la configuración que se corresponde con el nombre especificado.
        /// </summary>
        /// <param name="nombre">Nombre del valor a evaluar.</property>
        /// <param name="valorPredeterminado">Valor predeterminado.</property>
        private static string ObtenerConfiguracion(string nombre, object valorPredeterminado)
        {
            string valor = ConfigurationManager.AppSettings[nombre];
            return (valor == null) ? valorPredeterminado.ToString() : valor;
        }

        #endregion
    }
}
