using SGE.Entidades.Usuarios;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Entidades.Categorias
{
    [Table("Categoria")]
    public class Categoria
    {
        [Key]
        public string Codigo { get; set; }
        public decimal ConsumoMinimo { get; set; }
        public decimal ConsumoMaximo { get; set; }
        public decimal CostoFijo { get; set; }
        public decimal CostoVariable { get; set; }

        public List<Cliente> Clientes { get; set; } // one to many con Categoria

        public bool ConsumoDentroDeLosLimites(decimal consumo)
        {
            return this.ConsumoMinimo < consumo && consumo < this.ConsumoMaximo;
        }

        public decimal CalcularFacturaMensual(decimal consumo)
        {
            return this.CostoFijo + (this.CostoVariable * consumo);
        }
    }
}
