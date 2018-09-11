using SGE.Entidades.Dispositivos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Entidades.Usuarios
{
    [Table(name: "Administrador")]
    public class Administrador : Usuario
    {
        //TODO: el cliente tambien tiene listados de dispositivos, no se podrían poner en la clase "Usuario"?
        //  public List<Inteligente> Inteligentes { get; set; } //many to many con Dispositivo
        //  public List<Estandar> Estandars { get; set; } //many to many con Dispositivo 
        public string Nui { get; set; }
        public int Antiguedad()
        {
            return (int)Math.Truncate((DateTime.Now - this.FechaAlta).TotalDays);
        }
    }
}
