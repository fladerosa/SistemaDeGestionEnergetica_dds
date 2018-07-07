using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Entidades.Dispositivos;

namespace SGE.Entidades.Acciones
{
    public class CambiarCanal: IAccion
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
