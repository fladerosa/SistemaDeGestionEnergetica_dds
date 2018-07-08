using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Entidades.Dispositivos;

namespace SGE.Entidades.Acciones.TV
{
    public class CambiarEntrada: IAccion
    {
        #region Campos

        private Televisor dispositivo;

        #endregion

        #region Constructores

        public CambiarEntrada(Televisor dispositivo)
        {
            this.dispositivo = dispositivo;
        }

        #endregion

        #region Ejecución

        public void Ejecutar()
        {
            this.dispositivo.CambiarEntrada();
        }

        #endregion
    }
}
