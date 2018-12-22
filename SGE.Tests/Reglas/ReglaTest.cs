using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGE.Entidades.Acciones;
using SGE.Entidades.Acciones.Acciones;
using SGE.Entidades.Dispositivos;
using SGE.Entidades.Reglas;
using SGE.Entidades.Reglas.Operadores;
using SGE.Entidades.Sensores;
using SGE.Entidades.Sensores.Sensores;

namespace SGE.Tests.Reglas {
    [TestClass]
    public class ReglaTest {

        private Condicion condicion1 { get; set; }
        private Accion accion1 { get; set; }
        private Inteligente dispositivo { get; set; }

        [TestInitialize]
        public void TestInitialize() {
            this.dispositivo = new Inteligente("TV LG", 100m);
            dispositivo.ColocarEnAhorroEnergia();
            this.condicion1 = new Condicion() {
                Operador = new Igual(),
                ValorReferencia = "Apagado",
                Sensor = new SensorFisico() {
                    Dispositivo = dispositivo,
                    TipoSensor = new Estado() {
                        Dispositivo = dispositivo
                    }
                }
            };
            this.accion1 = new Encender() {
                Dispositivo = dispositivo
            };
        }


        [TestMethod()]
        public void EjecutarTest() {
            Assert.IsTrue(dispositivo.EstaEnModoAhorroEnergia);
            Regla regla = new Regla() {
                Inteligente = dispositivo
            };
            regla.Condiciones.Add(condicion1);
            regla.Acciones.Add(accion1);
            regla.Ejecutar();

            Assert.IsTrue(dispositivo.EstaEnModoAhorroEnergia);
            dispositivo.Encender();
            regla.Ejecutar();

            Assert.IsTrue(dispositivo.EstaPrendido);

            dispositivo.Apagar();
            regla.Ejecutar();

            Assert.IsTrue(dispositivo.EstaPrendido);
        }
    }
}
