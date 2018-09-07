using SGE.Entidades.Categorias;
using SGE.Entidades.Dispositivos;
using System.Collections.Generic;
using System.Linq;
using SGE.Entidades.Usuarios;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Entidades
{
    [Table("Transformador")]
    public class Transformador
    {
        #region Propiedades
        public int Id { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public List<Cliente> Clientes { get; set; }
        public int ZonaId { get; set; } //fk con tabla Zona
        public Zona Zona { get; set; } // one to many con  Zona

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
