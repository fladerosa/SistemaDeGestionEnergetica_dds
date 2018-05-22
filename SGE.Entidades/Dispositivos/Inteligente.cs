using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Dispositivos
{
    public class Inteligente: Dispositivo
    {   
        public decimal ConsumoEnergia { get; set; }
        public bool EstaEncendido { get; set; }
        public String indicador_fab { get; set; }
        public Inteligente()
        {
            this.EstaEncendido = false;
        }

        public void EncenderA()
        {
            if(this.EstaEncendido == false)
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
        public bool CambiarModo()
        {

            return this.EstaEncendido = true;
        }
        public override decimal ObtenerConsumoEnergia()
        {
            return this.ConsumoEnergia;
        }
     
    }

}

