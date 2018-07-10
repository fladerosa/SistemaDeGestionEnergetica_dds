using System;
using SGE.Entidades.Categorias;
using SGE.Entidades.Dispositivos;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace SGE.Entidades
{
    public class Zona
    {
        #region Propiedades

        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public decimal Radio { get; set; }
        public List<Transformador> Transformadores { get; set; }


        public Zona()
        {
            this.Transformadores = new List<Transformador>();
        }

        public decimal ObtenerConsumo()
        {
            decimal consumo = 0;
            foreach (Transformador transformador in Transformadores)
            {
                consumo = consumo + transformador.ObtenerConsumo();
            }
            return consumo;
        }

        #endregion
    }
}
