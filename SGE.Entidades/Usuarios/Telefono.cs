using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.WebconAutenticacion.Usuarios
{
    [Table("Telefono")]
    public class Telefono
    {
        public int Id { get; set; }
        [MaxLength(16)]
        public string Numero { get; set; }

        public int ClienteId { get; set; } //fk con tabla cliente
        [ForeignKey("ClienteId")]
        public virtual Cliente Cliente { get; set; } // one to many con  Cliente
    }
}
