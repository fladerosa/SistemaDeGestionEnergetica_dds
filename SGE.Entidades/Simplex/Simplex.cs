using Microsoft.SolverFoundation.Services;
using SGE.Entidades.Contexto;
using SGE.Entidades.Dispositivos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SGE.Entidades.Simplex {
    /// <summary>
    /// Clase encargada de resolver la optimizacion energetica del uso de dispositivos.
    /// Tener en cuenta que:
    ///     1) Los dispositivos disponibles son determinados exclusivamente por el sistema.
    ///     2) Todo dispositivo tiene su coeficiente de consumo energetico correspondiente.
    ///     3) Es imposible que el usuario ingrese una variable que no tenga coeficiente de consumo.
    /// </summary>
    public class SimplexNormal {
        private const double VALOR_OPTIMO_DEFAULT = 440640;
        private const double HORAS_MINIMAS = 0;
        private const double HORAS_MAXIMAS = 720;
        public SGEContext db = new SGEContext();

        private SolverContext Context { get; set; }
        private Model Model { get; set; }
        private int Minimos { get; set; }
        private int Maximos { get; set; }
        private double ConsumoTotal { get; set; }
        private Dictionary<string, double> Coeficientes { get; set; }
        private Dictionary<string, Inteligente> Dispositivos { get; set; }
        public Dictionary<string, double> Resultado { get; private set; }


        public SimplexNormal() {
            this.Context = SolverContext.GetContext();
            this.Model = Context.CreateModel();
            this.Minimos = 0;
            this.Maximos = 0;
            this.CargarCoeficientes();
            this.Dispositivos = new Dictionary<string, Inteligente>();
        }


        #region METODOS_PUBLICOS

        /// <summary>
        /// Agrega una restriccion minima y maxima para un determinado dispositivo.
        /// </summary>
        public SimplexNormal AgregarRestriccion(Inteligente dispositivo) {
            var horasEmpleadas = dispositivo.ObtenerCantidadDeHoraDeUsoMensual();
            if (horasEmpleadas >= HORAS_MINIMAS && horasEmpleadas <= HORAS_MAXIMAS) {
                var par = new KeyValuePair<string, Inteligente>(dispositivo.Nombre, dispositivo);
                this.Dispositivos.Add(par.Key, par.Value);

                Decision decision = new Decision(Domain.RealNonnegative, dispositivo.Nombre);
                this.Model.AddDecision(decision);
                this.Model.AddConstraint("Minimo" + this.Minimos++, decision >= HORAS_MINIMAS);
                this.Model.AddConstraint("Maximo" + this.Maximos++, decision <= HORAS_MAXIMAS - horasEmpleadas);
            } else {
                throw new Exception(); //TODO completar
            }
            return this;
        }

        /// <summary>
        /// Genera la solucion del modelo planteado, dejando el resultado en la propiedad correspondiente.
        /// </summary>
        public void Resolver() {
            this.AgregarRestriccionConsumoMensualMaximo();
            this.AgregarFuncionEconomica();

            Solution solution = Context.Solve(new SimplexDirective());
            List<Goal> listaObjetivos = new List<Goal>();
            listaObjetivos.AddRange(solution.Goals);
            Resultado = ParsearResultado(solution.Decisions);
            Resultado.Add("TotalHorasRestantes", listaObjetivos[0].ToDouble());
            Resultado.Add("ConsumoRestanteTotal", CalcularConsumoTotal(Resultado));
        }
        #endregion METODOS_PUBLICOS



        #region METODOS_PRIVADOS

        private void AgregarRestriccionConsumoMensualMaximo() {
            Term term = 0;
            foreach (Decision d in this.Model.Decisions)
                term += d * this.Coeficientes[this.Dispositivos[d.Name].Nombre];
            this.Model.AddConstraint("ConsumoMensualMaximo", term <= this.CalcularValorOptimo());
        }

        private void AgregarFuncionEconomica() {
            Term term = 0;
            foreach (Decision d in this.Model.Decisions)
                term += d;

            this.Model.AddGoal("Objetivo", GoalKind.Maximize, term);
        }

        private Dictionary<string, double> ParsearResultado(IEnumerable<Decision> decisiones) {
            Dictionary<string, double> resultado = new Dictionary<string, double>();
            string[] vec;

            foreach (Decision decision in decisiones) {
                vec = decision.ToString().Split('/');
                if (vec.Length == 1)
                    resultado.Add(decision.Name, Double.Parse(decision.ToString()));
                else
                    resultado.Add(decision.Name, Double.Parse(vec[0]) / Double.Parse(vec[1]));
            }

            return resultado;
        }

        private void CargarCoeficientes() {
            this.Coeficientes = new Dictionary<string, double>();


            foreach (Dispositivo d in db.Inteligentes.ToList())
                this.Coeficientes.Add(d.Nombre, (double)d.ConsumoEnergia);
        }

        private double CalcularConsumoTotal(Dictionary<string, double> resultado) {
            double consumo = 0;

            foreach (KeyValuePair<string, double> elemento in resultado) {
                if (!elemento.Key.Contains("TotalHorasRestantes")) {
                    consumo += elemento.Value * this.Coeficientes[this.Dispositivos[elemento.Key].Nombre];
                }
            }

            return consumo;
        }

        private double CalcularValorOptimo() {
            string[] vec;
            double consumo = 0;

            foreach (var con in this.Model.Constraints)
                if (con.Name.StartsWith("Maximo")) {
                    vec = con.Expression.Split(' ');
                    consumo += (720 - double.Parse(vec[2])) * this.Coeficientes[this.Dispositivos[vec[0]].Nombre];
                }

            return (VALOR_OPTIMO_DEFAULT - consumo) < 0 ? 0 : VALOR_OPTIMO_DEFAULT - consumo;
        }


        #endregion METODOS_PRIVADOS
    }
}
