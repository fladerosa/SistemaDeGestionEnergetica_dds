using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGE.Entidades;

namespace SGE.Tests.Entidades
{
    [TestClass]
    public class ClienteTest
    {
        [TestMethod]
        public void TieneDispositivosEncendidosTest()
        {
            Cliente cliente = new Cliente();
            Dispositivo d1 = new Dispositivo();
            Dispositivo d2 = new Dispositivo();
            Dispositivo d3 = new Dispositivo();
            
            cliente.Dispositivos.Add(d1);
            cliente.Dispositivos.Add(d2);
            cliente.Dispositivos.Add(d3);

            d1.Accionar();

            Assert.IsTrue(cliente.TieneDispositivosEncendidos());
        }

        [TestMethod]
        public void TieneDispositivosApagadosTest()
        {
            Cliente cliente = new Cliente();
            Dispositivo d1 = new Dispositivo();
            Dispositivo d2 = new Dispositivo();
            Dispositivo d3 = new Dispositivo();

            cliente.Dispositivos.Add(d1);
            cliente.Dispositivos.Add(d2);
            cliente.Dispositivos.Add(d3);

            Assert.IsFalse(cliente.TieneDispositivosEncendidos());
        }

        [TestMethod]
        public void TieneDosDispositivosEncendidosTest()
        {
            Cliente cliente = new Cliente();
            Dispositivo d1 = new Dispositivo();
            Dispositivo d2 = new Dispositivo();
            Dispositivo d3 = new Dispositivo();

            cliente.Dispositivos.Add(d1);
            cliente.Dispositivos.Add(d2);
            cliente.Dispositivos.Add(d3);

            d1.Accionar();
            d2.Accionar();

            Assert.AreEqual(cliente.CantidadDispositivosEncendidos(), 2);
        }

        [TestMethod]
        public void TieneDosDispositivosApagadosTest()
        {
            Cliente cliente = new Cliente();
            Dispositivo d1 = new Dispositivo();
            Dispositivo d2 = new Dispositivo();
            Dispositivo d3 = new Dispositivo();

            cliente.Dispositivos.Add(d1);
            cliente.Dispositivos.Add(d2);
            cliente.Dispositivos.Add(d3);

            d1.Accionar();

            Assert.AreEqual(cliente.CantidadDispositivosApagados(), 2);
        }

        [TestMethod]
        public void TieneDosDispositivosATest()
        {
            Cliente cliente = new Cliente();
            Dispositivo d1 = new Dispositivo();
            Dispositivo d2 = new Dispositivo();

            cliente.Dispositivos.Add(d1);
            cliente.Dispositivos.Add(d2);

            Assert.AreEqual(cliente.CantidadTotalDispositivos(), 2);
        }

        [TestMethod]
        public void EncenderDosDispositivos()
        {
            Cliente cliente = new Cliente();
            Dispositivo d1 = new Dispositivo();
            Dispositivo d2 = new Dispositivo();

            cliente.Dispositivos.Add(d1);
            cliente.Dispositivos.Add(d2);

            cliente.Dispositivos.ForEach(d => d.Accionar());

            Assert.AreEqual(cliente.CantidadDispositivosEncendidos(), 2);
        }
    }
}
