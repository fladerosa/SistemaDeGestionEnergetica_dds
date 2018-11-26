using SGE.Entidades.Dispositivos;
using SGE.Entidades.Reglas;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Entidades.Acciones {
    //Esta clase se genera para establecer una Accion generica y poder mapear los diferentes tipos
    [Table("Accion")]
    public abstract class Accion {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Regla> Reglas { get; set; }

        public virtual ICollection<Catalogo> Catalogos { get; set; }

        [NotMapped]
        public Inteligente Dispositivo { get; set; }

        public virtual void Ejecutar() { }
        public virtual void Ejecutar(string valor) { }
    }
}