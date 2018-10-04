using SGE.Entidades.ValueProviders;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Entidades.Reglas
{
    [Table("Medicion")]
    public class Medicion
    {
        #region Propiedades
        [Key]
        public int MedicionId { get; set; }
        public decimal Valor { get; set; }
        public UnidadEnum Unidad { get; set; }
        public DateTime FechaRegistro { get; set; }

        public int SensorId { get; set; } // fk con tabla Sensor
        [ForeignKey("SensorId")]
        public Sensor Sensor { get; set; } // one to many con  Sensor

        public int ? CondicionId { get; set; } // fk con tabla Condicion
        [ForeignKey("CondicionId")]
        public Condicion Condicion { get; set; } // one to many con Condicion

        #endregion

        #region Constructores

        public Medicion(decimal valor, UnidadEnum unidad)
        {
            this.Valor = valor;
            this.Unidad = unidad;
            this.FechaRegistro = DateTime.Now;
        }

        #endregion
    }
}
