using Newtonsoft.Json;
using SGE.Core.Entidades;
using System.Collections.Generic;
using System.IO;

namespace SGE.Core.Helpers
{
    public class DispositivosHelper
    {
        public List<Dispositivo> Dispositivos { get; private set; }


        public DispositivosHelper()
        {
            this.Dispositivos = JsonConvert.DeserializeObject<List<Dispositivo>>(File.ReadAllText(@"Resources/dispositivos.json"));
        }

        public Dispositivo GetDispositivo(string id)
        {
            foreach(Dispositivo dispositivo in this.Dispositivos)
                if (dispositivo.Id.Equals(id))
                    return dispositivo;
            return null;
        }

    }
}
