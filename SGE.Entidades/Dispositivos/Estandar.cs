using SGE.Entidades.Usuarios;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Entidades.Dispositivos
{
    [Table(name: "Estandar")]
    public class Estandar : Dispositivo
    {
        public virtual ICollection<Usuario> Usuarios { get; set; } //many to many con Usuario

        #region Constructor
        public Estandar() {

        }
        public Estandar(string nombre, decimal consumo) : base(nombre, consumo)
        {

        }
        #endregion Constructor

        public decimal ConsumoAproximado(int horas)
        {
            return this.ConsumoEnergia * horas;
        }
    }
}
