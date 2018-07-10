using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Entidades.Drivers.Interfaces;

namespace SGE.Entidades.Dispositivos
{
    public class Televisor: Inteligente
    {
        #region Propiedades

        private ITelevisorDriver driver;

        #endregion

        #region Constructores

        public Televisor(ITelevisorDriver driver, string nombre, decimal consumo): base(nombre, consumo, driver)
        {
            this.driver = driver;
        }

        #endregion

        #region Acciones

        public void CambiarCanal(decimal canal)
        {
            this.driver.CambiarCanal(canal);
        }

        public void CambiarEntrada()
        {
            this.driver.CambiarEntrada();
        }

        public void SubirVolumen()
        {
            this.driver.SubirVolumen();
        }

        public void BajarVolumen()
        {
            this.driver.BajarVolumen();
        }
        
        public void Mute()
        {
            this.driver.Mute();
        }


        #endregion
    }
}
