using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGE.Entidades.Dispositivos;

namespace SGE.Entidades.Actuadores.Tests
{
    [TestClass()]
    public class ActuadoresTests
    {
        private Inteligente dispositivo;
        private Actuador actuador;

        [TestInitialize]
        public void TestInitialize()
        {
            this.dispositivo = new Inteligente("TV LG", 100m);
        }

        [TestMethod()]
        public void EjecutarEncenderTest()
        {
            this.actuador = new AccionEncender("Encende-dor", this.dispositivo);

            this.dispositivo.Apagar();
            this.actuador.Ejecutar();
            Assert.IsTrue(this.dispositivo.EstaEncendido);
        }

        [TestMethod()]
        public void EjecutarApagarTest()
        {
            this.actuador = new AccionApagar("Apaga-dor", this.dispositivo);

            this.dispositivo.Encender();
            this.actuador.Ejecutar();
            Assert.IsTrue(this.dispositivo.EstaApagado);
        }

        [TestMethod()]
        public void EjecutarCambiarModoTest()
        {
            this.actuador = new AccionCambiarModoOperacion("Cambia-dor", this.dispositivo);
            
            this.actuador.Ejecutar();
            Assert.IsTrue(this.dispositivo.EstaEnModoAhorro);
        }
    }
}