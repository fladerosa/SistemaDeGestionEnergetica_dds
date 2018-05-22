using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Dispositivos
{
    public class Estandar: Dispositivo
    {
        #region Propiedades

        public override decimal ConsumoEnergia {get; set;}

        public decimal PromedioUsoDiario { get; set; }

        #endregion
    }   
    
}
