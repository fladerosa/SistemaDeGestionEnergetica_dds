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
        public void mostrar_dispositivo()
        {
            this.estandarAdaptado.mostrar_dispositivo();
        }
       
    }
}
