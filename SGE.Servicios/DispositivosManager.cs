using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Entidades.Dispositivos;

namespace SGE.Servicios
{
    public class DispositivosManager
    {
        #region Campos

        private List<Inteligente> dispositivos;

        #endregion

        #region Constructores

        private DispositivosManager()
        {
            this.dispositivos = new List<Inteligente>();
        }

        #endregion
    }
}
