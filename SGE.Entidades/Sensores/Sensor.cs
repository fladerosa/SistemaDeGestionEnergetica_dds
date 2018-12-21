using SGE.Entidades.Dispositivos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Entidades.Sensores {
    [Table("Sensor")]
    public class Sensor {
        #region Campos
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Catalogo> Catalogos { get; set; }
        #endregion
    }
}
