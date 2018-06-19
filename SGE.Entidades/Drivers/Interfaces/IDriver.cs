using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Entidades.Reglas;

namespace SGE.Entidades.Drivers.Interfaces
{
    public interface IDriver
    {
        #region Propiedades

        List<Actuador> Actuadores { get; set; }
        List<Sensor> Sensores { get; set; }

        #endregion

        #region Acciones

        void Encender();
        void Apagar();
        void PonerEnModoAhorroEnergia();

        #endregion
    }
}
