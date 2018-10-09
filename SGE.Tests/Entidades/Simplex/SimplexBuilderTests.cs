using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGE.Entidades.Dispositivos;
using SGE.Entidades.Drivers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SGE.Entidades.Simplex.Tests
{
    [TestClass()]
    public class SimplexBuilderTests
    {
        [TestMethod()]
        public void HogarSinConsumo()
        {
            SimplexBuilder simplex = new SimplexBuilder();
            simplex.AgregarRestriccion(new KeyValuePair<string, string>("DDEAEA7C1ADE458991D496812D5D41FA", "elem_1"), 0);
            simplex.AgregarRestriccion(new KeyValuePair<string, string>("A0BA3245EAFC4EC994CC841698B835C0", "elem_2"), 0);
            simplex.Resolver();

            Assert.IsTrue(simplex.Resultado["ConsumoRestanteTotal"] == 440640);
        }

        [TestMethod()]
        public void HogarEficiente()
        {
            SimplexBuilder simplex = new SimplexBuilder();
            simplex.AgregarRestriccion(new KeyValuePair<string, string>("DDEAEA7C1ADE458991D496812D5D41FA", "elem_1"), 204);
            simplex.AgregarRestriccion(new KeyValuePair<string, string>("A0BA3245EAFC4EC994CC841698B835C0", "elem_2"), 40);
            simplex.Resolver();

            Assert.IsTrue(simplex.Resultado["ConsumoRestanteTotal"] > 0 && simplex.Resultado["ConsumoRestanteTotal"] < 440640);
        }

        [TestMethod()]
        public void HogarNoEficiente()
        {
            SimplexBuilder simplex = new SimplexBuilder();
            simplex.AgregarRestriccion(new KeyValuePair<string, string>("DDEAEA7C1ADE458991D496812D5D41FA", "elem_1"), 720);
            simplex.AgregarRestriccion(new KeyValuePair<string, string>("A0BA3245EAFC4EC994CC841698B835C0", "elem_2"), 720);
            simplex.Resolver();

            Assert.IsTrue(simplex.Resultado["ConsumoRestanteTotal"] == 0);
        }

        [TestMethod()]
        public void NoPasarAModoAhorroEnergia()
        {
            Inteligente Tele = new Inteligente("TV", new SonyTVDriver(), "DDEAEA7C1ADE458991D496812D5D41FA");
            Inteligente LavaRopa = new Inteligente("LAVARROPAS", new DreanLavarropasDriver(), "A0BA3245EAFC4EC994CC841698B835C0");

            Tele.Encender();
            Tele.RegistroDeActivaciones.ElementAt(0).FechaDeRegistro = Tele.RegistroDeActivaciones.ElementAt(0).FechaDeRegistro.AddHours(-300);

            LavaRopa.Encender();
            LavaRopa.RegistroDeActivaciones.ElementAt(0).FechaDeRegistro = LavaRopa.RegistroDeActivaciones.ElementAt(0).FechaDeRegistro.AddHours(-40);

            List<Inteligente> lista = new List<Inteligente>();
            lista.Add(Tele);
            lista.Add(LavaRopa);

            Assert.IsFalse(Tele.EstaApagado);

            Planificador pl = Planificador.getInstance();
            pl.Iniciar();
            pl.agregarTareaTest(lista, 1);

            Thread.Sleep(3000);
            pl.Detener();

            Assert.IsFalse(Tele.EstaApagado);
        }

        [TestMethod()]
        public void PasarAModoAhorroEnergia()
        {
            Inteligente Tele = new Inteligente("TV", new SonyTVDriver(), "DDEAEA7C1ADE458991D496812D5D41FA");
            Inteligente LavaRopa = new Inteligente("LAVARROPAS", new DreanLavarropasDriver(), "A0BA3245EAFC4EC994CC841698B835C0");

            Tele.Encender();
            Tele.RegistroDeActivaciones.ElementAt(0).FechaDeRegistro = Tele.RegistroDeActivaciones.ElementAt(0).FechaDeRegistro.AddHours(-300);

            LavaRopa.Encender();
            LavaRopa.RegistroDeActivaciones.ElementAt(0).FechaDeRegistro = LavaRopa.RegistroDeActivaciones.ElementAt(0).FechaDeRegistro.AddHours(-60);

            List<Inteligente> lista = new List<Inteligente>();
            lista.Add(Tele);
            lista.Add(LavaRopa);

            Assert.IsFalse(Tele.EstaApagado);

            Planificador pl = Planificador.getInstance();
            pl.Iniciar();
            pl.agregarTareaTest(lista, 1);
            
            Thread.Sleep(3000);
            pl.Detener();

            Assert.IsTrue(Tele.EstaApagado);
        }
    }
}