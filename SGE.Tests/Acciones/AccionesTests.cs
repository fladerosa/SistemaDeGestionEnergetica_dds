using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGE.Entidades.Acciones.Acciones;
using SGE.Entidades.Dispositivos;

namespace SGE.Entidades.Acciones.Tests {
    [TestClass()]
    public class ActuadoresTests {
        private Inteligente dispositivo;
        private Accion accion;

        [TestInitialize]
        public void TestInitialize() {
            this.dispositivo = new Inteligente("TV LG", 100m);
        }

        [TestMethod()]
        public void EjecutarEncenderTest() {
            this.accion = new Encender() {
                Dispositivo = this.dispositivo
            };

            this.accion.Ejecutar();
            Assert.IsTrue(this.dispositivo.EstaPrendido);

            this.accion = new Apagar() {
                Dispositivo = this.dispositivo
            };
            this.accion.Ejecutar();
            Assert.IsFalse(this.dispositivo.EstaPrendido);
        }
    }
}