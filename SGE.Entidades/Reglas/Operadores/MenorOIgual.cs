using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.WebconAutenticacion.Reglas.Operadores
{
    public class MenorOIgual: IOperador
    {
        public bool Verificar(decimal referencia, decimal valor)
        {
            return valor <= referencia;
        }
    }
}
