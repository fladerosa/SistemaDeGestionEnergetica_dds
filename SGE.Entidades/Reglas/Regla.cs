using System;
using System.Collections.Generic;
using SGE.Entidades.Acciones;

namespace SGE.Entidades.Reglas
{
    public class Regla
    {
        #region Propiedades

        public string Nombre { get; set; }
        public List<Condicion> Condiciones { get; set; }
        public List<IAccion> Acciones { get; set; }

        #endregion Propiedades

        #region Constructores

        public Regla(String nombre, List<Condicion> condiciones, List<IAccion> acciones)
        {
            this.Nombre = nombre;
            this.Condiciones = condiciones;
            this.Acciones = acciones;
        }

        #endregion

        #region Metodos

        public void Ejecutar()
        {
            //verifico que se cumpla todas las condiciones
            foreach (Condicion condicion in Condiciones)
                if (!condicion.Evaluar()) return;

            foreach (Actuador actuador in Acciones)
                actuador.Ejecutar();
        }
       
        #endregion Metodos
    }
}
