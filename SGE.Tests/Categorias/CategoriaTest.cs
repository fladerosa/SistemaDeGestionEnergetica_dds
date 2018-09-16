using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGE.Entidades.Categorias;
using SGE.Entidades.Repositorio;
using System.Linq;

namespace SGE.Tests.Categorias
{
    [TestClass()]
    public class CategoriaTest
    {
        
        [TestMethod()]
        public void Inicializar()
        {
            BaseRepositorio<Categoria> repoCategoria = new BaseRepositorio<Categoria>();
            Categoria categoriaNueva = new Categoria()
            {
                Codigo = "R2",
                ConsumoMinimo = 500,
                ConsumoMaximo = 1200,
                CostoFijo = 500 ,
                CostoVariable = 350
            };

            repoCategoria.Create(categoriaNueva);

            var ListaCategorias = repoCategoria.GetAll();

            Assert.IsTrue(ListaCategorias.Any(c => c.Codigo == categoriaNueva.Codigo));
            repoCategoria.Delete(categoriaNueva);
        }
    }
}
