using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Dispositivos
{
    public class EstandarAdaptado
    {
        public decimal ConsumoEnergia { get; set; }
        public bool EstaEncendido { get; set; }
        public string Nombre { get; set; }
        public EstandarAdaptado()
        {
            this.EstaEncendido = false;
        }

        public void EncenderA()
        {
            if (this.EstaEncendido == false)
            {
                this.EstaEncendido = true;
            }
        }

        public void ApagarA()
        {
            if (this.EstaEncendido == true)
            {
                this.EstaEncendido = false;
            }
        }

        public bool EstaPrendido()
        {
            return this.EstaEncendido == true;
        }
        public bool EstaApagado()
        {
            return this.EstaEncendido == false;
        }
        public bool cambiarModo()
        {

            return this.EstaEncendido = true;
        }
        public  decimal obtenerConsumoEnergia()
        {
            return this.ConsumoEnergia;
        }

        public void mostrar_dispositivoAdaptado()
        {
            Console.WriteLine("\nDispositivo:  ------ ", Nombre);
        }
    }

}