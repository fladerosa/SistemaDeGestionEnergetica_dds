using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Entidades.Dispositivos
{
 /*   [Table("EstadoDispositivo")]
    public class EstadoDispositivo
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(15)]
        public string Tipo { get; set; }

    }*/
    public enum EstadoDispositivo
    {
        Encendido,
        Apagado,
        AhorroEnergia
        //public virtual Activacion Activacion { get; set; } //one to one Activacion
        // public int InteligenteId { get; set; } //fk con tabla Inteligente
        // public Inteligente Inteligente { get; set; } // one to many con  Inteligente
        
    }
}
