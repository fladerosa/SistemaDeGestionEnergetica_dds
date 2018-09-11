using Quartz;
using Quartz.Impl;
using SGE.Entidades.Dispositivos;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace SGE.Entidades.Simplex
{
    public class Planificador
    {
        #region Propiedades
        private IScheduler scheduler;
        private static Planificador me;
        private int CantidadTareas;
        #endregion Propiedades

        #region Constructores
        private Planificador()
        {
            this.CantidadTareas = 0;
            Iniciar().GetAwaiter().GetResult();
        }

        public static Planificador getInstance()
        {
            if (me == null) me = new Planificador();
            return me;
        }
        #endregion Constructores

        #region Funcionalidades
        public async Task Iniciar()
        {
            try
            {
                // Grab the Scheduler instance from the Factory
                NameValueCollection props = new NameValueCollection{ { "quartz.serializer.type", "binary" } };
                StdSchedulerFactory factory = new StdSchedulerFactory(props);
                this.scheduler = await factory.GetScheduler();

                // and start it off
                await scheduler.Start();
            }
            catch (SchedulerException se)
            {
                await Console.Error.WriteLineAsync(se.ToString());
            }
        }

        public async void agregarTarea(List<Inteligente> dispositivos, int intervalo)
        {
            Dictionary<string, List<Inteligente>> dic = new Dictionary<string, List<Inteligente>>();
            dic.Add("dispositivos", dispositivos);

            IJobDetail job = JobBuilder.Create<Tarea>()
                                          .WithIdentity("tarea" + this.CantidadTareas, "group" + this.CantidadTareas)
                                          .UsingJobData(new JobDataMap(dic))
                                          .Build();

            ITrigger trigger = TriggerBuilder.Create()
                                             .WithIdentity("trigger" + this.CantidadTareas, "group" + this.CantidadTareas)
                                             .StartNow()
                                             .WithSimpleSchedule(x => x.WithIntervalInSeconds(intervalo).RepeatForever())
                                             .Build();

            await this.scheduler.ScheduleJob(job, trigger);
        }

        public async void Terminar()
        {
            // and last shut down the scheduler when you are ready to close your program
            await scheduler.Shutdown();
        }
        #endregion Funcionalidades
    }
}
