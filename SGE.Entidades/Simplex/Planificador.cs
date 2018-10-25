using Quartz;
using Quartz.Impl;
using SGE.WebconAutenticacion.Dispositivos;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace SGE.WebconAutenticacion.Simplex
{
    public class Planificador
    {
        #region Propiedades
        private const string GRUPO = "0";
        private bool Estado;
        private int CantidadTareas;
        private IScheduler scheduler;
        private static Planificador instance;
        #endregion Propiedades

        #region Constructores
        private Planificador()
        {
            this.CantidadTareas = 0;
            this.Estado = false;
        }
        #endregion Constructores
        
        #region Funcionalidades
        public static Planificador getInstance()
        {
            if (instance == null) instance = new Planificador();
            return instance;
        }

        public async void Iniciar()
        {
            if (!this.Estado)
            {
                try
                {
                    NameValueCollection props = new NameValueCollection{ { "quartz.serializer.type", "binary" } };
                    StdSchedulerFactory factory = new StdSchedulerFactory(props);
                    this.scheduler = await factory.GetScheduler();
                    
                    await scheduler.Start();
                    this.Estado = true;
                }
                catch (SchedulerException se)
                {
                    //TODO: Resolver el manejo de excepciones con logs?
                    await Console.Error.WriteLineAsync(se.ToString());
                }
            }
        }

        public async void Detener()
        {
            if (this.Estado)
            {
                await scheduler.Clear();
                await scheduler.Shutdown();
                this.CantidadTareas = 0;
                this.Estado = false;
            }
        }

        public async void agregarTarea(List<Inteligente> dispositivos, int intervalo)
        {
            if (this.Estado)
            {
                Dictionary<string, List<Inteligente>> dic = new Dictionary<string, List<Inteligente>>();
                dic.Add("dispositivos", dispositivos);

                IJobDetail job = JobBuilder.Create<Tarea>()
                                              .WithIdentity(this.CantidadTareas.ToString(), GRUPO)
                                              .UsingJobData(new JobDataMap(dic))
                                              .Build();

                ITrigger trigger = TriggerBuilder.Create()
                                                 .WithIdentity(this.CantidadTareas.ToString(), GRUPO)
                                                 .StartNow()
                                                 .WithSimpleSchedule(x => x.WithIntervalInSeconds(intervalo).RepeatForever())
                                                 .Build();

                this.CantidadTareas++;

                await this.scheduler.ScheduleJob(job, trigger);
            }
        }
        public async void agregarTareaTest(List<Inteligente> dispositivos, int intervalo)
        {
            if (this.Estado)
            {
                Dictionary<string, List<Inteligente>> dic = new Dictionary<string, List<Inteligente>>();
                dic.Add("dispositivos", dispositivos);

                IJobDetail job = JobBuilder.Create<Tarea>()
                                              .WithIdentity(this.CantidadTareas.ToString(), GRUPO)
                                              .UsingJobData(new JobDataMap(dic))
                                              .Build();

                ITrigger trigger = TriggerBuilder.Create()
                                                 .WithIdentity(this.CantidadTareas.ToString(), GRUPO)
                                                 .StartNow()
                                                 .WithSimpleSchedule(x => x.WithRepeatCount(0))
                                                 .Build();

                this.CantidadTareas++;

                await this.scheduler.ScheduleJob(job, trigger);
            }
        }


        public async void TerminarTarea(string id)
        {
            if (this.Estado)
            {
                JobKey jb = new JobKey(id, GRUPO);
                await scheduler.DeleteJob(jb);
            }
        }
        #endregion Funcionalidades
    }
}
