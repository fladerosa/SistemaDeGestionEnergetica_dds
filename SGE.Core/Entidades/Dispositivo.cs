using System.Collections.Generic;

namespace SGE.Core.Entidades
{
    public class Dispositivo
    {
        public string Id { get; set; }
        public string Tipo { get; set; }
        public string EsInteligente { get; set; }
        public string EsBajoConsumo { get; set; }
        public double Consumo { get; set; }
        public List<string> Caracteristicas { get; set; }
    }
}
