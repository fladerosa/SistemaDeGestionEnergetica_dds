using SGE.Entidades.Acciones;
using SGE.Entidades.Sensores;
using SGE.Entidades.Usuarios;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Entidades.Dispositivos {
    [Table(name: "Catalogo")]
    public class Catalogo {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal ConsumoEnergia { get; set; }
        public string IdentificadorFabrica { get; set; }

        public int? AdministradorId { get; set; } // fk con tabla administrador
        [ForeignKey("AdministradorId")]
        public virtual Administrador Administrador { get; set; }

        public virtual ICollection<Accion> Acciones { get; set; }
        public virtual ICollection<Sensor> Sensores { get; set; }
    }

}
