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
        private static object syncLock = new object();

        #endregion

        #region Propiedades

        private DispositivosManager instance;
        public DispositivosManager Instance
        {
            get
            {
                if (this.instance == null)
                {
                    lock (DispositivosManager.syncLock)
                    {
                        if (this.instance == null)
                        {
                            this.instance = new DispositivosManager();
                        }
                    }
                }

                return this.instance;
            }
        }

        #endregion

        #region Constructores

        private DispositivosManager()
        {
            this.dispositivos = new List<Inteligente>();
        }

        #endregion

        #region Registro

        public void RegistrarDispositivo(Inteligente dispositivo)
        {
            this.dispositivos.Add(dispositivo);

        }

        #endregion
    }
}
