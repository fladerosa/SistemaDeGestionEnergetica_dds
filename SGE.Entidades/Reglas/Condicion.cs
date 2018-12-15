using SGE.Entidades.Reglas.Operadores;
using SGE.Entidades.Sensores;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Entidades.Reglas {
    [Table("Condicion")]
    public class Condicion {
        [Key]
        public int CondicionId { get; set; }
        public int OperadorId { get; set; }
        [ForeignKey("OperadorId")]
        public Operador Operador { get; set; }
        public string ValorReferencia { get; set; }

        public int SensorId { get; set; }
        [ForeignKey("SensorId")]
        public Sensor Sensor { get; set; }

        public int ReglaId { get; set; } // fk con tabla regla
        [ForeignKey("ReglaId")]
        public Regla Regla { get; set; } // one to many con  regla 


        public bool Evaluar() {
            return this.Operador.Verificar(ValorReferencia, this.Sensor.Medir().Valor);
        }
    }
}
