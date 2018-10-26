using SGE.Entidades.Drivers;
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
    //Esta clase se genera para establecer una Accion generica y poder mapear los diferentes tipos
    [Table("Accion")]
    public abstract class Accion : IAccion
    { 
        public int AccionId { get; set; }
        public string Descripcion { get; set; }

        public int ReglaId { get; set; } // fk con tabla regla
        public Regla Regla { get; set; } // one to many con  regla 
        public int ActuadorId { get; set; } //fk con Driver
        public Driver Actuador { get; set; } // one to many con Actuador (Driver)

        public void Ejecutar()
        {
        }
    }
}
