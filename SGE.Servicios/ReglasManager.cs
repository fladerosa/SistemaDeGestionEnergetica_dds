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

        private static object syncLock = new object();

        #endregion

        #region Propiedades

        #region Propiedades

        private ReglasManager instance;
        public ReglasManager Instance
        {
            get
            {
                if (this.instance == null)
                {
                    lock (ReglasManager.syncLock)
                    {
                        if (this.instance == null)
                        {
                            this.instance = new ReglasManager();
                        }
                    }
                }

                return this.instance;
            }
        }

        #endregion

        #endregion

        #region Constructores

        private ReglasManager()
        {

        }

        #endregion

        #region Registro de Reglas

        public void RegistrarReglas(Dispositivo dispositivo, List<Regla> reglas)
        {

        }

        #endregion
    }
}
