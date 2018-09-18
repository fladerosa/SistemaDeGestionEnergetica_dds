using SGE.Entidades.Drivers;
using SGE.Entidades.Drivers.Interfaces;
using SGE.Entidades.Managers;
using SGE.Entidades.Usuarios;
using SGE.Entidades.ValueProviders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SGE.Entidades.Dispositivos
{
    [Table(name: "Inteligente")]
    public class Inteligente : Dispositivo
    {
        #region Propiedades

        DispositivosManager dispositivosManager;

        /// <summary>
        /// Indica el estado del dispositivo
        /// </summary>
        protected EstadoDispositivo Estado = EstadoDispositivo.Apagado;
        public virtual ICollection<Usuario> Usuarios { get; set; } //many to many con Usuarios
        public virtual ICollection<Activacion> RegistroDeActivaciones { get; set; }
        public IDriver Driver { get; set; }
       
        public int SensorId { get; set; } // fk con tabla Sensor
        [ForeignKey("SensorId")]
        public virtual Sensor Sensor { get; set; } // one to many con  Sensor
        public int ActuadorId { get; set; } //fk con tabla Actuador
        [ForeignKey("ActuadorId")]
        public virtual Driver Actuador { get; set; } // one to many con  Actuador

        /// <summary>
        /// Devuelve un valor que indica si el equipo esta encendido
        /// </summary>
        public bool EstaPrendido
        {
            get
            {
                return this.Estado == EstadoDispositivo.Encendido;
            }
        }

        /// <summary>
        /// Devuelve un valor que indica si el equipo esta encendido
        /// </summary>
        public bool EstaApagado
        {
            get
            {
                return this.Estado == EstadoDispositivo.Apagado;
            }
        }

        /// <summary>
        /// Devuelve un valor que indicasiel equipo esta en modo ahorro de energia
        /// </summary>
        public bool EstaEnModoAhorroEnergia
        {
            get
            {
                return this.Estado == EstadoDispositivo.AhorroEnergia;
            }
        }

        #endregion

        #region Constructor

        public Inteligente() {
        }

        public Inteligente(string nombre, decimal consumo, IDriver driver) : base(nombre, consumo)
        {
            this.RegistroDeActivaciones = new List<Activacion>();
            this.Driver = driver;
        }

        #endregion

        #region Funcionamiento

        /// <summary>
        /// Enciendo el equipo
        /// </summary>
        public void Encender()
        {
            if (this.Estado != EstadoDispositivo.Encendido)
            {
                this.Estado = EstadoDispositivo.Encendido;
                this.Driver.Encender();
                Activacion activacion = new Activacion() {
                    Estado = this.Estado,
                    Inteligente = this,
                    FechaDeRegistro = DateTime.Now,
                    InteligenteId = this.Id
                };

                this.RegistroDeActivaciones.Add(activacion);
            }
        }

        public List<string> ObtenerIntervalosEncendidoPorPeriodo(DateTime fechaDesde, DateTime fechaHasta) {
            List<string> intervalosEncendido = new List<string>();

            List<Activacion> activacionesDentroPeriodo = this.RegistroDeActivaciones.Where(a => a.FechaDeRegistro >= fechaDesde && a.FechaDeRegistro <= fechaHasta).ToList();
            Activacion activacion = null;

            for (var i = 0; i < activacionesDentroPeriodo.Count; i++) {
                activacion = activacionesDentroPeriodo[i];
                if(activacion.Estado == EstadoDispositivo.Encendido) {
                    //Busco la posicion siguiente para saber cuando se apagó, si no la encuentro asumo que nunca se apagó, por lo que asumo que sigue encendido hasta la fecha máxima indicada
                    if(activacionesDentroPeriodo.ElementAtOrDefault(i+1) != null) {
                        intervalosEncendido.Add("Encendido desde '" + activacion.FechaDeRegistro.ToShortDateString() + "' hasta '" + activacionesDentroPeriodo[i + 1].FechaDeRegistro.ToShortDateString());
                    } else {
                        intervalosEncendido.Add("Encendido desde '" + activacion.FechaDeRegistro.ToShortDateString() + "' hasta (al menos) '" + fechaHasta.ToShortDateString());
                    }
                }
            }

            return intervalosEncendido;
        }

        /// <summary>
        /// Apaga el equipo
        /// </summary>
        public void Apagar()
        {
            if (this.Estado != EstadoDispositivo.Apagado && this.Estado != EstadoDispositivo.AhorroEnergia)
            {
                this.Estado = EstadoDispositivo.Apagado;
                this.Driver.Apagar();
                this.RegistroDeActivaciones.Add(new Activacion(this.Estado) {
                    Inteligente = this,
                    FechaDeRegistro = DateTime.Now,
                    InteligenteId = this.Id
                });
            }
        }

        /// <summary>
        /// Coloca el dispositivo en modo ahorro energía
        /// </summary>
        public void ColocarEnAhorroEnergia()
        {
            this.Estado = EstadoDispositivo.AhorroEnergia;
            this.Driver.PonerEnModoAhorroEnergia();
            this.RegistroDeActivaciones.Add(new Activacion(this.Estado) {
                Inteligente = this,
                FechaDeRegistro = DateTime.Now,
                InteligenteId = this.Id
            });
        }

        #endregion

        #region Estadisticas

        public decimal ObtenerConsumoDeUltimasNHoras(int cantidadHoras)
        {
            DateTime fechaBusqueda = (DateTime.Now).AddHours((-1) * cantidadHoras);
            if (this.RegistroDeActivaciones != null && this.RegistroDeActivaciones.Count > 0) {
                List<Activacion> lista = this.RegistroDeActivaciones.Where(x => x.FechaDeRegistro >= fechaBusqueda).ToList<Activacion>();
                return this.ConsumoEnergia * this.CalcularHorasDeUso(lista);
            }
            return 0;
        }

        public decimal ObtenerConsumoPeriodo(DateTime fechaDesde, DateTime fechaHasta)
        {
            if(this.RegistroDeActivaciones != null && this.RegistroDeActivaciones.Count > 0) {
                List<Activacion> lista = this.RegistroDeActivaciones.Where(x => x.FechaDeRegistro >= fechaDesde && x.FechaDeRegistro <= fechaHasta).ToList<Activacion>();
                return this.ConsumoEnergia * this.CalcularHorasDeUso(lista);
            }
            return 0;
        }

        private decimal CalcularHorasDeUso(List<Activacion> lista)
        {
            int horas = 0;
            bool flag = false;
            Activacion activacionAnterior = null;
            
            foreach (Activacion registro in lista)
            {
                if (registro.Estado != EstadoDispositivo.Apagado)
                {
                    activacionAnterior = registro;
                    flag = true;
                }
                else if (registro.Estado == EstadoDispositivo.Apagado && flag)
                {
                    horas += Math.Abs(registro.FechaDeRegistro.Subtract(activacionAnterior.FechaDeRegistro).Hours);
                    flag = false;
                }
            }

            if (activacionAnterior != null && activacionAnterior.Estado != EstadoDispositivo.Apagado && flag)
                horas += Math.Abs(activacionAnterior.FechaDeRegistro.Subtract(DateTime.Now).Hours);

            return horas;
        }


        public double ObtenerCantidadDeHoraDeUsoMensual()
        {
            return 10; //TODO todo...
        }
        #endregion

        #region Nuevas Mediciones

        public void NotificarNuevaMedicion(decimal valor)
        {

        }

        #endregion

        #region Observer

        public void Agregar(DispositivosManager manager)
        {
            this.dispositivosManager = manager;
        }

        public void Quitar(Inteligente dispositivo)
        {
            this.dispositivosManager = null;
        }

        public void NotificarMedicion(decimal valor)
        {
            DispositivosManager.Instance.NotificarNuevaMedicion(this);
        }

        #endregion
    }
}
