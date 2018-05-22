using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Entidades.Dispositivos;

namespace SGE.Entidades.Reglas
{
    public class Sensor
    {
        public string Nombre { get; set; }
        public decimal ValorMedido { get; set; }
        public string TipoMagnitud { get; set; }
        public Dispositivo Dispositivo { get; set; }


        public decimal ObtenerMedicion()
        {
            return decimal.MaxValue;
        }

        public void ComunicarMedicion()
        {
        }
    }
}
