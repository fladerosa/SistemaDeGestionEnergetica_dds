using Newtonsoft.Json;
using SGE.Core.Entidades;
using System.Collections.Generic;
using System.IO;

namespace SGE.Core.Helpers
{
    public class TransformadoresHelper
    {
        public List<Transformador> Transformadores { get; private set; }


        public TransformadoresHelper()
        {
            this.Transformadores = JsonConvert.DeserializeObject<List<Transformador>>(File.ReadAllText(@"C:\Users\kenny\source\repos\dds-tp-2018-grupo-01\SGE.Core\Resources\transformadores.json"));
        }

        public TransformadoresHelper(string nombreTransformadorJson)
        {
            this.Transformadores = JsonConvert.DeserializeObject<List<Transformador>>(File.ReadAllText(@"C:\Users\kenny\source\repos\dds-tp-2018-grupo-01\SGE.Core\Resources\" + nombreTransformadorJson));
        }

        public Transformador GetDispositivo(string id)
        {
            foreach(Transformador transformador in this.Transformadores)
                if (transformador.Id.Equals(id))
                    return transformador;
            return null;
        }

    }
}
