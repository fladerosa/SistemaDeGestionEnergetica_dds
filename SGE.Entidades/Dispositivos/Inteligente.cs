using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using SGE.Entidades.Drivers.Interfaces;
using SGE.Entidades.Managers;
using SGE.Entidades.Usuarios;
using SGE.Entidades.ValueProviders;

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

        public List<Activacion> RegistroDeActivaciones { get; set; }
        public IDriver Driver { get; set; }
        public virtual List<Cliente> Clientes { get; set; } //many to many con Clientes
        public virtual List<Administrador> Administradores { get; set; } //many to many con Administrador
                                                                         //    public virtual List<Activacion> Activaciones { get; set; } //many to many con Activacion
        public int SensorId { get; set; } // fk con tabla Sensor
        public Sensor Sensor { get; set; } // one to many con  Sensor
        public int ActuadorId { get; set; } //fk con tabla Actuador
        public DispositivosManager Actuador { get; set; } // one to many con  Actuador

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
                this.RegistroDeActivaciones.Add(new Activacion(this.Estado));
            }
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
                this.RegistroDeActivaciones.Add(new Activacion(this.Estado));
            }
        }

        /// <summary>
        /// Coloca el dispositivo en modo ahorro energía
        /// </summary>
        public void ColocarEnAhorroEnergia()
        {
            this.Estado = EstadoDispositivo.AhorroEnergia;
            this.Driver.PonerEnModoAhorroEnergia();
            this.RegistroDeActivaciones.Add(new Activacion(this.Estado));
        }

        #endregion

        #region Estadisticas

        public decimal ObtenerConsumoDeUltimasNHoras(int cantidadHoras)
        {
            DateTime fechaBusqueda = (DateTime.Now).AddHours((-1) * cantidadHoras);
            List<Activacion> lista = this.RegistroDeActivaciones.Where(x => x.FechaDeRegistro >= fechaBusqueda).ToList<Activacion>();
            return this.ConsumoEnergia * this.CalcularHorasDeUso(lista);
        }

        public decimal ObtenerConsumoPeriodo(DateTime fechaDesde, DateTime fechaHasta)
        {
            List<Activacion> lista = this.RegistroDeActivaciones.Where(x => x.FechaDeRegistro >= fechaDesde && x.FechaDeRegistro <= fechaHasta).ToList<Activacion>();
            return this.ConsumoEnergia * this.CalcularHorasDeUso(lista);
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

            if (activacionAnterior.Estado != EstadoDispositivo.Apagado && flag)
                horas += Math.Abs(activacionAnterior.FechaDeRegistro.Subtract(DateTime.Now).Hours);

            return horas;
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
