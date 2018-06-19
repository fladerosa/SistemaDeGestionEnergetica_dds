using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Entidades.Drivers.Interfaces;
using SGE.Entidades.Reglas;

namespace SGE.Entidades.Drivers
{
    public class SamsungAireAcondicionado : IAireAcondicionadoDriver
    {
        public List<Actuador> Actuadores { get; set; }
        public List<Sensor> Sensores { get; set; }

        public void Apagar()
        {
            throw new NotImplementedException();
        }

        public void AumentarVelocidadVentilador()
        {
            throw new NotImplementedException();
        }

        public void CambiarDireccion()
        {
            throw new NotImplementedException();
        }

        public void DecrementarVelocidadVentilador()
        {
            throw new NotImplementedException();
        }

        public void Encender()
        {
            throw new NotImplementedException();
        }

        public void EstablecerModoCool()
        {
            throw new NotImplementedException();
        }

        public void EstablecerModoDry()
        {
            throw new NotImplementedException();
        }

        public void EstablecerModoHeat()
        {
            throw new NotImplementedException();
        }

        public void EstablecerTemperatura(int valor)
        {
            throw new NotImplementedException();
        }

        public void PonerEnModoAhorroEnergia()
        {
            throw new NotImplementedException();
        }
    }
}
