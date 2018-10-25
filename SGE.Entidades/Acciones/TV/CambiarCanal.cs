using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.WebconAutenticacion.Dispositivos;

namespace SGE.WebconAutenticacion.Acciones.TV
{
    public class CambiarCanal: Accion
    {
        #region Campos

        private Televisor dispositivo;
        private decimal canal;

        #endregion

        #region Constructores

        public CambiarCanal(Televisor dispositivo, decimal canal)
        {
            this.dispositivo = dispositivo;
            this.canal = canal;
        }

        #endregion

        #region Ejecución

        public void Ejecutar()
        {
            this.dispositivo.CambiarCanal(this.canal);
        }

        #endregion
    }
}
