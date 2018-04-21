using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades
{
    public class Categoria
    {
        public string Codigo { get; set; }
        public decimal ConsumoMinimo { get; set; }
        public decimal ConsumoMaximo { get; set; }
        public decimal CostoFijo { get; set; }
        public decimal CostoVariable { get; set; }
    }
}
