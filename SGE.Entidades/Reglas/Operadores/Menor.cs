using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Entidades.Reglas.Operadores;

namespace SGE.Entidades.Reglas.Operadores
{
    public class Menor: IOperador
    {
        public bool Verificar(decimal referencia, decimal valor)
        {
            return valor < referencia;
        }
    }
}
