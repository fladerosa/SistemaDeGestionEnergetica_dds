using System;
using System.Collections.Generic;
using SGE.Entidades.Usuarios;

namespace SGE.Core.Entidades
{
    public class Transformador
    {
        public int Id { get; set; }
        public decimal latitud { get; set; }
        public decimal longitud { get; set; }
        public int zona { get; set; }
        public List<Cliente> Clientes { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }

        public decimal ObtenerConsumo()
        {
            throw new NotImplementedException();
        }
    }
}
