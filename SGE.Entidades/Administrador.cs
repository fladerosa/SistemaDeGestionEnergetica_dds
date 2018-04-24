using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades
{
    public class Administrador: Usuario
    {
        public int Antiguedad()
        {
            return (int)Math.Truncate((DateTime.Now - this.FechaAlta).TotalDays);
        }
    }
}
