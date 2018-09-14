using System.Collections.Generic;
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
        [MaxLength(40)]
        public string Descripcion { get; set; }

        public List<Cliente> Clientes { get; set; } // one to many con Cliente
    }
}
