using Quartz;
using SGE.WebconAutenticacion.Dispositivos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGE.WebconAutenticacion.Simplex
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
                simplex.AgregarRestriccion(new KeyValuePair<string, string>(dispositivo.IdentificadorFabrica, dispositivo.Nombre), dispositivo.ObtenerCantidadDeHoraDeUsoMensual());

            simplex.Resolver();

            // Si da un consumo de energia elevado, pone los dispositivos en modo ahorro de energia
            if (simplex.Resultado["ConsumoRestanteTotal"] == 0)
            {
                foreach (Inteligente dispositivo in dispositivos)
                    dispositivo.Apagar();
            }
        }
    }
}
