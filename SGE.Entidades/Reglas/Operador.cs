using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Entidades.Reglas {
    [Table("Operadores")]
    public abstract class Operador {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public virtual bool Verificar(string referencia, string valor) {
            return false;
        }
    }
}
