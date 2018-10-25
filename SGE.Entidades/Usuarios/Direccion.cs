using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.WebconAutenticacion.Usuarios {
    [Table("Direccion")]
    public class Direccion
    {
        [Key, ForeignKey("Usuario")]
        public int Id { get; set; }
        [MaxLength(25)]
        public string Calle { get; set; }
        [MaxLength(7)]
        public string Nro { get; set; }

        public virtual Usuario Usuario { get; set; } //one to one

    }
}
