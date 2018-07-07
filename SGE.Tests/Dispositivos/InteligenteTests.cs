using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGE.Entidades.Drivers;

namespace SGE.Entidades.Dispositivos.Tests
{
    [TestClass()]
    public class InteligenteTests
    {
        [TestMethod()]
        public void ObtenerConsumoDeUltimasNHoras_2_activacionesTest()
        {
            Inteligente dispositivo = new Inteligente("TV", 100m, new SonyTV());
            decimal valor;

            dispositivo.Encender();
            dispositivo.RegistroDeActivaciones[0].FechaDeRegistro = dispositivo.RegistroDeActivaciones[0].FechaDeRegistro.AddHours(-1);
            dispositivo.Apagar();

            valor = dispositivo.ObtenerConsumoDeUltimasNHoras(8);
            Assert.IsTrue(valor == 100m);
        }

        [TestMethod()]
        public void ObtenerConsumoDeUltimasNHoras_3_activacionesTest()
        {
            Inteligente dispositivo = new Inteligente("TV", 100m, new SonyTV());
            decimal valor;

            dispositivo.Encender();
            dispositivo.RegistroDeActivaciones[0].FechaDeRegistro = dispositivo.RegistroDeActivaciones[0].FechaDeRegistro.AddHours(-3);
            dispositivo.Apagar();
            dispositivo.RegistroDeActivaciones[1].FechaDeRegistro = dispositivo.RegistroDeActivaciones[1].FechaDeRegistro.AddHours(-2);
            dispositivo.Encender();
            dispositivo.RegistroDeActivaciones[2].FechaDeRegistro = dispositivo.RegistroDeActivaciones[2].FechaDeRegistro.AddHours(-1);

            valor = dispositivo.ObtenerConsumoDeUltimasNHoras(8);
            Assert.IsTrue(valor == 200m);
        }

        [TestMethod()]
        public void ObtenerConsumoDeUltimasNHoras_EstandarAdaptadoTest()
        {
            Estandar d1 = new Estandar("tv", 100m);
            Inteligente dispositivo = new EstandarAdaptado(d1);
            decimal valor;

            dispositivo.Encender();
            dispositivo.RegistroDeActivaciones[0].FechaDeRegistro = dispositivo.RegistroDeActivaciones[0].FechaDeRegistro.AddHours(-1);
            dispositivo.Apagar();

            valor = dispositivo.ObtenerConsumoDeUltimasNHoras(8);
            Assert.IsTrue(valor == 100m);
        }

        [TestMethod()]
        public void ObtenerConsumoPeriodoTest()
        {
            Inteligente dispositivo = new Inteligente("TV", 100m, new SonyTV());
            decimal valor;

            dispositivo.Encender();
            dispositivo.RegistroDeActivaciones[0].FechaDeRegistro = dispositivo.RegistroDeActivaciones[0].FechaDeRegistro.AddHours(-25);
            dispositivo.Apagar();
            dispositivo.RegistroDeActivaciones[0].FechaDeRegistro = dispositivo.RegistroDeActivaciones[0].FechaDeRegistro.AddHours(-4);
            dispositivo.Encender();
            dispositivo.RegistroDeActivaciones[0].FechaDeRegistro = dispositivo.RegistroDeActivaciones[0].FechaDeRegistro.AddHours(-3);
            dispositivo.Apagar();
            dispositivo.RegistroDeActivaciones[0].FechaDeRegistro = dispositivo.RegistroDeActivaciones[0].FechaDeRegistro.AddHours(-1);

            valor = dispositivo.ObtenerConsumoDeUltimasNHoras(8);
            Assert.IsTrue(valor == 800m);
        }
    }
}