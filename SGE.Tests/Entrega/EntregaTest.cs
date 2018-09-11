using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGE.Entidades.Acciones;
using SGE.Entidades.Categorias;
using SGE.Entidades.Dispositivos;
using SGE.Entidades.Drivers;
using SGE.Entidades.Reglas;
using SGE.Entidades.Reglas.Operadores;
using SGE.Entidades.Reportes;
using SGE.Entidades.Repositorio;
using SGE.Entidades.Transformadores;
using SGE.Entidades.Usuarios;
using SGE.Entidades.ValueProviders;
using SGE.Entidades.Zonas;
using System;
using SGE.Core.Helpers;
using System.Collections.Generic;

namespace SGE.Tests.Entrega {
    [TestClass]
    public class EntregaTest
    {
        BaseRepositorio<Cliente> repoCliente = new BaseRepositorio<Cliente>();
        BaseRepositorio<Zona> repoZona = new BaseRepositorio<Zona>();

        //TODO: se crean los casos de pruebas mínimos para la entrega 3. Para facilitar la lectura se los agrupa en esta clase,
        //sin embargo luego se acomodarán las pruebas en las clases correspondientes.

        [TestMethod]
        
        public void CasoDePrueba1() {
            ///Crear 1 usuario nuevo.
            ///Persistirlo.
            ///Recuperarlo, modificar la geolocalización
            ///grabarlo.
            ///Recuperarlo y evaluar que el cambio se haya realizado.

            //se carga una zona para evitar que rompa por fk de transformador
            Zona zona = new Zona()
            {
                Nombre = "zona_02",
                Latitud = 35,
                Longitud = 45,
                Radio = 5
            };
            repoZona.Create(zona);

            Cliente cliente = new Cliente() {
                Nombre = "Nombre_test_cp1",
                Apellido = "Apellido_test_cp1",
                NombreUsuario = "NombreUsuario_test_cp1",
                Password = "Password_test_01",

                NumeroDocumento = "12345678",
                Latitud = 3,
                Longitud = 4,
                Telefonos = new List<Telefono>()
            };
            cliente.Direccion = new Direccion() {
                Calle = "calle_cp1",
                Nro = "2468"
            };
            cliente.Telefonos.Add(new Telefono() {
                Numero = "12345"
            });

            cliente.Transformador = new Transformador()
            {
                Latitud = 45,
                Longitud = 55,
                ZonaId = zona.Id
            };

            cliente.Categoria = new Categoria()
            {
                Codigo = "R3",
                ConsumoMinimo = 1500,
                ConsumoMaximo = 2200,
                CostoFijo = 300,
                CostoVariable = 550
            };

            repoCliente.Create(cliente);

            Cliente clienteConsultado = repoCliente.Single(c => c.Id == cliente.Id);

            clienteConsultado.Latitud = 3;
            clienteConsultado.Longitud = 4;

            repoCliente.Update(clienteConsultado);

            Cliente clienteConsultado2 = repoCliente.Single(c => c.Id == cliente.Id);

            Assert.AreEqual(clienteConsultado2.Latitud, 3);
            Assert.AreEqual(clienteConsultado2.Longitud, 4);
        }

