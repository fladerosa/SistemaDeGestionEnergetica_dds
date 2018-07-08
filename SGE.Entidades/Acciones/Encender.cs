using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Entidades.Dispositivos;

namespace SGE.Entidades.Acciones
{
    public class Encender: IAccion
    {
        #region Campos

        private Inteligente dispositivo;

        #endregion

        #region Constructores

        public Encender(Inteligente dispositivo)
        {
            this.dispositivo = dispositivo;
        }

        #endregion

        #region Ejecución

        public void Ejecutar()
        {
            this.dispositivo.Encender();
        }

        #endregion
    }
}
