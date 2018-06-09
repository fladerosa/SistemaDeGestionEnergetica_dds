using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Reglas
{
    public class Condicion
    {

        #region Propiedades
        public Sensor Sensor { get; set; }
        public decimal Valor { get; set; }
        public Enum OperacionEnum { get; set; }

        #endregion Propiedades


        #region Metodos
        public bool Evaluar()
        {
            return true;
        }

        #endregion Metodos
    }
}