        [TestMethod]
        public void CasoDePrueba2() {
            ///Recuperar un dispositivo.
            ///Mostrar por consola todos los intervalos que estuvo encendido durante el último mes.
            ///Modificar su nombre(o cualquier otro atributo editable) y grabarlo.
            ///Recuperarlo y evaluar que el nombre coincida con el esperado.
            Inteligente dispositivo = new Inteligente("TV_cp2", 100m, new SonyTVDriver());
            BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>();

            repoInteligente.Create(dispositivo);

            dispositivo.Encender();
            dispositivo.RegistroDeActivaciones[0].FechaDeRegistro = dispositivo.RegistroDeActivaciones[0].FechaDeRegistro.AddHours(-25);
            dispositivo.Apagar();
            dispositivo.RegistroDeActivaciones[1].FechaDeRegistro = dispositivo.RegistroDeActivaciones[1].FechaDeRegistro.AddHours(-4);
            dispositivo.Encender();
            dispositivo.RegistroDeActivaciones[2].FechaDeRegistro = dispositivo.RegistroDeActivaciones[2].FechaDeRegistro.AddHours(-3);
            dispositivo.Apagar();
            dispositivo.RegistroDeActivaciones[3].FechaDeRegistro = dispositivo.RegistroDeActivaciones[3].FechaDeRegistro.AddHours(-1);

            List<string> intervalosEncendido = dispositivo.ObtenerIntervalosEncendidoPorPeriodo(DateTime.Now.AddMonths(-1), DateTime.Now);

            foreach (string intervaloEncendido in intervalosEncendido) {
                Console.WriteLine(intervaloEncendido);
            }

            dispositivo.Nombre = "nombre modificado";

            repoInteligente.Update(dispositivo);

            Inteligente dispositivoModificado = repoInteligente.Single(i => i.Id == dispositivo.Id);

            Assert.AreEqual(dispositivoModificado.Nombre, "nombre modificado");

        }

        [TestMethod]
        public void CasoDePrueba3() {
            ///Crear una nueva regla.
            ///Asociarla a un dispositivo.
            ///Agregar condiciones y acciones.
            ///Persistirla.
            ///Recuperarla y ejecutarla.
            ///Modificar alguna condición y persistirla.
            ///Recuperarla y evaluar que la condición modificada posea la última modificación.
            Inteligente dispositivo = new Inteligente("TV_cp3", 100m, new SonyTVDriver());
            BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>();

            repoInteligente.Create(dispositivo);


            Regla regla = new Regla();
            regla.Nombre = "ReglaCalorEncenderAire";
            regla.Condiciones.Add(new Condicion(new SensorTemperaturaAA(new SamsungAireAcondicionadoDriver()), new Mayor(), 30));
            regla.Accions.Add(new Encender(new AireAcondicionado(new SamsungAireAcondicionadoDriver(), "AA Samsung", 100)));
            
            //TODO: no puedo mapear las reglas a los dispositivos....

        }

        [TestMethod]
        public void CasoDePrueba4()
        {
            //persisto las zonas para poder luego persistir los transformadores
            ZonasHelper zonaHelper = new ZonasHelper();
            BaseRepositorio<Zona> repoZona = new BaseRepositorio<Zona>();
            foreach (Core.Entidades.Zona z in zonaHelper.Zonas)
            {
                repoZona.Create(AsignarAtributosZona(z));
            }


            //Persisto los transformadores que el ENRE envia
            TransformadoresHelper transHelper = new TransformadoresHelper();
            BaseRepositorio<Transformador> repoTransformador = new BaseRepositorio<Transformador>();
            foreach (Core.Entidades.Transformador t in transHelper.Transformadores)
            {
                repoTransformador.Create(AsignarAtributosTransformador(t));
            }

            //Recupero los transformadores persistidos
            List<Transformador> transPer = repoTransformador.GetAll();

            //cargamos los transformadores desde el json con una instancia de transformador mas
            string nombreArchivoJsonMasUno = "transformadoresConUnoMas.json";
            TransformadoresHelper transHelperMasUno = new TransformadoresHelper(nombreArchivoJsonMasUno);
            //persistimos los transformadores
            foreach (Core.Entidades.Transformador t in transHelperMasUno.Transformadores)
            {
                var retorno = repoTransformador.Single(s => s.Id == t.Id);
                if (retorno != null)
                {
                    repoTransformador.Update(AsignarAtributosTransformador(t));
                }
                else
                {
                    repoTransformador.Create(AsignarAtributosTransformador(t));
                }

            }
            List<Transformador> transPerMasUno = repoTransformador.GetAll();

            // Evaluo que la cantidad actual sea la anterior + 1
            Assert.AreEqual(transPer.Count + 1, transPerMasUno.Count);

            //eliminamos los registros con los que hicimos las pruebas
            foreach (Core.Entidades.Zona z in zonaHelper.Zonas)
            {
                repoZona.Delete(AsignarAtributosZona(z));
            }
        }
        private Transformador AsignarAtributosTransformador(Core.Entidades.Transformador xT)
        {
            Transformador transformadorReturn = new Transformador();
            transformadorReturn.Id = xT.Id;
            transformadorReturn.Latitud = (double)xT.Latitud;
            transformadorReturn.Longitud = (double)xT.Longitud;
            transformadorReturn.ZonaId = xT.Zona;
            return transformadorReturn;

        }
        private Zona AsignarAtributosZona(Core.Entidades.Zona xZ)
        {
            Zona zonaReturn = new Zona();
            zonaReturn.Id = xZ.Id;
            zonaReturn.Nombre = xZ.Nombre;
            zonaReturn.Latitud = (double)xZ.Latitud;
            zonaReturn.Longitud = (double)xZ.Longitud;
            zonaReturn.Radio = xZ.Radio;
            return zonaReturn;
        }


