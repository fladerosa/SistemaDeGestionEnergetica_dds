using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGE.Core.Entidades;

namespace SGE.Core.Helpers.Tests
{
    [TestClass()]
    public class DispositivosHelperTests
    {
        DispositivosHelper helper;

        [TestInitialize()]
        public void TestInitialize()
        {
            this.helper = DispositivosHelper.GetInstace();
        }

        [TestMethod()]
        public void GetDispositivoTest()
        {
            Dispositivo DispositivoHelper = this.helper.GetDispositivo("E6533BACF5A74210A5D9708C8A0B3EE8");

            Assert.IsTrue(DispositivoHelper.Tipo.Equals("HELADERA"));
        }

        [TestMethod()]
        public void ExisteTest()
        {
            Assert.IsTrue(this.helper.Existe("E82D0A8FF83F4287878B0B88EDA5FACC"));
        }

        [TestMethod()]
        public void NoExisteTest()
        {
            Assert.IsFalse(this.helper.Existe("E82D0A8FF83F4287878B0B88EDA5FAC1"));
        }
    }
}