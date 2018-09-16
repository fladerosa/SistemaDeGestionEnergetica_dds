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
using System.Linq;

namespace SGE.Tests.Entrega {
    [TestClass]
    public class EntregaTest
    {
        BaseRepositorio<Cliente> repoCliente = new BaseRepositorio<Cliente>();
        BaseRepositorio<Zona> repoZona = new BaseRepositorio<Zona>();
        BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>();
        BaseRepositorio<Activacion> repoActivacion = new BaseRepositorio<Activacion>();
        BaseRepositorio<Transformador> repoTransformador = new BaseRepositorio<Transformador>();
        Zona zona = null;
        Cliente cliente = null;
        Inteligente dispositivoInteligente = null;

        //TODO: se crean los casos de pruebas mínimos para la entrega 3. Para facilitar la lectura se los agrupa en esta clase,
        //sin embargo luego se acomodarán las pruebas en las clases correspondientes.
        [TestInitialize]
        public void TestInitialize() {
            //se carga una zona para evitar que rompa por fk de transformador
            zona = new Zona() {
                codigo = 99,
                Nombre = "zona_02",
                Latitud = 35,
                Longitud = 45,
                Radio = 5
            };

            cliente = new Cliente() {
                Nombre = "Nombre_test_cp2",
                Apellido = "Apellido_test_cp2",
                NombreUsuario = "NombreUsuario_test_cp2",
                Password = "Password_test_02",

                NumeroDocumento = "2345678",
                Latitud = 3,
                Longitud = 4,
                Telefonos = new List<Telefono>()
            };
            cliente.TipoDocumento = Cliente.enum_TipoDocumento.DNI;
            cliente.Direccion = new Direccion() {
                Calle = "calle_cp2",
                Nro = "468"
            };
            cliente.Telefonos.Add(new Telefono() {
                Numero = "12345"
            });

            cliente.Transformador = new Transformador() {
                codigo = 99,
                Latitud = 5,
                Longitud = 15,
                ZonaId = zona.Id
            };

            cliente.Categoria = new Categoria() {
                Codigo = "R3",
                ConsumoMinimo = 1500,
                ConsumoMaximo = 2200,
                CostoFijo = 300,
                CostoVariable = 550
            };

            dispositivoInteligente = new Inteligente("TV_cp2", 100m, new SonyTVDriver());
        }

        [TestMethod]
        
        public void CasoDePrueba1() {
            ///Crear 1 usuario nuevo.
            ///Persistirlo.
            ///Recuperarlo, modificar la geolocalización
            ///grabarlo.
            ///Recuperarlo y evaluar que el cambio se haya realizado.
            repoZona.Create(zona);
            cliente.Transformador.ZonaId = zona.Id;
            repoCliente.Create(cliente);

            Cliente clienteConsultado = repoCliente.Single(c => c.Id == cliente.Id);

            clienteConsultado.Latitud = 3;
            clienteConsultado.Longitud = 4;

            repoCliente.Update(clienteConsultado);

            Cliente clienteConsultado2 = repoCliente.Single(c => c.Id == cliente.Id);

            Assert.AreEqual(clienteConsultado2.Latitud, 3);
            Assert.AreEqual(clienteConsultado2.Longitud, 4);
            //TODO: verificar las eliminaciones
        }

