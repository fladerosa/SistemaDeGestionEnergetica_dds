using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Entidades.Sensores {
    [Table("Medicion")]
    public class Medicion {
        public int Id { get; set; }
        public string Valor { get; set; }
        public UnidadEnum Unidad { get; set; }
        public DateTime FechaRegistro { get; set; }

        public int SensorId { get; set; } // fk con tabla Sensor
        [ForeignKey("SensorId")]
        [InverseProperty("Mediciones")]
        public virtual SensorFisico Sensor { get; set; } // one to many con  Sensor

        public Medicion() {
            FechaRegistro = DateTime.Now;
        }
    }
}
