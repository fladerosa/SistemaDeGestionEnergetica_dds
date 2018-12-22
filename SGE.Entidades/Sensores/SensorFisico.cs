using SGE.Entidades.Dispositivos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Entidades.Sensores {
    [Table("SensorFisico")]
    public class SensorFisico {
        public int Id { get; set; }
        public int IdDispositivo { get; set; }
        [ForeignKey("IdDispositivo")]
        public virtual Inteligente Dispositivo { get; set; }
        public int IdTipoSensor { get; set; }
        [ForeignKey("IdTipoSensor")]
        public virtual Sensor TipoSensor { get; set; }
        public virtual ICollection<Medicion> Mediciones { get; set; } // one to many 


        /// <summary>
        /// Función principal de medicion
        /// </summary>
        /// <returns>Medida</returns>
        public Medicion Medir() {
            return this.RealizarMedicion();
        }

        public virtual Medicion RealizarMedicion() {
            return TipoSensor.RealizarMedicion();
        }
    }
}
