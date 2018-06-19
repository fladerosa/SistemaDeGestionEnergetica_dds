using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Entidades.Drivers.Interfaces;
using SGE.Entidades.Reglas;

namespace SGE.Entidades.Drivers
{
    public class DreanLavarropas : ILavarropasDriver
    {
        public List<Actuador> Actuadores { get; set; }
        public List<Sensor> Sensores { get; set; }

        public void Apagar()
        {
            throw new NotImplementedException();
        }

        public void Encender()
        {
            throw new NotImplementedException();
        }

        public void LavarRopaAlgodon()
        {
            throw new NotImplementedException();
        }

        public void PonerEnModoAhorroEnergia()
        {
            throw new NotImplementedException();
        }
    }
}
