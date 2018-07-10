using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGE.Entidades.Usuarios;
using SGE.Entidades.Dispositivos;
using SGE.Entidades.Drivers;
using SGE.Core.Helpers;
using SGE.Entidades;

namespace SGE.Tests.Entidades
{
    [TestClass]
    public class TransformadorTest
    {
        Transformador transformador;
        List<Cliente> Clientes;
        decimal Latitud;
        decimal Longitud;

        [TestInitialize]
        public void TestInitialize()
        {
            Cliente cliente1 = new Cliente();
            Inteligente d1 = new Inteligente("TV LG", 100m, new SonyTVDriver());
            Estandar d2 = new Estandar("TV", 200m);
            cliente1.Inteligentes.Add(d1);
            cliente1.Estandars.Add(d2);

            Cliente cliente2 = new Cliente();
            Inteligente d3 = new Inteligente("TV LG 55", 200m, new SonyTVDriver());
            Estandar d4 = new Estandar("TV", 200m);
            Estandar d5 = new Estandar("TV", 200m);
            cliente2.Inteligentes.Add(d3);
            cliente2.Estandars.Add(d4);
            cliente2.Estandars.Add(d5);

            this.Clientes = new List<Cliente>
            {
                cliente1,
                cliente2
            };

            this.transformador = new Transformador();
            this.transformador.Clientes = this.Clientes;

        }


        [TestMethod]
        public void ObtenerConsumoTest()
        {
            decimal consumo = this.transformador.ObtenerConsumo();
            Assert.Equals(consumo, 900);
        }
    }
}
