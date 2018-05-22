using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Tests.Entidades
{
    public interface IInteligente
    {
        decimal ConsumoEnergia { get; }
        bool EstaEncendido { get; }
        string IdentificadorFab { get; }

    }
}
