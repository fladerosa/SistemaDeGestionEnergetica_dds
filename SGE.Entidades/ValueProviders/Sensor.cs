using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SGE.Entidades.Dispositivos;
using SGE.Entidades.Drivers.Interfaces;
using SGE.Entidades.Reglas;

namespace SGE.Entidades.ValueProviders
{
    [Table("Sensor")]
    public abstract class Sensor: IValueProvider
    {
        #region Campos
        [Key]
        public int SensorId { get; set; }
        public decimal ultimaMedicion { get; set; }
        public virtual Medicion Medicion { get; set; } //one to one
        public List<Inteligente> Inteligentes { get; set; } // one to many  
        public List<Medicion> Mediciones { get; set; } // one to many 

        public List<Inteligente> dispositivos = new List<Inteligente>();

        #endregion

        #region Observer

        public void Agregar(Inteligente dispositivo)
        {
            this.dispositivos.Add(dispositivo);
        }

        public void Quitar(Inteligente dispositivo)
        {
            this.dispositivos.Remove(dispositivo);
        }

        public void NotificarCambio()
        {
            foreach(Inteligente dispositivo in this.dispositivos)
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
