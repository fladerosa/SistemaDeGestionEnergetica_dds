using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Drivers.Interfaces
{
    public interface ITelevisorDriver: IDriver
    {
        void CambiarCanal(decimal canal);
        void CambiarEntrada();
        void SubirVolumen();
        void BajarVolumen();
        void Mute();
    }
}
