using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Reglas
{
    public class Medicion
    {
        public Medicion(Sensor xSensor)
        {
            Sensor = xSensor;
        }

        public Sensor Sensor { get; set; }
        public decimal Valor { get; set; }
    }
}
