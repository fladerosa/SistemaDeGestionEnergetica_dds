using Newtonsoft.Json;
using SGE.Core.Entidades;
using System;
using System.Collections.Generic;
using System.IO;

namespace SGE.Core.Helpers
{
    public class ZonasHelper
    {
        public List<Zona> Zonas { get; private set; }


        public ZonasHelper()
        {
            this.Zonas = JsonConvert.DeserializeObject<List<Zona>>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\zonas.json")));
           // this.Zonas = JsonConvert.DeserializeObject<List<Zona>>(File.ReadAllText(@"Resources\zonas.json"));

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
