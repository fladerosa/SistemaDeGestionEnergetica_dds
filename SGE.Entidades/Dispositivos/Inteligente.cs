using System;

namespace SGE.Entidades.Dispositivos
{
    public class Inteligente: Dispositivo
    {
        #region Campos

        /// <summary>
        /// Indica el estado del dispositivo
        /// </summary>
        private EstadoDispositivo Estado = EstadoDispositivo.Encendido;

        #endregion Campos

        #region Propiedades

        /// <summary>
        /// Indica el nombre del dispositivo
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Devuelve el estado de energia del dispositivo
        /// </summary>
        public override decimal ConsumoEnergia { get; set; }

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

        public string IdentificadorFabrica { get; set; }

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

        public EstadoDispositivo ObtenerEstado()
        {
            return this.Estado;
        }

        #endregion

        #region Estadísticas

        public decimal ObtenerConsumoUltimasHoras(int cantidadHoras)
        {
            return 0;
        }

        public decimal ObtenerConsumoPeriodo(DateTime fechaDesde, DateTime fechaHasta)
        {
            return 0;
        }

        #endregion
    }

}

