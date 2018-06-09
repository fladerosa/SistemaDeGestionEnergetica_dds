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
        public OperadorEnum Operador { get; set; }

        #endregion Propiedades


        #region Metodos
        public bool Evaluar()
        {
            switch (Operador)
            {
                case OperadorEnum.Mayor:
                    return Sensor.Medir() > Valor;
                case OperadorEnum.Menor:
                    return Sensor.Medir() < Valor;
                case OperadorEnum.Igual:
                    return Sensor.Medir() == Valor;
                default:
                    return Sensor.Medir() != Valor;
            }
        }

        #endregion Metodos
    }
}
