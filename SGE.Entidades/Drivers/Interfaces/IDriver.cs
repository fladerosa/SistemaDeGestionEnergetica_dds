using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.WebconAutenticacion.Reglas;

namespace SGE.WebconAutenticacion.Drivers.Interfaces
{
    public interface IDriver
    {
        #region Acciones

        void Encender();
        void Apagar();
        void PonerEnModoAhorroEnergia();

        #endregion
    }
}
