using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGE.Entidades;

namespace SGE.Tests.Entidades
{
    [TestClass]
    public class CategoriaTest
    {
        [TestMethod]
        public void CalcularFacturaMensual()
        {
            Categoria categoria = new Categoria();
            categoria.Codigo = "R2";
            categoria.CostoFijo = 32.5m;
            categoria.CostoVariable = 0.644m;

            Assert.AreEqual(Math.Round(categoria.CalcularFacturaMensual(100), 2), 96.92);
        }

        [TestMethod]
        public void ConsumoDentroDeLosLimites()
        {
            Categoria categoria = new Categoria();
            categoria.Codigo = "R2";
            categoria.ConsumoMinimo = 150;
            categoria.ConsumoMaximo = 325;

            Assert.IsTrue(categoria.ConsumoDentroDeLosLimites(189));
        }
    }
}
