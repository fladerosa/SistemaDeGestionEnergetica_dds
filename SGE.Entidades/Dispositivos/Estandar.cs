using SGE.Entidades.Usuarios;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Entidades.Dispositivos
{
    [Table(name: "Estandar")]
    public class Estandar : Dispositivo
    {
        public virtual List<Cliente> Clientes { get; set; } //many to many con Clientes
    //    public virtual List<Administrador> Administradores { get; set; } //many to many con Administrador
        #region Constructor
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
