using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Entidades.Sensores {
    [Table("Medicion")]
    public class Medicion {
        public int Id { get; set; }
        public string Valor { get; set; }
        public UnidadEnum Unidad { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime FechaRegistro { get; set; }

        public int SensorId { get; set; } // fk con tabla Sensor
        [ForeignKey("SensorId")]
        public virtual Sensor Sensor { get; set; } // one to many con  Sensor

        public Medicion() {
            FechaRegistro = DateTime.Now;
        }
    }
}
