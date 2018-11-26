using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGE.Entidades.Contexto;
using SGE.Entidades.Dispositivos;

namespace SGE.Tests.Dispositivos {
    [TestClass]
    public class CatalogoTest {
        SGEContext context = new SGEContext();

        [TestMethod]
        public void CreacionCatalogo() {
            //Se crea el catálogo con sus datos
            Catalogo catalogo = new Catalogo() {
                ConsumoEnergia = 100,
                IdentificadorFabrica = "12",
                Nombre = "Catalogo_UT"
            };

            context.Catalogos.Add(catalogo);
        }
    }
}
