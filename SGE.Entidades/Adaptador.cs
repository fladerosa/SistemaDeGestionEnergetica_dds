using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades
{
    public class Adaptador: Decorator
    {
        public Dispositivo Estedispositivo;

        public Adaptador(Dispositivo dispositivo)
        {
            this.Estedispositivo = dispositivo;
        }

         public override decimal obtenerConsumoEnergia()
        {
            return this.Estedispositivo.obtenerConsumoEnergia();
                
        }
    }
}
