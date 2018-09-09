using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGE.Core.Helpers;
using SGE.Entidades;
using SGE.Entidades.Usuarios;
using SGE.Entidades.Dispositivos;
using SGE.Entidades.Drivers;
//using SGE.Core.Entidades;

namespace SGE.Tests.Entidades
{
    [TestClass]
    public class ZonaTest
    {
        SGE.Entidades.Zona zona;
        List<SGE.Entidades.Transformador> transformadores;
        private List<Cliente> clientes1;
        private List<Cliente> clientes2;


        [TestInitialize]
        public void TestInitialize()
        {
            Cliente cliente1 = new Cliente();
            Inteligente d1 = new Inteligente("TV LG", 100m, new SonyTVDriver());
            Estandar d2 = new Estandar("TV", 200m);
            cliente1.Inteligentes.Add(d1);
            cliente1.Estandars.Add(d2);
            cliente1.Latitud = 5.41;
            cliente1.Longitud = 5.45;
            cliente1.TransformadorId = 1;

            Cliente cliente2 = new Cliente();
            Inteligente d3 = new Inteligente("TV LG 55", 200m, new SonyTVDriver());
            Estandar d4 = new Estandar("TV", 200m);
            Estandar d5 = new Estandar("TV", 200m);
            cliente2.Inteligentes.Add(d3);
            cliente2.Estandars.Add(d4);
            cliente2.Estandars.Add(d5);
            cliente2.Latitud = 5.39;
            cliente2.Longitud = 5.43;
            cliente2.TransformadorId = 1;

            this.clientes1 = new List<Cliente> { cliente1, cliente2 };


            //si se carga los transformadores con json comentar estoo
            SGE.Entidades.Transformador trasformador1 = new Transformador();
            trasformador1.Id = 1;
            trasformador1.Latitud = 5.4;
            trasformador1.Longitud = 5.44;
            trasformador1.Clientes = clientes1;


            Cliente cliente3 = new Cliente();
            Inteligente d6 = new Inteligente("TV LG", 100m, new SonyTVDriver());
            Estandar d7 = new Estandar("TV", 200m);
            cliente3.Inteligentes.Add(d6);
            cliente3.Estandars.Add(d7);
            cliente3.Latitud = -0.3;
            cliente3.Longitud = -0.1;
            cliente3.TransformadorId = 10;

            Cliente cliente4 = new Cliente();
            Inteligente d8 = new Inteligente("TV LG 55", 200m, new SonyTVDriver());
            Estandar d9 = new Estandar("TV", 200m);
            Estandar d10 = new Estandar("TV", 200m);
            cliente4.Inteligentes.Add(d8);
            cliente4.Estandars.Add(d9);
            cliente4.Estandars.Add(d10);
            cliente4.Latitud = -0.1;
            cliente4.Longitud = 0.1;
            cliente4.TransformadorId = 10;

            this.clientes2 = new List<Cliente> { cliente3, cliente4 };

            //si se carga los transformadores con json comentar estoo
            Transformador trasformador2 = new Transformador();
            trasformador2.Id = 10;
            trasformador2.Latitud = -0.2;
            trasformador2.Longitud = 0;
            trasformador2.Clientes = clientes2;


            this.transformadores = new List<Transformador> { trasformador1, trasformador2 };
            this.zona = new Zona();
            this.zona.Transformadores = transformadores;


            //TransformadoresHelper th = new TransformadoresHelper();
            //List<Transformador> transformadores = (List<Transformador>)th.Transformadores;
            //foreach (Transformador transformador in transformadores)
            //{
            //    if (transformador.Id==1)
            //    {
            //        transformador.Clientes = clientes1;
            //    }
            //    if (transformador.Id == 10)
            //    {
            //        transformador.Clientes = clientes2;
            //    }
            //}
        }


        [TestMethod]
        public void ObtenerConsumoTest()
        {
            decimal consumo = this.zona.ObtenerConsumo();
            Assert.AreEqual(consumo, 1800);
        }

        [TestMethod]
        public void AsignarTransformadorCorrespondienteTest()
        {
      
            AsignarTransformadorCercano(this.clientes1);
            AsignarTransformadorCercano(this.clientes2);
            //verifico que se mantienen los transformadores asignados a los clientes
            foreach (Cliente cliente in this.clientes1)
            {
                Assert.AreNotEqual(cliente.TransformadorId,10);
                Assert.AreEqual(cliente.TransformadorId, 1);
            }

            foreach (Cliente cliente in this.clientes2)
            {
                Assert.AreNotEqual(cliente.TransformadorId, 1);
                Assert.AreEqual(cliente.TransformadorId, 10);
            }
        }

        private void AsignarTransformadorCercano(List<Cliente> clientes)
        {
            DistanciaHelper dh = new DistanciaHelper();

            foreach (Cliente cliente in clientes)
            {
                double menorDistancia = 300;
                foreach (Transformador transformador in this.transformadores)
                {
                    double distanciaActual = dh.CalcularDistancia(transformador.Latitud, transformador.Longitud, cliente.Latitud,
                    cliente.Longitud, "K");

                    if (distanciaActual < menorDistancia)
                    {
                        menorDistancia = distanciaActual;
                        cliente.TransformadorId = transformador.Id;
                    }
                }
            }
        }


    }
}
