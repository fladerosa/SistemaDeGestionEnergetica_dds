using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SGE.Entidades.Acciones;
using SGE.Entidades.Contexto;
using SGE.Entidades.Dispositivos;
using SGE.Entidades.Reglas;
using SGE.Entidades.Reportes;
using SGE.Entidades.Repositorio;
using SGE.Entidades.Sensores;
using SGE.Entidades.Sesion;
using SGE.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace SGE.WebconAutenticacion.Areas.Cli.Controllers {
    public class HogarController : Controller {
        // GET: Cliente/Hogar
        public ActionResult Index() {
            ViewBag.ultimasMediciones = ObtenerUltimasMediciones();
            ViewBag.consumo = ObtenerConsumoUltimoMes();
            ViewBag.dispositivosEstado = ObtenerEstadosDispositivos();
            ViewBag.reglasActivas = ObtenerReglasActivas();
            return View();
        }

        private decimal ObtenerConsumoUltimoMes() {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = UserManager.FindById(User.Identity.GetUserId());

            SGEContext contexto = new SGEContext();

            BaseRepositorio<Cliente> repoCliente = new BaseRepositorio<Cliente>(contexto);
            Cliente cliente = repoCliente.Single(c => c.NombreUsuario == user.UserName);

            DateTime fDesde = DateTime.Now.AddMonths(-1);
            DateTime fHasta = DateTime.Now;

            return Reporte.consumoPorHogarYPeriodo(cliente.Id, fDesde, fHasta);
        }

        private ICollection<Inteligente> ObtenerEstadosDispositivos() {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = UserManager.FindById(User.Identity.GetUserId());

            BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>();
            var includesInteligente = new List<Expression<Func<Inteligente, object>>>() {
                i => i.RegistroDeActivaciones
            };

            var inteligentes = repoInteligente.Filter(i => i.Clientes.Any(c => c.NombreUsuario == user.UserName), includesInteligente);
            return inteligentes;
        }

        private ICollection<dynamic> ObtenerUltimasMediciones() {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = UserManager.FindById(User.Identity.GetUserId());
            ICollection<dynamic> salida = new List<dynamic>();

            SGEContext contexto = new SGEContext();

            BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>(contexto);
            var includesInteligente = new List<Expression<Func<Inteligente, object>>>() {
                i => i.Catalogo
            };

            var inteligentes = repoInteligente.Filter(i => i.Clientes.Any(c => c.NombreUsuario == user.UserName), includesInteligente);

            BaseRepositorio<Medicion> repoMedicion = new BaseRepositorio<Medicion>(contexto);
            foreach (Inteligente inteligente in inteligentes) {
                if (inteligente.Catalogo.Sensores != null && inteligente.Catalogo.Sensores.Count > 0) {
                    foreach (SensorFisico sensor in inteligente.Sensores) {
                        dynamic customMedicion = new ExpandoObject();
                        customMedicion.dispositivo = inteligente.Nombre;
                        customMedicion.sensor = sensor.Id;
                        customMedicion.medicion = sensor.Mediciones.LastOrDefault();

                        salida.Add(customMedicion);
                    }
                }
            }

            return salida;
        }

        private ICollection<dynamic> ObtenerReglasActivas() {
            ICollection<dynamic> salida = new List<dynamic>();

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = UserManager.FindById(User.Identity.GetUserId());

            SGEContext contexto = new SGEContext();

            BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>(contexto);
            var includesInteligente = new List<Expression<Func<Inteligente, object>>>() {
                i => i.Reglas
            };

            var inteligentes = repoInteligente.Filter(i => i.Clientes.Any(c => c.NombreUsuario == user.UserName), includesInteligente);

            BaseRepositorio<Accion> repoAccion = new BaseRepositorio<Accion>(contexto);
            var includesAccion = new List<Expression<Func<Accion, object>>>() {
                a => a.Reglas
            };

            BaseRepositorio<Condicion> repoCondicion = new BaseRepositorio<Condicion>(contexto);
            var includesCondicion = new List<Expression<Func<Condicion, object>>>() {
                c => c.Operador,
                c => c.Sensor
            };
            foreach (Inteligente inteligente in inteligentes) {
                foreach (Regla regla in inteligente.Reglas) {
                    var reglaId = regla.ReglaId;
                    var condiciones = repoCondicion.Filter(c => c.ReglaId == reglaId, includesCondicion);

                    if (condiciones.Count > 0) {
                        string strCondiciones = "";
                        foreach (Condicion condicion in condiciones) {
                            if (strCondiciones != "") strCondiciones += " | ";
                            string strTipoOperacion = condicion.Operador.Descripcion;
                            strCondiciones += condicion.Sensor.TipoSensor.Descripcion + " " + strTipoOperacion.ToLower() + " a " + condicion.ValorReferencia.ToString() + " ";
                        }

                        string strAcciones = "";
                        foreach (Accion accion in regla.Acciones) {
                            if (strAcciones != "") strAcciones += " | ";
                            strAcciones += accion.Descripcion;
                        }


                        dynamic customRegla = new ExpandoObject();
                        customRegla.regla = regla.Nombre;
                        customRegla.condicion = "{" + strCondiciones + "} => {" + strAcciones + "}";

                        salida.Add(customRegla);
                    }
                }
            }

            return salida;
        }
    }
}