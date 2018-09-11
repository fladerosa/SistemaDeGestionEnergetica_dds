using SGE.Entidades.Usuarios;
using SGE.Entidades.Zonas;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SGE.Entidades.Transformadores
{
    [Table("Transformador")]
    public class Transformador
    {
        #region Propiedades
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
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
                consumo += cliente.Estandars.Sum(e => e.ConsumoEnergia);
                consumo += cliente.Inteligentes.Sum(i => i.ConsumoEnergia);
            }
            return consumo;
        }
        #endregion
    }
}
