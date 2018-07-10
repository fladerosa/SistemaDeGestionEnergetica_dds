using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGE.Entidades.Acciones;
using SGE.Entidades.Acciones.TV;
using SGE.Entidades.Dispositivos;
using SGE.Entidades.Drivers;

namespace SGE.Entidades.Actuadores.Tests
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
            Assert.IsTrue(this.dispositivo.EstaPrendido);
        }

        [TestMethod()]
        public void EjecutarApagarTest()
        {
            this.accion = new Apagar(this.dispositivo);

            this.dispositivo.Encender();
            this.accion.Ejecutar();
            Assert.IsTrue(this.dispositivo.EstaApagado);
        }

        [TestMethod()]
        public void EjecutarCambiarModoTest()
        {
            this.accion = new ColocarEnAhorroEnergia(this.dispositivo);
            
            this.accion.Ejecutar();
            Assert.IsTrue(this.dispositivo.EstaEnModoAhorroEnergia);
        }
    }
}