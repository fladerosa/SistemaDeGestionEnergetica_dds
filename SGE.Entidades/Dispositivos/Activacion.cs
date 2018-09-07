using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Entidades.Dispositivos
{
    [Table("Activacion")]
    public class Activacion
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        public EstadoDispositivo Estado { get; private set; }
        public DateTime FechaDeRegistro { get; set; }

        public int InteligenteId { get; set; } //fk con tabla Inteligente
        public Inteligente Inteligente { get; set; } // one to many con  Inteligente
  //      public virtual EstadoDispositivo EstadoDispositivo { get; set; } //one to one EstadoDispositivo

        public Activacion(EstadoDispositivo estado)
        {
            this.FechaDeRegistro = DateTime.Now;
            this.Estado = estado;
        }
    }
}
