using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SGE.Entidades.Acciones;
using SGE.Entidades.Contexto;
using SGE.Entidades.Dispositivos;
using SGE.Entidades.Reglas;
using SGE.Entidades.Reportes;
using SGE.Entidades.Repositorio;
using SGE.Entidades.Sesion;
using SGE.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;

namespace SGE.WebconAutenticacion.Areas.Cli.Controllers {
    public class HogarController : Controller {
        // GET: Cliente/Hogar
        public ActionResult Index() {
            ViewBag.ultimasMediciones = obtenerUltimasMediciones();
            ViewBag.consumo = obtenerConsumoUltimoMes();
            ViewBag.dispositivosEstado = obtenerEstadosDispositivos();
            ViewBag.reglasActivas = obtenerReglasActivas();
            return View();
        }

        private decimal obtenerConsumoUltimoMes() {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = UserManager.FindById(User.Identity.GetUserId());

            SGEContext contexto = new SGEContext();

            BaseRepositorio<Cliente> repoCliente = new BaseRepositorio<Cliente>(contexto);
            Cliente cliente = repoCliente.Single(c => c.NombreUsuario == user.UserName);

            DateTime fDesde = DateTime.Now.AddMonths(-1);
            DateTime fHasta = DateTime.Now;

            return Reporte.consumoPorHogarYPeriodo(cliente.Id, fDesde, fHasta);
        }

        private ICollection<Inteligente> obtenerEstadosDispositivos() {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = UserManager.FindById(User.Identity.GetUserId());

            BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>();
            var includesInteligente = new List<Expression<Func<Inteligente, object>>>() {
                i => i.RegistroDeActivaciones
            };

            var inteligentes = repoInteligente.Filter(i => i.Clientes.Any(c => c.NombreUsuario == user.UserName), includesInteligente);
            return inteligentes;
        }

        private ICollection<dynamic> obtenerUltimasMediciones() {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = UserManager.FindById(User.Identity.GetUserId());
            ICollection<dynamic> salida = new List<dynamic>();

            SGEContext contexto = new SGEContext();

            BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>(contexto);
            var includesInteligente = new List<Expression<Func<Inteligente, object>>>() {
                i => i.Sensor
            };

            var inteligentes = repoInteligente.Filter(i => i.Clientes.Any(c => c.NombreUsuario == user.UserName), includesInteligente);

            var includesMedicion = new List<Expression<Func<Medicion, object>>>() {
                m => m.Sensor
            };
            BaseRepositorio<Medicion> repoMedicion = new BaseRepositorio<Medicion>(contexto);
            foreach (Inteligente inteligente in inteligentes) {
                if (inteligente.SensorId != null) {
                    var medicion = repoMedicion.Filter(m => m.SensorId == inteligente.SensorId, includesMedicion);

                    dynamic customMedicion = new ExpandoObject();
                    customMedicion.dispositivo = inteligente.Nombre;
                    customMedicion.sensor = inteligente.Sensor.Id;
                    customMedicion.medicion = medicion.LastOrDefault();

                    salida.Add(customMedicion);
                }
            }

            return salida;
        }

        private ICollection<dynamic> obtenerReglasActivas() {
            ICollection<dynamic> salida = new List<dynamic>();

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = UserManager.FindById(User.Identity.GetUserId());

            SGEContext contexto = new SGEContext();

            BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>(contexto);
            var includesInteligente = new List<Expression<Func<Inteligente, object>>>() {
                i => i.Actuador
            };

            var inteligentes = repoInteligente.Filter(i => i.Clientes.Any(c => c.NombreUsuario == user.UserName), includesInteligente);

            BaseRepositorio<Accion> repoAccion = new BaseRepositorio<Accion>(contexto);
            var includesAccion = new List<Expression<Func<Accion, object>>>() {
                a => a.Regla
            };

            BaseRepositorio<Condicion> repoCondicion = new BaseRepositorio<Condicion>(contexto);
            foreach (Inteligente inteligente in inteligentes) {
                if (inteligente.ActuadorId != null) {
                    var acciones = repoAccion.Filter(a => a.ActuadorId == inteligente.ActuadorId, includesAccion);
                    if(acciones.Count > 0) {
                        var reglaId = acciones.First().ReglaId;
                        var condiciones = repoCondicion.Filter(c => c.ReglaId == reglaId);

                        if(condiciones.Count > 0) {
                            string strCondiciones = "";
                            foreach(Condicion condicion in condiciones) {
                                if (strCondiciones != "") strCondiciones += " | ";
                                string strTipoOperacion = condicion.tipoOperacion.GetType()
                                    .GetMember(condicion.tipoOperacion.ToString())
                                    .First()
                                    .GetCustomAttribute<DisplayAttribute>()
                                    .GetName();
                                strCondiciones += condicion.valorReferencia.ToString() + " " + strTipoOperacion;
                            }

                            string strAcciones = "";
                            foreach (Accion accion in acciones) {
                                if (strAcciones != "") strAcciones += " | ";
                                strAcciones += accion.Descripcion;
                            }


                            dynamic customRegla = new ExpandoObject();
                            customRegla.regla = acciones.First().Regla.Nombre;
                            customRegla.condicion = "{" + strCondiciones + "} => {" + strAcciones + "}";

                            salida.Add(customRegla);
                        }
                    }
                }
            }

            return salida;
        }
    }
}