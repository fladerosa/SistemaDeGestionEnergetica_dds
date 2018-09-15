using Newtonsoft.Json;
using SGE.Core.Entidades;
using System.Collections.Generic;
using System.IO;

namespace SGE.Core.Helpers
{
    public class DispositivosHelper
    {
        public List<Dispositivo> Dispositivos { get; private set; }
        private static DispositivosHelper Instancia = null;

        private DispositivosHelper()
        {
            this.Dispositivos = JsonConvert.DeserializeObject<List<Dispositivo>>(File.ReadAllText(@"C:\Users\kenny\source\repos\dds-tp-2018-grupo-01\SGE.Core\Resources\dispositivos.json"));
        }

        /// <summary>
        /// Devuelve la instancia del helper.
        /// </summary>
        public static DispositivosHelper GetInstace()
        {
            if (Instancia == null)
                Instancia = new DispositivosHelper();
            return Instancia;
        }

        /// <summary>
        /// Devuelve el Dspositivo solicitado o null en caso de que no exista.
        /// </summary>
        public Dispositivo GetDispositivo(string id)
        {
            foreach(Dispositivo dispositivo in this.Dispositivos)
                if (dispositivo.Id.Equals(id))
                    return dispositivo;
            return null;
        }

        /// <summary>
        /// Devuelve True o False, si existe o no el dispositivo.
        /// </summary>
        public bool Existe(string id)
        {
            foreach (Dispositivo dispositivo in this.Dispositivos)
                if (dispositivo.Id.Equals(id))
                    return true;
            return false;
        }
    }
}
