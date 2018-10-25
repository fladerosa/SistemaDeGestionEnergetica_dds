using SGE.WebconAutenticacion.Usuarios;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.WebconAutenticacion.Dispositivos
{
    [Table(name: "Estandar")]
    public class Estandar : Dispositivo
    {
        public virtual ICollection<Cliente> Clientes { get; set; } //many to many con Cliente

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
