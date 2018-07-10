using SGE.Entidades.Categorias;
using SGE.Entidades.Dispositivos;
using System.Collections.Generic;
using System.Linq;
using SGE.Entidades.Usuarios;

namespace SGE.Entidades
{
    public class Transformador
    {
        #region Propiedades
        public int Id { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public List<Cliente> Clientes { get; set; }

        public Transformador()
        {
            this.Clientes = new List<Cliente>();
        }

        public decimal ObtenerConsumo()
        {
            decimal consumo = 0;
            foreach (Cliente cliente in Clientes)
            {
                consumo = consumo;
            }
            return consumo;
        }
        #endregion
    }
}
