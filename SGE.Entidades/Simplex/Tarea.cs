using Quartz;
using SGE.Entidades.Dispositivos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGE.Entidades.Simplex
{
    public class Tarea : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            // identificador de la tarea
            JobKey key = context.JobDetail.Key;

            // parametros
            List<Inteligente> dispositivos = (List<Inteligente>)context.MergedJobDataMap["dispositivos"];

            SimplexBuilder simplex = new SimplexBuilder();

            foreach (Inteligente dispositivo in dispositivos)
            {
                simplex.AgregarRestriccionMaximo(new KeyValuePair<string, string>(dispositivo.IdentificadorFabrica, dispositivo.Nombre), dispositivo.ObtenerCantidadDeHoraDeUsoMensual());
                // agregar minimo???? como??? de donde sale ese dato??
            }

            simplex.Resolver();

            // Si da un consumo de energia elevado, pone los dispositivos en modo ahorro de energia
            if ( !(simplex.Resultado["TotalConsumo"] > 0 && simplex.Resultado["TotalConsumo"] <= 440640))
            {
                foreach (Inteligente dispositivo in dispositivos)
                    dispositivo.ColocarEnAhorroEnergia();
            }
        }
    }
}
