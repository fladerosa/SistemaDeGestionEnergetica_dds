using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGE.WebconAutenticacion;
using SGE.WebconAutenticacion.Acciones;
using SGE.WebconAutenticacion.Actuadores;
using SGE.WebconAutenticacion.Dispositivos;
using SGE.WebconAutenticacion.Drivers;
using SGE.WebconAutenticacion.Reglas;
using SGE.WebconAutenticacion.Reglas.Operadores;
using SGE.WebconAutenticacion.ValueProviders;

namespace SGE.Tests.Reglas
{
    [TestClass]
    public class ReglaTest
    {

        private List<Condicion> condiciones { get; set; }
        private List<IAccion> acciones { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            this.condiciones = CrearCondiciones();
            this.acciones = CrearAcciones();
        }


        [TestMethod()]
        public void EjecutarTest()
        {
            Regla regla= new Regla("ReglaCalorEncenderAire", this.condiciones, this.acciones);
            regla.Ejecutar();
        }


        private List<Condicion> CrearCondiciones()
        {
            List<Condicion> returnCondiciones = new List<Condicion>
            {
                new Condicion(new SensorTemperaturaAA(new SamsungAireAcondicionadoDriver()), new Mayor(), 30)
            };
            return returnCondiciones;
        }
        private List<IAccion> CrearAcciones()
        {
            List<IAccion> returnActuadores = new List<IAccion>
            {
                new Encender(new AireAcondicionado(new SamsungAireAcondicionadoDriver(), "AA Samsung", 100))
            };
            return returnActuadores;
        }
    }
}
