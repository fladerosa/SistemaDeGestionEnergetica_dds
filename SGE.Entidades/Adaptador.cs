using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Tests.Entidades
{
    public class Adaptador : IEstandar
    {
        Adaptador estandarAdaptado;

        public Adaptador(Adaptador a)
        {
            this.estandarAdaptado = a;
        }

        public void MostrarDispositivo()
        {
            this.estandarAdaptado.MostrarDispositivo();
        }
       
    }
}
