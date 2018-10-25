using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGE.WebconAutenticacion.Acciones;
using SGE.WebconAutenticacion.Categorias;
using SGE.WebconAutenticacion.Dispositivos;
using SGE.WebconAutenticacion.Drivers;
using SGE.WebconAutenticacion.Reglas;
using SGE.WebconAutenticacion.Reglas.Operadores;
using SGE.WebconAutenticacion.Reportes;
using SGE.WebconAutenticacion.Repositorio;
using SGE.WebconAutenticacion.Transformadores;
using SGE.WebconAutenticacion.Usuarios;
using SGE.WebconAutenticacion.ValueProviders;
using SGE.WebconAutenticacion.Zonas;
using System;
using SGE.Core.Helpers;
using System.Collections.Generic;
using System.Linq;
using SGE.WebconAutenticacion.Acciones.AA;

namespace SGE.Tests.Entrega {
    [TestClass]
    public class EntregaTest
    {
        BaseRepositorio<Cliente> repoCliente = new BaseRepositorio<Cliente>();
        BaseRepositorio<Zona> repoZona = new BaseRepositorio<Zona>();
        BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>();
        BaseRepositorio<Activacion> repoActivacion = new BaseRepositorio<Activacion>();
        BaseRepositorio<Transformador> repoTransformador = new BaseRepositorio<Transformador>();
        BaseRepositorio<SensorTemperaturaAA> repoSensor = new BaseRepositorio<SensorTemperaturaAA>();
        BaseRepositorio<Medicion> repoMedicion = new BaseRepositorio<Medicion>();
        BaseRepositorio<SamsungAireAcondicionadoDriver> repoActuador = new BaseRepositorio<SamsungAireAcondicionadoDriver>();
        BaseRepositorio<EstablecerTemperaturaAireAcondicionado> repoAccion = new BaseRepositorio<EstablecerTemperaturaAireAcondicionado>();
        BaseRepositorio<Regla> repoRegla = new BaseRepositorio<Regla>();
        BaseRepositorio<Condicion> repoCondicion = new BaseRepositorio<Condicion>();

        Zona zona = null;
        Cliente cliente = null;

        Inteligente dispositivoInteligente = null;
        SensorTemperaturaAA sensor = null;
        Medicion medicion = null;

        SamsungAireAcondicionadoDriver actuador = null;
        EstablecerTemperaturaAireAcondicionado accion = null;
        Regla regla = null;
        Condicion condicion = null;

