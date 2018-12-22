using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGE.Entidades.Acciones;
using SGE.Entidades.Acciones.Acciones;
using SGE.Entidades.Dispositivos;
using SGE.Entidades.Sensores;
using SGE.Entidades.Sensores.Sensores;

namespace SGE.Tests.Sensores {
    [TestClass]
    public class SensoresTests {
        private Inteligente dispositivo;
        private Sensor sensor;
        private Accion accion;

        [TestInitialize]
        public void TestInitialize() {
            this.dispositivo = new Inteligente("TV LG", 100m);
            this.accion = new EstablecerTemperatura();
        }

        [TestMethod]
        public void TestLectura() {
            accion.Dispositivo = dispositivo;
            accion.Ejecutar("100");

            Assert.AreEqual("100", dispositivo.Temperatura.ToString());

            sensor = new Temperatura();
            sensor.Dispositivo = dispositivo;

            Medicion medicion = sensor.RealizarMedicion();

            Assert.AreEqual(dispositivo.Temperatura.ToString(), medicion.Valor);
        }
    }
}
