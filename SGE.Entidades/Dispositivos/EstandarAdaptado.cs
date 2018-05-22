using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Dispositivos
{
    public class EstandarAdaptado
    {
        #region Campos

        /// <summary>
        /// Indica el estado del dispositivo
        /// </summary>
        private EstadoDispositivo Estado = EstadoDispositivo.Encendido;

        #endregion

        #region Propiedades

        /// <summary>
        /// Indica el nombre del dispositivo
        /// </summary>
        public string Nombre { get; set; }
       
        /// <summary>
        /// Devuelve el estado de energia del dispositivo
        /// </summary>
        public decimal ConsumoEnergia { get; set; }

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

        #endregion

        #region Estadísticas

        public decimal ObtenerConsumoEnergiaNHoras()
        {
            return 0;
        }

        #endregion
    }

}