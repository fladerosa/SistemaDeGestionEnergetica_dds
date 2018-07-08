using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Drivers.Interfaces
{
    public interface IAireAcondicionadoDriver: IDriver
    {
        void EstablecerTemperatura(int valor);
        void CambiarDireccion();
        void AumentarVelocidadVentilador();
        void DecrementarVelocidadVentilador();
        void EstablecerModoCool();
        void EstablecerModoHeat();
        void EstablecerModoDry();
        decimal ObtenerTemperaturaActual();
    }
}
