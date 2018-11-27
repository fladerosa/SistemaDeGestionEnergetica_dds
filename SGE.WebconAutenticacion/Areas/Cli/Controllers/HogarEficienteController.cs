using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SGE.Entidades.Contexto;
using SGE.Entidades.Dispositivos;
using SGE.Entidades.Repositorio;
using SGE.Entidades.Sesion;
using SGE.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace SGE.WebconAutenticacion.Areas.Cli.Controllers {
    public class HogarEficienteController : Controller {
        // GET: Cli/HogarEficiente
        public ActionResult Index() {
            Dictionary<string, double> resultadoSimplex = ejecucionSimplex();

            if (resultadoSimplex != null) {
                ViewBag.ConsumoRestanteTotal = resultadoSimplex["ConsumoRestanteTotal"];
                ViewBag.ResultadoSimplex = resultadoSimplex;
            }

            return View();
        }

        public JsonResult ejecutarSimplex() {
            Dictionary<string, double> resultadoSimplex = ejecucionSimplex();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = UserManager.FindById(User.Identity.GetUserId());
            SGEContext db = new SGEContext();

            if (resultadoSimplex == null) {
                return Json(new { success = false, error = "No se puede ejecutar el simplex" });
            }

            BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>(db);
            var includesCliente = new List<Expression<Func<Inteligente, object>>>() {
                i => i.RegistroDeActivaciones,
                i => i.Clientes
            };
            Inteligente inteligente = null;
            foreach (KeyValuePair<string, double> item in resultadoSimplex) {
                if(item.Value > 0 && item.Key != "TotalHorasRestantes" && item.Key != "ConsumoRestanteTotal") {
                    inteligente = repoInteligente.Single(i => i.Nombre == item.Key && i.Clientes.Any(c => c.NombreUsuario == user.UserName), includesCliente);
                    inteligente.Encender();
                    repoInteligente.Update(inteligente);
                }
            }

            return Json(new { success = true });
        }

        private Dictionary<string, double> ejecucionSimplex() {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = UserManager.FindById(User.Identity.GetUserId());

            SGEContext db = new SGEContext();

            BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>(db);
            var includesCliente = new List<Expression<Func<Inteligente, object>>>() {
                i => i.RegistroDeActivaciones,
                i => i.Clientes
            };
            List<Inteligente> inteligentes = repoInteligente.Filter(i => i.Clientes.Any(c => c.NombreUsuario == user.UserName), includesCliente);

            Cliente cliente = new Cliente();

            cliente.Inteligentes = inteligentes;

            return cliente.HogarEficiente();
        }
    }
}
