using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades
{
    public abstract class Decorator: Dispositivo
    {
         public abstract override decimal obtenerConsumoEnergia();
          
    }
}

