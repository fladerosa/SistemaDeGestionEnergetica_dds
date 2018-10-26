using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Reglas.Operadores
{
    public class MayorOIgual: IOperador
    {
        public bool Verificar(decimal referencia, decimal valor)
        {
            return valor >= referencia;
        }
    }
}
