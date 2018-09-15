using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Entidades.Usuarios
{
    public class TipoDocumento
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(15)]
        public string Tipo { get; set; }
        [MaxLength(40)]
        public string Descripcion { get; set; }

        public List<Cliente> Clientes { get; set; } // one to many con Cliente
        //TODO: no es necesario una relación uno a muchos, con un ENUM se resuelve, se deja, se plantea el enum y luego se debería eliminar esta clase
    }
}
