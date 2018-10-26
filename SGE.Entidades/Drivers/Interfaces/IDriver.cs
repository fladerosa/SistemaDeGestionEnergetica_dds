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
        #region Acciones

        void Encender();
        void Apagar();
        void PonerEnModoAhorroEnergia();

        #endregion
    }
}
