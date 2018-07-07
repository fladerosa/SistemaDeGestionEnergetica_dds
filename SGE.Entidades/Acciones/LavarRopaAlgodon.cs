using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Entidades.Dispositivos;

namespace SGE.Entidades.Acciones
{
    public class LavarRopaAlgodon: IAccion
    {
        #region Campos

        private Lavarropas dispositivo;

        #endregion

        #region Constructores

        public LavarRopaAlgodon(Lavarropas dispositivo)
        {
            this.dispositivo = dispositivo;
        }

        #endregion

        #region Ejecución

        public void Ejecutar()
        {
            this.dispositivo.LavarRopaAlgodon();
        }

        #endregion
    }
}
