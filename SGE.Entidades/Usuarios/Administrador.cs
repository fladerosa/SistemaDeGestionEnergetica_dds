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
        public string Nui { get; set; }
        //Segun la E0 los admin gestionan los clientes, no los dispositivos
        //segun el modelo de clases que se planteo en la E1 el cliente posee 
        //una lista de Dispositivos Inteligentes y otra lista de Dispositivos Estandar
        public int Antiguedad()
        {
            return (int)Math.Truncate((DateTime.Now - this.FechaAlta).TotalDays);
        }
    }
}
