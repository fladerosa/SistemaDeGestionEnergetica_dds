using Newtonsoft.Json;
using SGE.Core.Entidades;
using System.Collections.Generic;
using System.IO;

namespace SGE.Core.Helpers
{
    public class ZonasHelper
    {
        public List<Zona> Zonas { get; private set; }


        public ZonasHelper()
        {
            this.Zonas = JsonConvert.DeserializeObject<List<Zona>>(File.ReadAllText(@"C:\Users\Flavia\source\repos\DiseñoSistemasTPAnual\dds-tp-2018-grupo-01\SGE.Core\Resources\zonas.json"));
        }

        public Zona GetDispositivo(string id)
        {
            foreach(Zona zona in this.Zonas)
                if (zona.Id.Equals(id))
                    return zona;
            return null;
        }

    }
}
