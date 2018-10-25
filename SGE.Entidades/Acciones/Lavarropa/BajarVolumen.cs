using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.WebconAutenticacion.Dispositivos;

namespace SGE.WebconAutenticacion.Acciones.TV
{
    public class BajarVolumen: Accion
    {
        #region Campos

        private Televisor dispositivo;

        #endregion

        #region Constructores

        public BajarVolumen(Televisor dispositivo)
        {
            this.dispositivo = dispositivo;
        }

        #endregion

        #region Ejecución

        public void Ejecutar()
        {
            this.dispositivo.BajarVolumen();
        }

        #endregion
    }
}
