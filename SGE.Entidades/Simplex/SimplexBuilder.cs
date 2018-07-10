using Microsoft.SolverFoundation.Services;
using System.Collections.Generic;
using System;

namespace SGE.Entidades.Simplex
{
    /// <summary>
    /// Clase encargada de resolver la optimizacion energetica del uso dedispositivos.
    /// Tener en cuenta que:
    ///     1) Los dispositivos disponibles son determinados exclusivamente por el sistema.
    ///     2) Todo dispositivo tiene su coeficiente de consumo energetico correspondiente.
    ///     3) Es imposible que el usuario ingrese una variable que no tenga coeficiente de consumo.
    /// </summary>
    public class SimplexBuilder
    {
        private double? ConsumoOptimo;
        private double MAXIMO_CO2 = 32.5;
        private double COEFICIENTE_CO2 = 0.076;

        private SolverContext Context;
        private Model Model;
        private int Minimos, Maximos;
        private List<string> Co2;
        private Dictionary<string, double> Coeficientes;
        public Dictionary<string, double> Resultado { get; private set; }

        public SimplexBuilder()
        {
            this.Context = SolverContext.GetContext();
            this.Model = Context.CreateModel();
            this.Minimos = 0;
            this.Maximos = 0;
            this.ConsumoOptimo = null;
            this.Co2 = new List<string>();
            this.CargarCoeficientes();
        }


        #region METODOS_PUBLICOS
        public SimplexBuilder AgregarConsumoOptimo(double valor)
        {
            this.ConsumoOptimo = valor;
            return this;
        }

        public SimplexBuilder AgregarRestriccionMinimo(string id, double valor)
        {
            foreach (Decision d in this.Model.Decisions)
                if (id.ToUpper().Equals(d.Name))
                {
                    this.Model.AddConstraint("Minimo" + this.Minimos++, d >= valor);
                    return this;
                }
            
            Decision decision = new Decision(Domain.RealNonnegative, id.ToUpper());
            this.Model.AddDecision(decision);
            this.Model.AddConstraint("Minimo" + this.Minimos++, decision >= valor);
            return this;
        }

        public SimplexBuilder AgregarRestriccionMaximo(string id, double valor)
        {
            foreach (Decision d in this.Model.Decisions)
                if (id.ToUpper().Equals(d.Name))
                {
                    this.Model.AddConstraint("Maximo" + this.Maximos++, d <= valor);
                    return this;
                }

            Decision decision = new Decision(Domain.RealNonnegative, id.ToUpper());
            this.Model.AddDecision(decision);
            this.Model.AddConstraint("Maximo" + this.Maximos++, decision <= valor);
            return this;
        }

        public SimplexBuilder AgregarDispositivoEmisorCO2(string id)
        {
            this.Co2.Add(id.ToUpper());
            return this;
        }

        public void Resolver()
        {
            List<Goal> listaObjetivos = new List<Goal>();

            if (this.ConsumoOptimo == null)
                throw new NotImplementedException();
            else
            {
                this.GenerarFuncionEconomica();

                Solution solution = Context.Solve(new SimplexDirective());
                listaObjetivos.AddRange(solution.Goals);
                Resultado = ObtenerConsumoMensuales(solution.Decisions);
                Resultado.Add("Total", listaObjetivos[0].ToDouble());
                
            }
        }
        #endregion METODOS_PUBLICOS



        #region METODOS_PRIVADOS

        private Dictionary<string, double> ObtenerConsumoMensuales(IEnumerable<Decision> decisiones)
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

        private void AgregarRestriccionCO2() // ESTO NO ME CIERRA
        {
            Term term = 0;
            foreach (Decision d in this.Model.Decisions)
                if (this.Co2.Contains(d.Name))
                    term += d * COEFICIENTE_CO2;
            term = term <= MAXIMO_CO2;
            this.Model.AddConstraint("MaximaEmisionCO2", term);
        }

        private void CargarCoeficientes()
        {
            //TODO: Esto se tiene que levantar desde un JSON a traves de un servicio en SGE.Core
            /*
             * DispositivosHelper helper = new DispositivosHelper();
             * 
             */

            this.Coeficientes = new Dictionary<string, double>();

            this.Coeficientes.Add("A1", 1.613);
            this.Coeficientes.Add("A2", 1.013);
            this.Coeficientes.Add("A3", 0.075);
            this.Coeficientes.Add("A4", 0.175);
            this.Coeficientes.Add("A5", 0.18);
            this.Coeficientes.Add("A6", 0.04);
            this.Coeficientes.Add("A7", 0.055);
            this.Coeficientes.Add("A8", 0.08);
            this.Coeficientes.Add("A9", 0.09);
            this.Coeficientes.Add("A10", 0.075);
            this.Coeficientes.Add("A11", 0.875);
            this.Coeficientes.Add("A12", 0.175);
            this.Coeficientes.Add("A13", 0.1275);
            this.Coeficientes.Add("A14", 0.09);
            this.Coeficientes.Add("A15", 0.06);
            this.Coeficientes.Add("A16", 0.04);
            this.Coeficientes.Add("A17", 0.06);
            this.Coeficientes.Add("A18", 0.15);
            this.Coeficientes.Add("A19", 0.11);
            this.Coeficientes.Add("A20", 0.015);
            this.Coeficientes.Add("A21", 0.02);
            this.Coeficientes.Add("A22", 0.075);
            this.Coeficientes.Add("A23", 0.99);
            this.Coeficientes.Add("A24", 0.122);
        }

        private void GenerarFuncionEconomica()
        {
            Term term = 0;
            foreach (Decision d in this.Model.Decisions)
                term += d * this.Coeficientes[d.Name];
            this.Model.AddConstraint("ConsumoMensualMaximo", term <= this.ConsumoOptimo);

            this.Model.AddGoal("Objetivo", GoalKind.Maximize, term);
        }

        #endregion METODOS_PRIVADOS
    }
}
