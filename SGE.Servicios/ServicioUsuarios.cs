using Newtonsoft.Json;
using SGE.Core.Helpers;
using SGE.Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Servicios
{
    public class ServicioUsuarios
    {
        #region Obtención

        /// <summary>
        /// Permite obtener la lista de usuarios desde la secuencia de bytes (flujo) de archivo especificado.
        /// </summary>
        /// <param name="flujoArchivo">Secuencia de bytes (flujo) de datos que contiene el archivo a procesar</param>
        public List<Usuario> ObtenerUsuarios(Stream flujoArchivo, string nombreArchivo)
        {
            List<Usuario> usuarios = new List<Usuario>();
         
            if (this.EsArchivoValido(flujoArchivo, nombreArchivo))
            {
                string contenido = this.ObtenerContenidoArchivo(flujoArchivo);

                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                usuarios = JsonConvert.DeserializeObject<List<Usuario>>(contenido, settings);
            }
            else
                LogHelper.LogErrorMessage("El archivo que intenta cargar el usuario está vacío o no es un archivo con extensión 'json'");

            return usuarios;
        }

        /// <summary>
        /// Obtiene el contenido del archivo de la secuencia de bytes (flujo)
        /// </summary>
        /// <param name="flujoArchivo">Secuencia de bytes (flujo) de donde se desea obtener el archivo</param>
        /// <returns></returns>
        private string ObtenerContenidoArchivo(Stream flujoArchivo)
        {
            string contenido = string.Empty;

            using (var ms = new MemoryStream())
            {
                flujoArchivo.CopyTo(ms);
                ms.Position = 0;

                contenido = new StreamReader(ms).ReadToEnd();
            }

            return contenido;
        }

        #endregion

        #region Validaciones

        /// <summary>
        /// Indica si el archivo contenido en la secuencia de bytes (flujo) cumple las condiciones para ser procesado
        /// </summary>
        /// <param name="flujoArchivo">Secuencia de bytes (flujo) de donde se desea obtener el archivo</param>
        /// <param name="nombreArchivo">Nombre del archivo especificado por el usuario.</param>
        /// <returns></returns>
        private bool EsArchivoValido(Stream flujoArchivo, string nombreArchivo)
        {
            string extensionArchivo = Path.GetExtension(nombreArchivo);
            return flujoArchivo != null && flujoArchivo.Length > 0 && extensionArchivo == ".json";
        }

        #endregion
    }
}
