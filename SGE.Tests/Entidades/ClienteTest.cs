using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGE.Entidades;

namespace SGE.Tests.Entidades
{ //21/05 se modificaron los dispositivos, teniendo en cuenta los nuevos tipos y el EstandarAdaptado
    [TestClass]
    public class ClienteTest
    {
        [TestMethod]
        public void TieneDispositivosEncendidosTest()
        {
            Cliente cliente = new Cliente();
            Inteligente d1 = new Inteligente();
            EstandarAdaptado d2 = new EstandarAdaptado();
            Estandar d3 = new Estandar();
         
            cliente.Inteligentes.Add(d1);
            cliente.EstandarAdaptados.Add(d2);
            cliente.Estandars.Add(d3);
           
            d1.EncenderA();

            Assert.IsTrue(cliente.TieneDispositivosEncendidos());
        }

        [TestMethod]
        public void TieneDispositivosApagadosTest()
        {
            Cliente cliente = new Cliente();
            Inteligente d1 = new Inteligente();
            Estandar d2 = new Estandar();
            EstandarAdaptado d3 = new EstandarAdaptado();

            cliente.Inteligentes.Add(d1);
            cliente.Estandars.Add(d2);
            cliente.EstandarAdaptados.Add(d3);

            Assert.IsFalse(cliente.TieneDispositivosEncendidos());
        }

        [TestMethod]
        public void TieneDosDispositivosEncendidosTest()
        {
            Cliente cliente = new Cliente();
            Inteligente d1 = new Inteligente();
            EstandarAdaptado d2 = new EstandarAdaptado();
            Estandar d3 = new Estandar();

            cliente.Inteligentes.Add(d1);
            cliente.EstandarAdaptados.Add(d2);
            cliente.Estandars.Add(d3);

            d1.EncenderA();
            d2.EncenderA();

            Assert.AreEqual(cliente.CantidadDispositivosEncendidos(), 2);
        }

        [TestMethod]
        public void TieneDosDispositivosApagadosTest()
        {
            Cliente cliente = new Cliente();
            Inteligente d1 = new Inteligente();
            EstandarAdaptado d2 = new EstandarAdaptado();
            Estandar    d3 = new Estandar();

            cliente.Inteligentes.Add(d1);
            cliente.EstandarAdaptados.Add(d2);
            cliente.Estandars.Add(d3);

            d1.ApagarA();

            Assert.AreEqual(cliente.CantidadDispositivosApagados(), 2);
        }

        [TestMethod]
        public void TieneDosDispositivosATest()
        {
            Cliente cliente = new Cliente();
            Inteligente d1 = new Inteligente();
            Estandar d2 = new Estandar();

            cliente.Inteligentes.Add(d1);
            cliente.Estandars.Add(d2);

            Assert.AreEqual(cliente.CantidadTotalDispositivos(), 2);
        }

        [TestMethod]
        public void EncenderSoloDIDispositivos()
        {
            Cliente cliente = new Cliente();
            Inteligente d1 = new Inteligente();
            Estandar d2 = new Estandar();

            cliente.Inteligentes.Add(d1);
            cliente.Estandars.Add(d2);

            cliente.Inteligentes.ForEach(d => d.EncenderA());

            Assert.AreEqual(cliente.CantidadDispositivosEncendidos(), 2);
        }
    }
}
