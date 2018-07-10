using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Entidades.Dispositivos;
using SGE.Entidades.Reglas;

namespace SGE.Servicios
{
    public class ReglasManager
    {
        #region Campos

        private List<Regla> reglas;

        #endregion

        #region Constructores

        private ReglasManager()
        {
            this.reglas = new List<Regla>();
        }

        #endregion

        #region Registro de Reglas

        public void RegistrarReglas(Dispositivo dispositivo, List<Regla> reglas)
        {

        }

        #endregion
    }
}
