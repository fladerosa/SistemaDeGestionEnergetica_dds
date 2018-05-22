﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades
{
    public abstract class Comando:IComando
    {
        private string nombre;
        public string Estado { get; set; }

        public void EjecutarAccion()
        {
        }
        public void DeshacerAccion()
        {
        }
    }
}