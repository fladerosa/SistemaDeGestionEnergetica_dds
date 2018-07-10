using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Entidades.Drivers.Interfaces;
using SGE.Entidades.Reglas;

namespace SGE.Entidades.Drivers
{
    public class SamsungAireAcondicionadoDriver : IAireAcondicionadoDriver
    {
        #region Propiedades

        public string Mensaje { get; set; }

        private decimal temperaturaActual = 10;
        public decimal TemperaturaActual
        {
            get
            {
                return this.temperaturaActual;
            }
        }

        #endregion

        #region Acciones

        public void Apagar()
        {
            this.Mensaje = "Apagando...";
        }

        public void Encender()
        {
            this.Mensaje = "Encendiendo...";
        }


        public void PonerEnModoAhorroEnergia()
        {
            this.Mensaje = "Colocando en modo ahorro energía..";
        }


        public void AumentarVelocidadVentilador()
        {
            this.Mensaje = "Aumentando velocidad de ventilador...";
        }

        public void CambiarDireccion()
        {
            this.Mensaje = "Cambiando dirección aire...";
        }

        public void DecrementarVelocidadVentilador()
        {
            this.Mensaje = "Decrementando velocidad ventilador...";
        }


        public void EstablecerModoCool()
        {
            this.Mensaje = "Estableciendo modo cool...";
        }

        public void EstablecerModoDry()
        {
            this.Mensaje = "Estableciendo modo dry...";
        }

        public void EstablecerModoHeat()
        {
            this.Mensaje = "Estableciendo modo heat...";
        }

        public void EstablecerTemperatura(int valor)
        {
            this.Mensaje = "Estableciendo temperatura..." + valor.ToString();
        }

        #endregion

        #region Sensado

        public decimal ObtenerTemperaturaActual()
        {
            return this.temperaturaActual;
        }

        #endregion
    }
}
