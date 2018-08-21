using System.Collections.Generic;

namespace SGE.Core.Entidades
{
    public class Dispositivo
    {
        public string Id { get; set; }
        public string Tipo { get; set; }
        public string Inteligente { get; set; }
        public string BajoConsumo { get; set; }
        public double Consumo { get; set; }
        public string Reemplazable { get; set; }
        public List<string> Caracteristicas { get; set; }

        public bool EsInteligente
        {
            get { return this.Inteligente.Equals("S"); }
        }

        public bool EsBajoConsumo
        {
            get { return this.BajoConsumo.Equals("S"); }
        }

        public bool EsReemplazable
        {
            get { return this.Reemplazable.Equals("S"); }
        }
    }
}
