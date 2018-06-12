using System;
using System.Collections.Generic;

namespace SGE.Entidades.Reglas
{
    public class Regla
    {
        #region Propiedades
        public string Nombre { get; set; }
        public List<Condicion> Condiciones { get; set; }
        public List<Actuador> Actuadores { get; set; }

        #endregion Propiedades

        public Regla(String xNombre, List<Condicion> xCondiciones, List<Actuador> xActuadores)
        {
            this.Nombre = xNombre;
            this.Condiciones = xCondiciones;
            this.Actuadores = xActuadores;
        }


        #region Metodos
        public void Ejecutar()
        {
            //verifico que se cumpla todas las condiciones
            foreach (Condicion condicion in Condiciones)
                if (!condicion.Evaluar()) return;

            foreach (Actuador actuador in Actuadores)
                actuador.Ejecutar();
        }
       
        #endregion Metodos
    }
}
