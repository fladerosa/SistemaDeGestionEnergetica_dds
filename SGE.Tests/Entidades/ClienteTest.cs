using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGE.Entidades.Dispositivos;
using SGE.Entidades.Usuarios;

namespace SGE.Tests.Entidades {
    [TestClass]
    public class ClienteTest
    {
        Cliente cliente;
        Inteligente d1;
        Estandar d3;


        [TestInitialize]
        public void TestInitialize()
        {
            this.cliente = new Cliente();
            this.d1 = new Inteligente("TV LG", 100m);
            this.d3 = new Estandar("TV", 200m);
        }


        [TestMethod]
        public void TieneDispositivosEncendidosTest()
        {
            cliente.Inteligentes.Add(d1);
            cliente.Estandars.Add(d3);
           
            d1.Encender();

            Assert.IsTrue(cliente.TieneDispositivosEncendidos());
        }

        [TestMethod]
        public void TieneDispositivosApagadosTest()
        {
            cliente.Inteligentes.Add(d1);
            cliente.Estandars.Add(d3);

            d1.Apagar();
            
            Assert.IsFalse(cliente.TieneDispositivosEncendidos());
        }

        [TestMethod]
        public void TieneDosDispositivosEncendidosTest()
        {
            cliente.Inteligentes.Add(d1);
            cliente.Estandars.Add(d3);

            d1.Encender();

            Assert.AreEqual(cliente.CantidadDispositivosEncendidos(), 1);
        }

        [TestMethod]
        public void TieneDosDispositivosApagadosTest()
        {
            cliente.Inteligentes.Add(d1);
            cliente.Estandars.Add(d3);

            d1.Apagar();

            Assert.AreEqual(cliente.CantidadDispositivosApagados(), 1);
        }

        [TestMethod]
        public void TieneDosDispositivosATest()
        {
            cliente.Inteligentes.Add(d1);
            cliente.Estandars.Add(d3);

            Assert.AreEqual(cliente.CantidadTotalDispositivos(), 1);
        }

        [TestMethod]
        public void EncenderSoloDIDispositivos()
        {
            cliente.Inteligentes.Add(d1);
            cliente.Estandars.Add(d3);

            
            foreach (Inteligente inteligente in cliente.Inteligentes) {
                inteligente.Encender();
            }

            Assert.AreEqual(cliente.CantidadDispositivosEncendidos(), 1);
        }
    }
}
