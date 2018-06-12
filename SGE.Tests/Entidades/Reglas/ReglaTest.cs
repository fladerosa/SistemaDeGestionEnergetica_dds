using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGE.Entidades;
using SGE.Entidades.Actuadores;
using SGE.Entidades.Dispositivos;
using SGE.Entidades.Reglas;

namespace SGE.Tests.Reglas
{
    [TestClass]
    public class ReglaTest
    {

        private List<Condicion> condiciones { get; set; }
        private List<Actuador> actuadores { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            this.condiciones = CrearCondiciones();
            this.actuadores = CrearActuadores();
        }


        [TestMethod()]
        public void EjecutarTest()
        {
            Regla regla= new Regla("ReglaCalorEncenderAire", this.condiciones, this.actuadores);
            regla.Ejecutar();
        }


        private List<Condicion> CrearCondiciones()
        {
            List<Condicion> returnCondiciones = new List<Condicion>
            {
                new Condicion(new Sensor(TipoUnidadEnum.Temperatura, UnidadEnum.CENTIGRADOS), 30, OperadorEnum.MAYOR)
            };
            return returnCondiciones;
        }
        private List<Actuador> CrearActuadores()
        {
            List<Actuador> returnActuadores = new List<Actuador>
            {
                new AccionEncender("Aire acondicionado",new Inteligente("Aire Acondicionado",50))
            };
            return returnActuadores;
        }
    }
}