        //TODO: se crean los casos de pruebas mínimos para la entrega 3. Para facilitar la lectura se los agrupa en esta clase,
        //sin embargo luego se acomodarán las pruebas en las clases correspondientes.
        [TestInitialize]
        public void TestInitialize() {           
            zona = new Zona() {
                codigo = 99,
                Nombre = "zona_02",
                Latitud = 35,
                Longitud = 45,
                Radio = 5
            };

            cliente = new Cliente() {
                Nombre = "Nombre_test_cp1",
                Apellido = "Apellido_test_cp1",
                NombreUsuario = "NombreUsuario_test_cp1",
                Password = "Cam_167", 

                NumeroDocumento = "2345678",
                Latitud = 60,
                Longitud = 29,
                Telefonos = new List<Telefono>()
            };
            cliente.TipoDocumento = Cliente.enum_TipoDocumento.DNI;
            cliente.Direccion = new Direccion() {
                Calle = "calle_cp1",
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
//TODO : inicializacion de sensor, actuador, medicion, accion, regla y condicion
// se usa en caso 2 y 3. 

            sensor = new SensorTemperaturaAA(35, new SamsungAireAcondicionadoDriver());
            dispositivoInteligente = new Inteligente("AireA-x_cp2", 100, new SamsungAireAcondicionadoDriver());
            medicion = new Medicion(350, UnidadEnum.CENTIGRADOS);

            actuador = new SamsungAireAcondicionadoDriver() {
                Mensaje = "PruebaActuador",
                temperaturaActual = 20
            };

            accion = new EstablecerTemperaturaAireAcondicionado()
            {
                Descripcion = "CambiarTemperaturaAire"
            };

            regla = new Regla() {
                Nombre = "ReglaAhorroTemperatura"
            };

            condicion = new Condicion(new Mayor(), 30);
          
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
         
            Console.WriteLine("Cliente Persistido: " + cliente.Apellido + " Latitud: " + cliente.Latitud + " Longitud: " + cliente.Longitud);
           
            Cliente clienteConsultado = repoCliente.Single(c => c.Id == cliente.Id);

            clienteConsultado.Latitud = 3;
            clienteConsultado.Longitud = 4;

            repoCliente.Update(clienteConsultado);

            Console.WriteLine("Cliente Modificado: " + cliente.Apellido + " Latitud: " + cliente.Latitud + " Longitud: " + cliente.Longitud);
            Cliente clienteConsultado2 = repoCliente.Single(c => c.Id == cliente.Id);

            Assert.AreEqual(clienteConsultado2.Latitud, 3);
            Assert.AreEqual(clienteConsultado2.Longitud, 4);
            //TODO: verificar las eliminaciones
        }

        [TestMethod]
        public void CasoDePrueba2() {
            ///Recuperar un dispositivo. (o sea que ya pertenece a un cliente)
            ///Mostrar por consola todos los intervalos que estuvo encendido durante el último mes.
            ///Modificar su nombre(o cualquier otro atributo editable) y grabarlo.
            ///Recuperarlo y evaluar que el nombre coincida con el esperado.

            cliente.Nombre = "Test_cp2";
            cliente.NumeroDocumento = "3521634";
            cliente.TipoDocumento = Cliente.enum_TipoDocumento.PASAPORTE;
            repoZona.Create(zona);
            cliente.Transformador.ZonaId = zona.Id;
            repoCliente.Create(cliente);

            repoSensor.Create(sensor);
            dispositivoInteligente.SensorId = sensor.Id;
            medicion.SensorId = sensor.Id;

            repoActuador.Create(actuador);
            dispositivoInteligente.ActuadorId = actuador.Id;
            repoInteligente.Create(dispositivoInteligente);
            repoMedicion.Create(medicion);

            accion.ActuadorId = actuador.Id;
            repoRegla.Create(regla);
            accion.ReglaId = regla.ReglaId;
            repoAccion.Create(accion);

            dispositivoInteligente.Encender();
            dispositivoInteligente.RegistroDeActivaciones.ElementAt(0).FechaDeRegistro = dispositivoInteligente.RegistroDeActivaciones.ElementAt(0).FechaDeRegistro.AddHours(-165);
            dispositivoInteligente.Apagar();
            dispositivoInteligente.RegistroDeActivaciones.ElementAt(1).FechaDeRegistro = dispositivoInteligente.RegistroDeActivaciones.ElementAt(1).FechaDeRegistro.AddHours(-80);
            dispositivoInteligente.Encender();
            dispositivoInteligente.RegistroDeActivaciones.ElementAt(2).FechaDeRegistro = dispositivoInteligente.RegistroDeActivaciones.ElementAt(2).FechaDeRegistro.AddHours(-43);
            dispositivoInteligente.Apagar();
            dispositivoInteligente.RegistroDeActivaciones.ElementAt(3).FechaDeRegistro = dispositivoInteligente.RegistroDeActivaciones.ElementAt(3).FechaDeRegistro.AddHours(-15);

            List<string> intervalosEncendido = dispositivoInteligente.ObtenerIntervalosEncendidoPorPeriodo(DateTime.Now.AddMonths(-1), DateTime.Now);

            foreach (string intervaloEncendido in intervalosEncendido) {
                Console.WriteLine(intervaloEncendido); 
            }

            dispositivoInteligente.Nombre = "nombre modificado";

            //TODO: Se debe persistir en el momento en el que cambia de estado, no aca
            //foreach (Activacion activacion in dispositivoInteligente.RegistroDeActivaciones) {
            //    repoActivacion.Create(activacion);
            //}

            repoInteligente.Update(dispositivoInteligente);

            Inteligente dispositivoModificado = repoInteligente.Single(i => i.Id == dispositivoInteligente.Id);

            Assert.AreEqual(dispositivoModificado.Nombre, "nombre modificado");

            //-----------------------------------
            //repoAccion.Delete(accion);
            //repoRegla.Delete(regla);
            //repoMedicion.Delete(medicion);
            //repoActivacion.Delete(a => a.Id >= 1);
            //repoInteligente.Delete(dispositivoInteligente);
            //repoSensor.Delete(sensor);
            //repoActuador.Delete(actuador);
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
            /*    Inteligente dispositivo = new Inteligente("TV_cp3", 100m, new SonyTVDriver());

                repoInteligente.Create(dispositivo);

                Regla regla = new Regla();
                regla.Nombre = "ReglaCalorEncenderAire";
                regla.Condiciones.Add(new Condicion(new SensorTemperaturaAA(new SamsungAireAcondicionadoDriver()), new Mayor(), 30));
                regla.Accions.Add(new Encender(new AireAcondicionado(new SamsungAireAcondicionadoDriver(), "AA Samsung", 100)));
               */
            
            repoSensor.Create(sensor);
            dispositivoInteligente.SensorId = sensor.Id;
            medicion.SensorId = sensor.Id;

            repoActuador.Create(actuador);
            dispositivoInteligente.ActuadorId = actuador.Id;
            repoInteligente.Create(dispositivoInteligente);
            repoMedicion.Create(medicion);

            repoRegla.Create(regla);
            accion.ActuadorId = actuador.Id;
            condicion.ReglaId = regla.ReglaId;

            //se crea condicion en la BD y se muestra por aplicacion
            repoCondicion.Create(condicion);
           

            accion.ReglaId = regla.ReglaId;
            repoAccion.Create(accion);
            //Se crea la regla con la inicializacion usada en el caso 2, 
            // y probando el mapeo relacional, protegiendo las fk

            Console.WriteLine("Valor de condicion: " + condicion.valorReferencia);
            //se modifica  y se muestra
            condicion.valorReferencia = 150;
            repoCondicion.Update(condicion);
            Console.WriteLine("Valor de condicion Modificado: " + condicion.valorReferencia);

            //se crea regla en la BD y se muestra por aplicacion
            Console.WriteLine(regla.Nombre);
            //se modifica  y se muestra
            regla.Nombre = "ReglaControlTemperatura";
            repoRegla.Update(regla);
            Console.WriteLine(regla.Nombre);

            ///////////////////////////////////////////////////////////////////////
       /*   TODO: Si se borran los repos, no se persisten a la base, entonces no se puede comprobar
        *   la creacion y modificacion de la regla y condicion, asociada al dispositivo
        *   repoAccion.Delete(accion);
            repoCondicion.Delete(condicion);
            repoRegla.Delete(regla);
            repoMedicion.Delete(medicion);
            repoInteligente.Delete(dispositivoInteligente);
            repoActuador.Delete(actuador);
            repoSensor.Delete(sensor);
         */   
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
            Console.WriteLine("Cantidad de Transformadores: " + transPerMasUno.Count);
            /* TODO: si ejecuto los foreach y elimino los transformadores, no se persisten los datos a la base
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
                        } */
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
            cliente.NumeroDocumento = "1521634";
            cliente.TipoDocumento = Cliente.enum_TipoDocumento.DNI;
            //    cliente.Id = 1;          
            //TODO: se toman los datos de inicializacion de todo el modelo, sobre todo de Activacion, dado que
            // sino la funcion CalcularHorasDeUso(), que utiliza la funcion ObtenerConsumoPeriodo(), trae 0 dado que no habria registros en la tabla
            cliente.Inteligentes.Add(dispositivoInteligente);

            repoZona.Create(zona);
            cliente.Transformador.ZonaId = zona.Id;
            repoCliente.Create(cliente);

            Transformador transformador = cliente.Transformador;

            repoSensor.Create(sensor);
            dispositivoInteligente.SensorId = sensor.Id;
            medicion.SensorId = sensor.Id;

            repoActuador.Create(actuador);
            dispositivoInteligente.ActuadorId = actuador.Id;
            repoInteligente.Create(dispositivoInteligente);
            repoMedicion.Create(medicion);

            accion.ActuadorId = actuador.Id;
            repoRegla.Create(regla);
            accion.ReglaId = regla.ReglaId;
            repoAccion.Create(accion);
            //repoTransformador.Create(transformador);

            dispositivoInteligente.Encender();
            dispositivoInteligente.RegistroDeActivaciones.ElementAt(0).FechaDeRegistro = dispositivoInteligente.RegistroDeActivaciones.ElementAt(0).FechaDeRegistro.AddHours(-65);
            dispositivoInteligente.Apagar();
            dispositivoInteligente.RegistroDeActivaciones.ElementAt(1).FechaDeRegistro = dispositivoInteligente.RegistroDeActivaciones.ElementAt(1).FechaDeRegistro.AddHours(-40);
            dispositivoInteligente.Encender();
            dispositivoInteligente.RegistroDeActivaciones.ElementAt(2).FechaDeRegistro = dispositivoInteligente.RegistroDeActivaciones.ElementAt(2).FechaDeRegistro.AddHours(-23);
            dispositivoInteligente.Apagar();
            dispositivoInteligente.RegistroDeActivaciones.ElementAt(3).FechaDeRegistro = dispositivoInteligente.RegistroDeActivaciones.ElementAt(3).FechaDeRegistro.AddHours(-5);

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

            Console.WriteLine("Consumo Incrementado por transformador en el período '" + fechaDesde.ToShortDateString() + "' y '" + fechaHasta.ToShortDateString() + "': " +
                Reporte.consumoTransformadorPorPeriodo(transformador.Id, fechaDesde, fechaHasta));
        }
    }
}
