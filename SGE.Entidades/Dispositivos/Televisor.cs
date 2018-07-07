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

        public ITelevisorDriver Driver { get; set; }

        #endregion

        #region Constructores

        public Televisor(ITelevisorDriver driver, string nombre, decimal consumo): base(nombre, consumo, driver)
        {
            this.Driver = driver;
        }

        #endregion

        #region Acciones

        public void CambiarCanal(decimal canal)
        {
            this.Driver.CambiarCanal(canal);
        }

        public void CambiarEntrada()
        {
            this.Driver.CambiarEntrada();
        }

        public void SubirVolumen()
        {
            this.Driver.SubirVolumen();
        }

        public void BajarVolumen()
        {
            this.Driver.BajarVolumen();
        }
        
        public void Mute()
        {
            this.Driver.Mute();
        }


        #endregion
    }
}
