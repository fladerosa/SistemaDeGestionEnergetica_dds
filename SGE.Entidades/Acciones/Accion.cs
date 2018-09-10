using SGE.Entidades.Reglas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Acciones
{
    [Table("Accion")]
    public abstract class Accion : IAccion
    {
        [Key]
        public int AccionId { get; set; }
        [MaxLength(15)]
        public string Tipo { get; set; }
        [MaxLength(40)]
        public string descripcion { get; set; }

        public virtual List<Condicion> Condiciones { get; set; }

        public void Ejecutar()
        {
        }
    }
}
