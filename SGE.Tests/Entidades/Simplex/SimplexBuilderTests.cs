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

            s.AgregarRestriccionMinimo("A06C86E69C7A4858A38B3CBB5F5F881C", 90)
             .AgregarRestriccionMinimo("A2DC1DAA5B104DC88E564D49D5FAFE39", 90)
             .AgregarRestriccionMinimo("E0BD49B5FC4D48F1B66B55F543238E1E", 90)
             .AgregarRestriccionMinimo("DDEAEA7C1ADE458991D496812D5D41FA", 6)
             .AgregarRestriccionMinimo("E6533BACF5A74210A5D9708C8A0B3EE8", 60)
             .AgregarRestriccionMinimo("AA63D8C46C8344A19B034995555C49CD", 3)
             .AgregarRestriccionMinimo("A0BA3245EAFC4EC994CC841698B835C0", 3)
             .AgregarRestriccionMinimo("E82D0A8FF83F4287878B0B88EDA5FACC", 120)
             .AgregarRestriccionMaximo("A06C86E69C7A4858A38B3CBB5F5F881C", 360)
             .AgregarRestriccionMaximo("A2DC1DAA5B104DC88E564D49D5FAFE39", 360)
             .AgregarRestriccionMaximo("E0BD49B5FC4D48F1B66B55F543238E1E", 360)
             .AgregarRestriccionMaximo("DDEAEA7C1ADE458991D496812D5D41FA", 30)
             .AgregarRestriccionMaximo("E6533BACF5A74210A5D9708C8A0B3EE8", 360)
             .AgregarRestriccionMaximo("AA63D8C46C8344A19B034995555C49CD", 15)
             .AgregarRestriccionMaximo("A0BA3245EAFC4EC994CC841698B835C0", 30)
             .AgregarRestriccionMaximo("E82D0A8FF83F4287878B0B88EDA5FACC", 360)
             .AgregarValorOptimo(440)
             .Resolver();

            Console.WriteLine(s.Resultado.ToString());
        }
    }
}