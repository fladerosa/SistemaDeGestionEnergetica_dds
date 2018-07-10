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
        Dictionary<Inteligente, List<Regla>> reglas;

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
            this.reglas = new Dictionary<Inteligente, List<Regla>>();
        }

        #endregion

        #region Registro de Reglas

        public void RegistrarReglas(Inteligente dispositivo, List<Regla> reglas)
        {
            this.reglas.Add(dispositivo, reglas);
        }

        #endregion

        #region Obtención

        public List<Regla> ObtenerReglasParaDispositivo(Inteligente dispositivo)
        {
            return this.reglas[dispositivo];
        }

        #endregion
    }
}
