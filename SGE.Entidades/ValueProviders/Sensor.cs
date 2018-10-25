using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SGE.WebconAutenticacion.Dispositivos;
using SGE.WebconAutenticacion.Drivers.Interfaces;
using SGE.WebconAutenticacion.Reglas;

namespace SGE.WebconAutenticacion.ValueProviders
{
    [Table("Sensor")]
    public abstract class Sensor: IValueProvider
    {
        #region Campos
        public int Id { get; set; }
        public decimal ultimaMedicion { get; set; }
      
        public List<Inteligente> Inteligentes { get; set; } // one to many  
        public virtual ICollection<Medicion> Mediciones { get; set; } // one to many 

     //   public List<Inteligente> dispositivos = new List<Inteligente>();
        //TODO: porque tiene dos listados de dispositivos inteligentes?
        #endregion

        #region Observer

        public void Agregar(Inteligente dispositivo)
        {
            this.Inteligentes.Add(dispositivo);
        }

        public void Quitar(Inteligente dispositivo)
        {
            this.Inteligentes.Remove(dispositivo);
        }

        public void NotificarCambio()
        {
            foreach(Inteligente dispositivo in this.Inteligentes)
            {
                dispositivo.NotificarNuevaMedicion(this.ultimaMedicion);
            }
        }

        #endregion

        #region Medición

        public decimal Medir()
        {
            this.ultimaMedicion = this.RealizarMedicion();
            this.NotificarCambio();
            return this.ultimaMedicion;
        }

        public abstract decimal RealizarMedicion();

        #endregion

        #region Implementación de métodos IValueProvider

        public decimal ObtenerValor()
        {
            return this.ultimaMedicion;
        }

        #endregion
    }
}
