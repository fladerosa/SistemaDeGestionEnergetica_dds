using SGE.WebconAutenticacion.Drivers.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;


namespace SGE.WebconAutenticacion.Dispositivos
{
    [NotMapped]
    public class AireAcondicionado: Inteligente
    {
        #region Propiedades

        private IAireAcondicionadoDriver driver;

        #endregion

        #region Constructores

        public AireAcondicionado(IAireAcondicionadoDriver driver, string nombre, decimal consumo): base(nombre, consumo, driver)
        {
            this.driver = driver;
        }

        #endregion

        #region Acciones

        public void EstablecerTemperatura(int valor)
        {
            this.driver.EstablecerTemperatura(valor);
        }

        public void CambiarDireccion()
        {
            this.driver.CambiarDireccion();
        }

        public void AumentarVelocidadVentilador()
        {
            this.driver.AumentarVelocidadVentilador();
        }

        public void DecrementarVelocidadVentilador()
        {
            this.driver.DecrementarVelocidadVentilador();
        }

        public void EstablecerModoCool()
        {
            this.driver.EstablecerModoCool();
        }

        public void EstablecerModoHeat()
        {
            this.driver.EstablecerModoHeat();
        }

        public void EstablecerModoDry()
        {
            this.driver.EstablecerModoDry();
        }

        public decimal ObtenerTemperaturaActual()
        {
            return this.driver.ObtenerTemperaturaActual();
        }

        #endregion
    }
}
