using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SGE.Entidades.Simplex.Tests
{
    [TestClass()]
    public class SimplexBuilderTests
    {
        [TestMethod()]
        public void SimplexBuilderTest()
        {
            SimplexBuilder s = new SimplexBuilder();

            s.AgregarRestriccionMinimo("a1", 90)
                .AgregarRestriccionMinimo("a2", 90)
                .AgregarRestriccionMinimo("a3", 90)
                .AgregarRestriccionMinimo("a4", 6)
                .AgregarRestriccionMinimo("a5", 60)
                .AgregarRestriccionMinimo("a6", 3)
                .AgregarRestriccionMinimo("a7", 3)
                .AgregarRestriccionMinimo("a8", 120)
                .AgregarRestriccionMaximo("a1", 360)
                .AgregarRestriccionMaximo("a2", 360)
                .AgregarRestriccionMaximo("a3", 360)
                .AgregarRestriccionMaximo("a4", 30)
                .AgregarRestriccionMaximo("a5", 360)
                .AgregarRestriccionMaximo("a6", 15)
                .AgregarRestriccionMaximo("a7", 30)
                .AgregarRestriccionMaximo("a8", 360)
                .AgregarDispositivoEmisorCO2("a1")
                .AgregarDispositivoEmisorCO2("a2")
                .AgregarDispositivoEmisorCO2("a4")
                .AgregarConsumoOptimo(440)
                .Resolver();

            Console.WriteLine(s.Resultado.ToString());
        }
    }
}