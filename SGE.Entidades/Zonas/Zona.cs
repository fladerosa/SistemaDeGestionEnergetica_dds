using SGE.WebconAutenticacion.Transformadores;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.WebconAutenticacion.Zonas {
    [Table("Zona")]
    public class Zona
    {
        #region Propiedades
        public int Id { get; set; }
        public int codigo { get; set; }
        [MaxLength(15)]
        public string Nombre { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public decimal Radio { get; set; }

        public virtual ICollection<Transformador> Transformadores { get; set; } //one to many con Transformador
   
        public Zona()
        {
            this.Transformadores = new List<Transformador>();
        }

        public decimal ObtenerConsumo()
        {
            decimal consumo = 0;
            foreach (Transformador transformador in Transformadores)
            {
                consumo += transformador.ObtenerConsumo();
            }
            return consumo;
        }

        #endregion
    }
}
