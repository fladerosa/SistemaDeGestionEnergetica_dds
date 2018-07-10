using SGE.Entidades.Reglas.Operadores;
using SGE.Entidades.Reglas.ValueProviders;

namespace SGE.Entidades.Reglas
{
    public class Condicion
    {
        #region Propiedades

        private IValueProvider provider;
        private IOperador operador;
        private decimal valorReferencia;

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