        [TestMethod]
        public void CasoDePrueba5() {
            ///Dado un hogar y un período, mostrar por consola (interfaz de comandos) el consumo total. 
            ///Dado un dispositivo y un período, mostrar por consola su consumo promedio.
            ///Dado un transformador y un período, mostrar su consumo promedio.
            ///Recuperar un dispositivo asociado a un hogar de ese transformador e incrementar un 1000 % el consumo para ese período. 
            ///Persistir el dispositivo.
            ///Nuevamente mostrar el consumo para ese transformador
            DateTime fechaHasta = DateTime.Now.AddDays(1);
            DateTime fechaDesde = fechaHasta.AddMonths(-1);
            BaseRepositorio<Cliente> repoCliente = new BaseRepositorio<Cliente>();
            BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>();
            BaseRepositorio<Transformador> repoTransformador = new BaseRepositorio<Transformador>();

            Inteligente inteligente = new Inteligente("TV_cp5", 100m, new SonyTVDriver());
            repoInteligente.Create(inteligente);

            Cliente cliente = new Cliente() {
                Nombre = "nombre_cp5",
                NombreUsuario = "nombreUsuario_cp5",
                Apellido = "Apellido_cp5",
                Latitud = 10,
                Longitud = 11
            };

            cliente.Inteligentes.Add(inteligente);

            repoCliente.Create(cliente);

            Transformador transformador = new Transformador() {
                Latitud = 10,
                Longitud = 11
            };
            transformador.Clientes.Add(cliente);

            repoTransformador.Create(transformador);

            Console.WriteLine("Consumo por hogar en el período '" + fechaDesde.ToShortDateString() + "' y '" + fechaHasta.ToShortDateString() + "': " +
                Reporte.consumoPorHogarYPeriodo(cliente.Id, fechaDesde, fechaHasta));
            Console.WriteLine("Consumo por dispositivo en el período '" + fechaDesde.ToShortDateString() + "' y '" + fechaHasta.ToShortDateString() + "': " +
                Reporte.consumoPorTipoDeDispositivoPorPeriodo("INTELIGENTE", fechaDesde, fechaHasta));
            Console.WriteLine("Consumo por transformador en el período '" + fechaDesde.ToShortDateString() + "' y '" + fechaHasta.ToShortDateString() + "': " +
                Reporte.consumoTransformadorPorPeriodo(transformador.Id, fechaDesde, fechaHasta));

            Inteligente inteligenteConsultado = repoTransformador.Single(t => t.Id == transformador.Id).Clientes[0].Inteligentes[0];
            inteligenteConsultado.ConsumoEnergia = inteligenteConsultado.ConsumoEnergia * 10;

            repoInteligente.Update(inteligenteConsultado);

            Console.WriteLine("Consumo por transformador en el período '" + fechaDesde.ToShortDateString() + "' y '" + fechaHasta.ToShortDateString() + "': " +
                Reporte.consumoTransformadorPorPeriodo(transformador.Id, fechaDesde, fechaHasta));
        }
    }
}
