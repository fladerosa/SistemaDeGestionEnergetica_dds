using SGE.Entidades.Dispositivos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Entidades.Sensores {
    [Table("Sensor")]
    public class Sensor {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        [NotMapped]
        public virtual Inteligente Dispositivo { get; set; }
        public virtual ICollection<Catalogo> Catalogos { get; set; }
        public virtual ICollection<SensorFisico> SensoresFisicos { get; set; }

        public Sensor() {
            this.Catalogos = new List<Catalogo>();
        }

        public virtual Medicion RealizarMedicion() {
            return null;
        }
    }
}