        [TestMethod]
        public void CasoDePrueba2() {
            ///Recuperar un dispositivo.
            ///Mostrar por consola todos los intervalos que estuvo encendido durante el último mes.
            ///Modificar su nombre(o cualquier otro atributo editable) y grabarlo.
            ///Recuperarlo y evaluar que el nombre coincida con el esperado.
            repoInteligente.Create(dispositivoInteligente);

            dispositivoInteligente.Encender();
            dispositivoInteligente.RegistroDeActivaciones.ElementAt(0).FechaDeRegistro = dispositivoInteligente.RegistroDeActivaciones.ElementAt(0).FechaDeRegistro.AddHours(-25);
            dispositivoInteligente.Apagar();
            dispositivoInteligente.RegistroDeActivaciones.ElementAt(1).FechaDeRegistro = dispositivoInteligente.RegistroDeActivaciones.ElementAt(1).FechaDeRegistro.AddHours(-4);
            dispositivoInteligente.Encender();
            dispositivoInteligente.RegistroDeActivaciones.ElementAt(2).FechaDeRegistro = dispositivoInteligente.RegistroDeActivaciones.ElementAt(2).FechaDeRegistro.AddHours(-3);
            dispositivoInteligente.Apagar();
            dispositivoInteligente.RegistroDeActivaciones.ElementAt(3).FechaDeRegistro = dispositivoInteligente.RegistroDeActivaciones.ElementAt(3).FechaDeRegistro.AddHours(-1);

            List<string> intervalosEncendido = dispositivoInteligente.ObtenerIntervalosEncendidoPorPeriodo(DateTime.Now.AddMonths(-1), DateTime.Now);

            foreach (string intervaloEncendido in intervalosEncendido) {
                Console.WriteLine(intervaloEncendido);
            }

            dispositivoInteligente.Nombre = "nombre modificado";

            //TODO: Se debe persistir en el momento en el que cambia de estado, no aca
            foreach (Activacion activacion in dispositivoInteligente.RegistroDeActivaciones) {
                repoActivacion.Create(activacion);
            }

            repoInteligente.Update(dispositivoInteligente);

            Inteligente dispositivoModificado = repoInteligente.Single(i => i.Id == dispositivoInteligente.Id);

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

            foreach (Core.Entidades.Zona z in zonaHelper.Zonas)
            {
                repoZona.Create(AsignarAtributosZona(z));
            }


            //Persisto los transformadores que el ENRE envia
            TransformadoresHelper transHelper = new TransformadoresHelper();
            foreach (Core.Entidades.Transformador t in transHelper.Transformadores)
            {
                repoTransformador.Create(new Transformador() {
                    codigo = t.codigo,
                    Latitud = (double)t.Latitud,
                    Longitud = (double)t.Longitud,
                    ZonaId = t.Zona
                });
            }

            //Recupero los transformadores persistidos
            List<Transformador> transPer = repoTransformador.GetAll();

            //cargamos los transformadores desde el json con una instancia de transformador mas
            string nombreArchivoJsonMasUno = "transformadoresConUnoMas.json";
            TransformadoresHelper transHelperMasUno = new TransformadoresHelper(nombreArchivoJsonMasUno);
            //persistimos los transformadores
            foreach (Core.Entidades.Transformador t in transHelperMasUno.Transformadores)
            {
                Transformador retorno = repoTransformador.Single(s => s.codigo == t.codigo);
                if (retorno != null)
                {
                    retorno.codigo = t.codigo;
                    retorno.Latitud = (double)t.Latitud;
                    retorno.Longitud = (double)t.Longitud;
                    retorno.ZonaId = t.Zona;
                    repoTransformador.Update(retorno);
                } else {
                    repoTransformador.Create(new Transformador() {
                        codigo = t.codigo,
                        Latitud = (double)t.Latitud,
                        Longitud = (double)t.Longitud,
                        ZonaId = t.Zona
                    });
                }

            }
            List<Transformador> transPerMasUno = repoTransformador.GetAll();

            // Evaluo que la cantidad actual sea la anterior + 1
            Assert.IsTrue(transHelperMasUno.Transformadores.Count <= transPerMasUno.Count);

            //eliminamos los registros con los que hicimos las pruebas
            foreach (Core.Entidades.Transformador transformador in transHelperMasUno.Transformadores) {
                Transformador transformadorDB = repoTransformador.Single(t => t.codigo == transformador.codigo);
                if (transformadorDB != null) {
                    repoTransformador.Delete(transformadorDB);
                }
            }

            //eliminamos los registros con los que hicimos las pruebas
            foreach (Core.Entidades.Zona zona in zonaHelper.Zonas) {
                Zona zonaDB = repoZona.Single(z => z.codigo == zona.codigo);
                if(zonaDB != null) {
                    repoZona.Delete(zonaDB);
                }
            }
        }

        private Zona AsignarAtributosZona(Core.Entidades.Zona xZ)
        {
            Zona zonaReturn = new Zona();
            zonaReturn.Id = xZ.Id;
            zonaReturn.codigo = xZ.codigo;
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

            cliente.Nombre = "Test_cp5";
            cliente.Id = 0;

            cliente.Inteligentes.Add(dispositivoInteligente);

            repoZona.Create(zona);
            cliente.Transformador.ZonaId = zona.Id;
            repoCliente.Create(cliente);

            Transformador transformador = cliente.Transformador;

            //repoTransformador.Create(transformador);

            Console.WriteLine("Consumo por hogar en el período '" + fechaDesde.ToShortDateString() + "' y '" + fechaHasta.ToShortDateString() + "': " +
                Reporte.consumoPorHogarYPeriodo(cliente.Id, fechaDesde, fechaHasta));
            Console.WriteLine("Consumo por dispositivo en el período '" + fechaDesde.ToShortDateString() + "' y '" + fechaHasta.ToShortDateString() + "': " +
                Reporte.consumoPorTipoDeDispositivoPorPeriodo("INTELIGENTE", fechaDesde, fechaHasta));
            Console.WriteLine("Consumo por transformador en el período '" + fechaDesde.ToShortDateString() + "' y '" + fechaHasta.ToShortDateString() + "': " +
                Reporte.consumoTransformadorPorPeriodo(transformador.Id, fechaDesde, fechaHasta));

            int idInteligente = cliente.Inteligentes.First().Id;
            Inteligente inteligenteConsultado = repoInteligente.Single(i => i.Id == idInteligente);
            inteligenteConsultado.ConsumoEnergia = inteligenteConsultado.ConsumoEnergia * 10;

            repoInteligente.Update(inteligenteConsultado);

            Console.WriteLine("Consumo por transformador en el período '" + fechaDesde.ToShortDateString() + "' y '" + fechaHasta.ToShortDateString() + "': " +
                Reporte.consumoTransformadorPorPeriodo(transformador.Id, fechaDesde, fechaHasta));
        }
    }
}
