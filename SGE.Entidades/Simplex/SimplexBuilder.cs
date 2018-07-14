using Microsoft.SolverFoundation.Services;
using SGE.Core.Entidades;
using SGE.Core.Helpers;
using System;
using System.Collections.Generic;

namespace SGE.Entidades.Simplex
{
    /// <summary>
    /// Clase encargada de resolver la optimizacion energetica del uso de dispositivos.
    /// Tener en cuenta que:
    ///     1) Los dispositivos disponibles son determinados exclusivamente por el sistema.
    ///     2) Todo dispositivo tiene su coeficiente de consumo energetico correspondiente.
    ///     3) Es imposible que el usuario ingrese una variable que no tenga coeficiente de consumo.
    /// </summary>
    public class SimplexBuilder
    {
        private double? ValorOptimo { get; set; }
        private SolverContext Context { get; set; }
        private Model Model { get; set; }
        private int Minimos { get; set; }
        private int Maximos { get; set; }
        private Dictionary<string, double> Coeficientes { get; set; }
        public Dictionary<string, double> Resultado { get; private set; }


        public SimplexBuilder()
        {
            this.Context = SolverContext.GetContext();
            this.Model = Context.CreateModel();
            this.Minimos = 0;
            this.Maximos = 0;
            this.ValorOptimo = null;
            this.CargarCoeficientes();
        }


        #region METODOS_PUBLICOS

        /// <summary>
        /// Agrega el valor optimo para la restriccion de consumo mensual.
        /// </summary>
        public SimplexBuilder AgregarValorOptimo(double valor)
        {
            this.ValorOptimo = valor;
            return this;
        }

        /// <summary>
        /// Agrega una restriccion minima para un determinado dispositivo.
        /// En caso de que el identificador del dispositivo suministrado no exista en el modelo, se agrega y luego se agrega la restriccion minima.
        /// </summary>
        public SimplexBuilder AgregarRestriccionMinimo(string id, double valor)
        {
            // Busca el ID del dispositivo en la lista de variables y agrega una restriccion.
            foreach (Decision d in this.Model.Decisions)
                if (id.ToUpper().Equals(d.Name))
                {
                    this.Model.AddConstraint("Minimo" + this.Minimos++, d >= valor);
                    return this;
                }

            // De no encontrar el dipositivo, lo agrega como nueva variable y agrega una restriccion.
            Decision decision = new Decision(Domain.RealNonnegative, id.ToUpper());
            this.Model.AddDecision(decision);
            this.Model.AddConstraint("Minimo" + this.Minimos++, decision >= valor);
            return this;
        }

        /// <summary>
        /// Agrega una restriccion maxima para un determinado dispositivo.
        /// En caso de que el identificador del dispositivo suministrado no exista en el modelo, se agrega y luego se agrega la restriccion maxima.
        /// </summary>
        public SimplexBuilder AgregarRestriccionMaximo(string id, double valor)
        {
            // Busca el ID del dispositivo en la lista de variables y agrega una restriccion.
            foreach (Decision d in this.Model.Decisions)
                if (id.ToUpper().Equals(d.Name))
                {
                    this.Model.AddConstraint("Maximo" + this.Maximos++, d <= valor);
                    return this;
                }

            // De no encontrar el dipositivo, lo agrega como nueva variable y agrega una restriccion.
            Decision decision = new Decision(Domain.RealNonnegative, id.ToUpper());
            this.Model.AddDecision(decision);
            this.Model.AddConstraint("Maximo" + this.Maximos++, decision <= valor);
            return this;
        }

        /// <summary>
        /// Genera la solucion del modelo planteado, dejando el resultado en la propiedad correspondiente.
        /// </summary>
        public void Resolver()
        {
            if (this.ValorOptimo == null)
                throw new Exception("Debe definir el valor de la funcion economica.");
            else
            {
                this.AgregarRestriccionExtremosAbsolutos();
                this.AgregarRestriccionConsumoMensualMaximo();
                this.AgregarFuncionEconomica();

                Solution solution = Context.Solve(new SimplexDirective());
                List<Goal> listaObjetivos = new List<Goal>();
                listaObjetivos.AddRange(solution.Goals);
                Resultado = ParsearResultado(solution.Decisions);
                Resultado.Add("Total", listaObjetivos[0].ToDouble());
            }
        }

        #endregion METODOS_PUBLICOS



        #region METODOS_PRIVADOS

        private void AgregarRestriccionExtremosAbsolutos()
        {
            int max = 0, min = 0;
            // Por cada variable agrego una restriccion minima y maxima absolutas.
            // Ya que nunca se puede, por mes, dar un uso < 0hs o > 720hs
            foreach (Decision d in this.Model.Decisions)
            {
                this.Model.AddConstraint("MinimoAbsoluto" + min++, d >= 0);
                this.Model.AddConstraint("MaximoAbsoluto" + max++, d <= 720);
            }
        }

        private void AgregarRestriccionConsumoMensualMaximo()
        {
            Term term = 0;
            foreach (Decision d in this.Model.Decisions)
                term += d * this.Coeficientes[d.Name];
            this.Model.AddConstraint("ConsumoMensualMaximo", term <= this.ValorOptimo);
        }

        private void AgregarFuncionEconomica()
        {
            Term term = 0;
            foreach (Decision d in this.Model.Decisions)
                term += d * this.Coeficientes[d.Name];

            this.Model.AddGoal("Objetivo", GoalKind.Maximize, term);
        }

        private Dictionary<string, double> ParsearResultado(IEnumerable<Decision> decisiones)
        {
            Dictionary<string, double> resultado = new Dictionary<string, double>();
            string[] vec;

            foreach (Decision decision in decisiones)
            {
                vec = decision.ToString().Split('/');
                if (vec.Length == 1)
                    resultado.Add(decision.Name, Double.Parse(decision.ToString()));
                else
                    resultado.Add(decision.Name, Double.Parse(vec[0]) / Double.Parse(vec[1]));
            }

            return resultado;
        }

        private void CargarCoeficientes()
        {
            this.Coeficientes = new Dictionary<string, double>();

            foreach(Dispositivo d in DispositivosHelper.GetInstace().Dispositivos)
                this.Coeficientes.Add(d.Id.ToUpper(), d.Consumo);
        }


        #endregion METODOS_PRIVADOS
    }
}
