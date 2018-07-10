using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Entidades.Dispositivos;
using SGE.Entidades.Reglas;

namespace SGE.Entidades.Managers
{
    public class ReglasManager
    {
        #region Campos

        private static object reglasManagerSyncLock = new object();

        #endregion

        #region Propiedades

        private static ReglasManager instance;
        public static ReglasManager Instance
        {
            get
            {
                if (ReglasManager.instance == null)
                {
                    lock (ReglasManager.reglasManagerSyncLock)
                    {
                        if (ReglasManager.instance == null)
                        {
                            ReglasManager.instance = new ReglasManager();
                        }
                    }
                }

                return ReglasManager.instance;
            }
        }

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

        #region Obtención

        public List<Regla> ObtenerReglas(Inteligente dispositivo)
        {
            return new List<Regla>();
        }

        #endregion
    }
}
