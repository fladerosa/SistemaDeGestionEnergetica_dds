using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Entidades.Dispositivos {
    [Table("Activacion")]
    public class Activacion
    {
        public int Id { get; set; }
        public EstadoDispositivo Estado { get; set; }
        public DateTime FechaDeRegistro { get; set; }

        public int InteligenteId { get; set; } //fk con tabla Inteligente
        [ForeignKey("InteligenteId")]
        public virtual Inteligente Inteligente { get; set; } // one to many con  Inteligente

        public Activacion() {
        }

        public Activacion(EstadoDispositivo estado)
        {
            this.FechaDeRegistro = DateTime.Now;
            this.Estado = estado;
        }
    }
}
