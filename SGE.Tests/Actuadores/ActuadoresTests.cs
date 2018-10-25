using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGE.WebconAutenticacion.Acciones;
using SGE.WebconAutenticacion.Acciones.TV;
using SGE.WebconAutenticacion.Dispositivos;
using SGE.WebconAutenticacion.Drivers;

namespace SGE.WebconAutenticacion.Actuadores.Tests
{
    [TestClass()]
    public class ActuadoresTests
    {
        private Inteligente dispositivo;
        private IAccion accion;

        [TestInitialize]
        public void TestInitialize()
        {
            this.dispositivo = new Inteligente("TV LG", 100m, new SonyTVDriver());
        }

        [TestMethod()]
        public void EjecutarEncenderTest()
        {
            this.accion = new Encender(this.dispositivo);

            this.dispositivo.Apagar();
            this.accion.Ejecutar();
            Assert.IsFalse(this.dispositivo.EstaPrendido);
        }

        [TestMethod()]
        public void EjecutarApagarTest()
        {
            this.accion = new Apagar(this.dispositivo);

            this.dispositivo.Encender();
            this.accion.Ejecutar();
            Assert.IsFalse(this.dispositivo.EstaApagado);
        }

        [TestMethod()]
        public void EjecutarCambiarModoTest()
        {
            this.accion = new ColocarEnAhorroEnergia(this.dispositivo);
            
            this.accion.Ejecutar();
            Assert.IsFalse(this.dispositivo.EstaEnModoAhorroEnergia);
        }
    }
}