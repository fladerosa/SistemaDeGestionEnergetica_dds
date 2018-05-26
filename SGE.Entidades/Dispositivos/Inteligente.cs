using System;

namespace SGE.Entidades.Dispositivos
{
    public class Inteligente: Dispositivo
    {
        #region Propiedades
        /// <summary>
        /// Indica el estado del dispositivo
        /// </summary>
        protected EstadoDispositivo Estado = EstadoDispositivo.Encendido;
        public string IdentificadorFabrica { get; set; }

        /// <summary>
        /// Devuelve un valor que indica si el equipo esta encendido
        /// </summary>
        public bool EstaEncendido
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
        public bool EstaEnModoAhorro
        {
            get
            {
                return this.Estado == EstadoDispositivo.AhorroEnergia;
            }
        }
        #endregion Propiedades


        #region Constructor
        public Inteligente(string nombre, decimal consumo) : base(nombre, consumo)
        {

        }
        #endregion Constructor


        #region Funcionamiento
        /// <summary>
        /// Enciendo el equipo
        /// </summary>
        public void Encender()
        {
            if (this.Estado != EstadoDispositivo.Encendido)
            {
                this.Estado = EstadoDispositivo.Encendido;
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
            }
        }

        /// <summary>
        /// Coloca el dispositivo en modo ahorro energía
        /// </summary>
        public void CambiarModo()
        {
            this.Estado = EstadoDispositivo.AhorroEnergia;
        }

        public void SubirIntensidad()
        {
            throw new NotImplementedException();
        }

        public void BajarIntensidad()
        {
            throw new NotImplementedException();
        }
        #endregion Funcionamiento


        #region Estadisticas
        public decimal ObtenerConsumoUltimasHoras(int cantidadHoras)
        {
            return 0;
        }

        public decimal ObtenerConsumoPeriodo(DateTime fechaDesde, DateTime fechaHasta)
        {
            return 0;
        }
        #endregion Estadisticas
    }
}
