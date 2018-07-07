using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Entidades.Drivers.Interfaces;

namespace SGE.Entidades.Dispositivos
{
    public class AireAcondicionado: Inteligente
    {
        #region Propiedades

        public IAireAcondicionadoDriver Driver { get; set; }

        #endregion

        #region Constructores

        public AireAcondicionado(IAireAcondicionadoDriver driver, string nombre, decimal consumo): base(nombre, consumo, driver)
        {
            this.Driver = driver;
        }

        #endregion

        #region Acciones

        public void EstablecerTemperatura(int valor)
        {
            this.Driver.EstablecerTemperatura(valor);
        }

        public void CambiarDireccion()
        {
            this.Driver.CambiarDireccion();
        }

        public void AumentarVelocidadVentilador()
        {
            this.Driver.AumentarVelocidadVentilador();
        }

        public void DecrementarVelocidadVentilador()
        {
            this.Driver.DecrementarVelocidadVentilador();
        }

        public void EstablecerModoCool()
        {
            this.Driver.EstablecerModoCool();
        }

        public void EstablecerModoHeat()
        {
            this.Driver.EstablecerModoHeat();
        }

        public void EstablecerModoDry()
        {
            this.Driver.EstablecerModoDry();
        }

        #endregion
    }
}
