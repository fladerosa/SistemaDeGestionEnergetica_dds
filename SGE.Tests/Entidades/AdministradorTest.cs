using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGE.Entidades.Repositorio;
using SGE.Entidades.Usuarios;

namespace SGE.Tests.Entidades
{
    [TestClass]
    public class AdministradorTest
    {
        [TestMethod]
        public void TieneAntiguedadIgualACero()
        {
            Assert.IsFalse((new Administrador()).Antiguedad() > 0);
        }

        [TestMethod]
        public void cargaAdmin()
        {
            BaseRepositorio<Administrador> repoAdmin = new BaseRepositorio<Administrador>();
            Administrador adminNuevo = new Administrador()
            {
                Nombre = "NombreAdmin_test_01",
                Apellido = "ApellidoAdmin_test_01",
                NombreUsuario = "NombreUsuarioAdmin_test_1",
                Password = "PasswordAdmin_1",

                Nui = "abc-235"
            };

            repoAdmin.Create(adminNuevo);
            repoAdmin.Delete(adminNuevo);
        }
        
    }
}
