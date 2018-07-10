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
            this.Transformadores = JsonConvert.DeserializeObject<List<Zona>>(File.ReadAllText(@"Resources/transformadores.json"));
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
