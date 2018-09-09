using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGE.Entidades.Usuarios;
using SGE.Entidades.Dispositivos;
using SGE.Entidades.Drivers;
using SGE.Core.Helpers;
using SGE.Entidades;
using SGE.Core.Entidades;

namespace SGE.Tests.Entidades
{
    [TestClass]
    public class TransformadorTest
    {
        SGE.Entidades.Transformador transformador;
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
            cliente1.TransformadorId = 1;

            Cliente cliente2 = new Cliente();
            Inteligente d3 = new Inteligente("TV LG 55", 200m, new SonyTVDriver());
            Estandar d4 = new Estandar("TV", 200m);
            Estandar d5 = new Estandar("TV", 200m);
            cliente2.Inteligentes.Add(d3);
            cliente2.Estandars.Add(d4);
            cliente2.Estandars.Add(d5);
            cliente2.TransformadorId = 10;

            this.Clientes = new List<Cliente>
            {
                cliente1,
                cliente2
            };

            this.transformador = new SGE.Entidades.Transformador();
            this.transformador.Clientes = this.Clientes;

            //TransformadoresHelper th =new TransformadoresHelper();
            //List<Transformador> transformadores =(List<Transformador>) th.Transformadores;
        }


        [TestMethod]
        public void ObtenerConsumoTest()
        {
            decimal consumo = this.transformador.ObtenerConsumo();
            Assert.AreEqual(consumo, 900);
        }
    }
}
