using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SGE.Entidades.Acciones;
using SGE.Entidades.Contexto;
using SGE.Entidades.Dispositivos;
using SGE.Entidades.Reglas;
using SGE.Entidades.Repositorio;
using SGE.Entidades.Sensores;
using SGE.Entidades.Sesion;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SGE.WebconAutenticacion.Areas.Cli.Controllers {
    public class ReglasController : Controller
    {
        // GET: Cliente/Reglas
        public ActionResult Index()
        {
            ViewBag.reglas = ObtenerReglasActivas();

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = UserManager.FindById(User.Identity.GetUserId());

            BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>();
            ViewBag.tieneDispositivos = repoInteligente.Filter(i => i.Clientes.Any(c => c.NombreUsuario == user.UserName)).Count > 0;

            return View();
        }

        public ActionResult Agregar() {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = UserManager.FindById(User.Identity.GetUserId());

            BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>();
            ViewBag.inteligentes = repoInteligente.Filter(i => i.Clientes.Any(c => c.NombreUsuario == user.UserName));

            BaseRepositorio<Operador> repoOperador = new BaseRepositorio<Operador>();
            ViewBag.Operadores = new SelectList(repoOperador.GetAll(), "Id", "Descripcion");

            return View();
        }

        [HttpPost]
        public JsonResult CargarAccionesYSensores(int idInteligente) {
            SGEContext db = new SGEContext();

            var jsonSerialiser = new JavaScriptSerializer();
            List<object> acciones = new List<object>();

            Inteligente inteligente = db.Inteligentes.First(i => i.Id == idInteligente);
            Catalogo catalogo = db.Catalogos.Include("Acciones").Include("Sensores").First(c => c.Id == inteligente.CatalogoId);

            foreach (Accion accion in catalogo.Acciones) {
                var objeto = Json(new { accion.Id, accion.Descripcion }).Data;

                acciones.Add(objeto);
            }

            List<object> sensores = new List<object>();

            foreach (Sensor sensor in catalogo.Sensores) {
                var objeto = Json(new { sensor.Id, sensor.Descripcion }).Data;

                sensores.Add(objeto);
            }

            return Json(new { success = true, sensores = jsonSerialiser.Serialize(sensores), acciones = jsonSerialiser.Serialize(acciones) });
        }

        [HttpPost]
        public JsonResult AgregarRegla(string nombreRegla, int idInteligente, long[] idsAcciones, List<Condicion> condiciones) {
            SGEContext db = new SGEContext();
            BaseRepositorio<Regla> repoRegla = new BaseRepositorio<Regla>(db);
            Regla regla = new Regla() {
                Nombre = nombreRegla,
                IdInteligente = idInteligente,
                Condiciones = condiciones
            };

            regla.Acciones = db.Acciones.Where(a => idsAcciones.Any(x => x == a.Id)).ToList();

            repoRegla.Create(regla);

            return Json(new { success = true });
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
                c => c.Operador
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
                            strCondiciones += condicion.ValorReferencia.ToString() + " " + strTipoOperacion;
                        }

                        string strAcciones = "";
                        foreach (Accion accion in repoAccion.Filter(a => a.Reglas.Any(r => r.ReglaId == reglaId))) {
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