using SGE.Entidades.Acciones;
using SGE.Entidades.Dispositivos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SGE.Entidades.Reglas {
    [Table("Regla")]
    public class Regla {
        [Key]
        public int ReglaId { get; set; }
        [MaxLength(25)]
        public string Nombre { get; set; }
        [ForeignKey("Inteligente")]
        public int IdInteligente { get; set; }
        public virtual Inteligente Inteligente { get; set; }

        public virtual ICollection<Accion> Acciones { get; set; } //one to many con Accion  
        public virtual ICollection<Condicion> Condiciones { get; set; } //one to many con condicion

        public Regla() {
            this.Acciones = new List<Accion>();
            this.Condiciones = new List<Condicion>();
        }

        public void Ejecutar() {
            if(Condiciones.All(c => c.Evaluar())) {
                foreach (Accion accion in this.Acciones) {
                    accion.Ejecutar();
                }
            }
        }
    }
}
