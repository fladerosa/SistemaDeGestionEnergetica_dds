using SGE.Entidades.Drivers.Interfaces;

namespace SGE.Entidades.Dispositivos
{
    public class Lavarropas: Inteligente
    {
        #region Propiedades

        private ILavarropasDriver driver;

        #endregion

        #region Constructores

        public Lavarropas(ILavarropasDriver driver, string nombre, decimal consumo): base(nombre, consumo, driver)
        {
            this.driver = driver;
        }

        #endregion

        #region Acciones

        public void LavarRopaAlgodon()
        {
            this.driver.LavarRopaAlgodon();
        }

        #endregion
    }
}
