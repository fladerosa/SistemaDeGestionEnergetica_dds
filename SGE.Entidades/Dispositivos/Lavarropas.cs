using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Entidades.Drivers.Interfaces;

namespace SGE.Entidades.Dispositivos
{
    public class Lavarropas: Inteligente
    {
        #region Propiedades

        public ILavarropasDriver Driver { get; set; }

        #endregion

        #region Constructores

        public Lavarropas(ILavarropasDriver driver, string nombre, decimal consumo): base(nombre, consumo)
        {
            this.Driver = driver;
        }

        #endregion

        #region Acciones

        public void LavarRopaAlgodon()
        {
            this.Driver.LavarRopaAlgodon();
        }

        #endregion
    }
}
