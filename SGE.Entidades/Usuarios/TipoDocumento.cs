using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Entidades.Usuarios
{
    [Table("TipoDocumento")]
    public class TipoDocumento
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(15)]
        public string Tipo { get; set; }

        public virtual Cliente Cliente { get; set; } //one to one
    }
    /*  public enum TipoDocumento
    {
        DNI,
        CI,
        LE,
        LC
    }*/
}
