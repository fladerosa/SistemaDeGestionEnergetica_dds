using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGE.Entidades;
using SGE.Entidades.Dispositivos;

namespace SGE.Tests.Entidades
{
    [TestClass]
    public class ClienteTest
    {
        Cliente cliente;
        Inteligente d1;
        EstandarAdaptado d2;
        Estandar d3;


        [TestInitialize]
        public void TestInitialize()
        {
            this.cliente = new Cliente();
            this.d1 = new Inteligente();
            this.d2 = new EstandarAdaptado();
            this.d3 = new Estandar();
        }


        [TestMethod]
        public void TieneDispositivosEncendidosTest()
        {
            cliente.Inteligentes.Add(d1);
            cliente.EstandarAdaptados.Add(d2);
            cliente.Estandars.Add(d3);
           
            d1.Encender();

            Assert.IsTrue(cliente.TieneDispositivosEncendidos());
        }

        [TestMethod]
        public void TieneDispositivosApagadosTest()
        {
            cliente.Inteligentes.Add(d1);
            cliente.Estandars.Add(d3);
            cliente.EstandarAdaptados.Add(d2);

            d1.Apagar();
            d2.Apagar();
            
            Assert.IsFalse(cliente.TieneDispositivosEncendidos());
        }

        [TestMethod]
        public void TieneDosDispositivosEncendidosTest()
        {
            cliente.Inteligentes.Add(d1);
            cliente.EstandarAdaptados.Add(d2);
            cliente.Estandars.Add(d3);

            d1.Encender();
            d2.Encender();

            Assert.AreEqual(cliente.CantidadDispositivosEncendidos(), 2);
        }

        [TestMethod]
        public void TieneDosDispositivosApagadosTest()
        {
            cliente.Inteligentes.Add(d1);
            cliente.EstandarAdaptados.Add(d2);
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

            cliente.Inteligentes.ForEach(d => d.Encender());

            Assert.AreEqual(cliente.CantidadDispositivosEncendidos(), 1);
        }
    }
}
