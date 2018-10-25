using SGE.WebconAutenticacion.Acciones;
using SGE.WebconAutenticacion.Dispositivos;
using SGE.WebconAutenticacion.Drivers.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.WebconAutenticacion.Drivers {
    //Se crea esta clase abstracta Driver para poder establecer un Actuador generico y cuando se realice la carga, se puede elegir entre los disponibles (TV, Lavarropas, AA)
    // Todo : Habria que evaluar una mejor forma de establecer los actuadores

    [Table("Actuador")]
    public  abstract class Driver : IAireAcondicionadoDriver, ITelevisorDriver, ILavarropasDriver
    {
        public int Id { get; set; }
        public string Mensaje { get; set; }
        public decimal temperaturaActual;

        public virtual ICollection<Inteligente> Inteligentes { get; set; } // one to many con Inteligente
        public List<Accion> Acciones { get; set; } // one to many con Accion
        // interface Idriver
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

        //interface Lavarropas
        public void LavarRopaAlgodon()
        {
            this.Mensaje = "Iniciando lavado de ropa de algodón...";
        }

        // interface televisor
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

        //interface Aire acondicionado

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
        public decimal ObtenerTemperaturaActual()
        {
            return this.temperaturaActual;
        }
    }
}
