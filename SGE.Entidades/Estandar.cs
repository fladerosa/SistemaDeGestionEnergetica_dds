using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades
{
    public class Estandar: Dispositivo{

        public Dispositivo Estedispositivo;
        
        public decimal ConsumoEnergia { get; set; }

        
        public override decimal ObtenerConsumoEnergia()
        {
            return this.Estedispositivo.ObtenerConsumoEnergia();
                
        }
       

    }   
    
}
