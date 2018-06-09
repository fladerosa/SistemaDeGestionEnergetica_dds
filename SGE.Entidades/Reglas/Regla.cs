using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Reglas
{
    public class Regla
    {
        #region Propiedades
        public string Nombre { get; set; }
        public List<Condicion> Condiciones { get; set; }
        //mientras no exista la clase Actuador va con Condicion para que compile 
        public List<Condicion> Actuadores { get; set; }
        
        #endregion Propiedades


        #region Metodos
        public void Ejecutar()
        {
            //verifico que se cumpla todas las condiciones
            bool condicionesCumplidas=true;
            foreach (Condicion condicion in Condiciones)
            {
                condicionesCumplidas = condicion.Evaluar();
            }
            if (condicionesCumplidas)
            {
                foreach (var Actuador in Actuadores)
                {
                    
                }
            }
        }
       
        #endregion Metodos
    }
}
