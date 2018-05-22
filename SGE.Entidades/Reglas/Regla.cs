using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Reglas
{
    public class Regla
    {
        public string Nombre { get; set; }
        public Sensor Sensor { get; set; }
        public decimal ValorEsperado { get; set; }


        public void Ejecutar()
        {
        }

        public void TieneValorEsperado(Medicion xMedicion)
        {
        }
    }
}
