using SGE.Entidades.Dispositivos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Entidades.Sensores {
    [Table("Sensor")]
    public class Sensor {
        #region Campos
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public virtual Medicion UltimaMedicion { get; set; }
        [NotMapped]
        public virtual Inteligente Dispositivo { get; set; }

        public virtual ICollection<Catalogo> Catalogos { get; set; }
        public virtual ICollection<Medicion> Mediciones { get; set; } // one to many 
        #endregion

        #region Observer
        public void NotificarCambio() {
            Dispositivo.NotificarNuevaMedicion(this.UltimaMedicion.Valor);
        }

        #endregion

        #region Medición
        /// <summary>
        /// Función principal de medicion
        /// </summary>
        /// <returns>Medida</returns>
        public Medicion Medir() {
            this.UltimaMedicion = this.RealizarMedicion();
            this.NotificarCambio();
            return this.UltimaMedicion;
        }

        public virtual Medicion RealizarMedicion() {
            return null;
        }

        #endregion
    }
}
