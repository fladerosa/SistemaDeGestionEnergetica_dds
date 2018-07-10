using System.Collections.Generic;

namespace SGE.Core.Entidades
{
    public class Zona
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public decimal latitud { get; set; }
        public decimal longitud { get; set; }
        public int radio { get; set; }
    }
}
