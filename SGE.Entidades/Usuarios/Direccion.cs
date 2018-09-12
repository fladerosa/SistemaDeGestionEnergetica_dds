using SGE.Entidades.Zonas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Usuarios
{

    [Table("Direccion")]
    public class Direccion
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(25)]
        public string Calle { get; set; }
        [MaxLength(7)]
        public string Nro { get; set; }

        public virtual Usuario Usuario { get; set; } //one to one

    }
}
