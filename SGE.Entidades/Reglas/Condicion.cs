using SGE.Entidades.Acciones;
using SGE.Entidades.Reglas.Operadores;
using SGE.Entidades.ValueProviders;
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
        private decimal valorReferencia;

        public int ReglaId { get; set; } // fk con tabla regla
        [ForeignKey("ReglaId")]
        public Regla Regla { get; set; } // one to many con  regla 
        
        #endregion

        #region Constructores

        public Condicion(IValueProvider provider, IOperador operador, decimal valorReferencia)
        {
            this.provider = provider;
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
