using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades
{
    public class Inteligente: Dispositivo
    {
        public Inteligente()
        {
            this.EstaEncendido = false;
        }

        public decimal ConsumoEnergia { get; set; }
        public bool EstaEncendido { get; set; }

        public void EncenderA()
        {
            this.EstaEncendido = !this.EstaEncendido;
        }

        public bool EstaPrendido()
        {
            return this.EstaEncendido;
        }

        public override decimal obtenerConsumoEnergia()
        {
            return this.ConsumoEnergia;
        }
     
    }

}

