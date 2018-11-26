using SGE.Entidades.Acciones;
using SGE.Entidades.Dispositivos;
using SGE.Entidades.Reglas;
using System.Collections.Generic;

namespace SGE.Entidades.Managers {

    public class DispositivosManager
    {
        #region Campos

        public List<Inteligente> dispositivos;
   
        public List<Accion> Acciones { get; set; }

        private static object dispositivosManagerSyncLock = new object();


        #endregion

        #region Propiedades

        private static DispositivosManager instance;
        public static DispositivosManager Instance
        {
            get
            {
                if (DispositivosManager.instance == null)
                {
                    lock (DispositivosManager.dispositivosManagerSyncLock)
                    {
                        if (DispositivosManager.instance == null)
                        {
                            DispositivosManager.instance = new DispositivosManager();
                        }
                    }
                }

                return DispositivosManager.instance;
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
            dispositivo.Agregar(this);
        }

        #endregion

        #region Mediciones

        public void NotificarNuevaMedicion(Inteligente dispositivo)
        {
            List<Regla> reglas = ReglasManager.Instance.ObtenerReglasParaDispositivo(dispositivo);

            foreach (Regla regla in reglas)
            {
                regla.Ejecutar();
            }
        }

        #endregion
    }
}
