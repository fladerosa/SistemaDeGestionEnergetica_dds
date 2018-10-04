using SGE.Entidades.Acciones;
using SGE.Entidades.Reglas.Operadores;
using SGE.Entidades.ValueProviders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Entidades.Reglas
{
    [Table("Condicion")]
    public class Condicion
    {
        #region Propiedades
        [Key]
        public int CondicionId { get; set; }
        private IValueProvider provider;
        private IOperador operador;
        public decimal valorReferencia { get; set; }

        public int ReglaId { get; set; } // fk con tabla regla
        [ForeignKey("ReglaId")]
        public Regla Regla { get; set; } // one to many con  regla 

        public virtual ICollection<Medicion> Mediciones { get; set; }
        #endregion

        #region Constructores

        public Condicion(IValueProvider provider, IOperador operador, decimal valorReferencia)
        {
            this.provider = provider;
            this.operador = operador;
            this.valorReferencia = valorReferencia;
        }

        public Condicion(IOperador operador, decimal valorReferencia)
        {
            this.operador = operador;
            this.valorReferencia = valorReferencia;
        }


        #endregion

        #region Evaluación

        public bool Evaluar()
        {
            decimal valor = this.provider.ObtenerValor();
            return this.operador.Verificar(valorReferencia, valor);
        }

        #endregion
    }
}
