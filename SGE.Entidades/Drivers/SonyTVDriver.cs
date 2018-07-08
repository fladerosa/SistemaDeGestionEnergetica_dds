using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Entidades.Drivers.Interfaces;
using SGE.Entidades.Reglas;

namespace SGE.Entidades.Drivers
{
    public class SonyTVDriver : ITelevisorDriver
    {
        public string Mensaje { get; set; }

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

        public void BajarVolumen()
        {
            this.Mensaje = "Bajando volúmen...";
        }

        public void CambiarCanal(decimal canal)
        {
            this.Mensaje = "Cambiando canal a..." + canal.ToString();
        }

        public void CambiarEntrada()
        {
            this.Mensaje = "Cambiando entrada TV...";
        }


        public void Mute()
        {
            this.Mensaje = "Muteando...";
        }

        public void SubirVolumen()
        {
            this.Mensaje = "Subiendo volúmen..";
        }
    }
}
