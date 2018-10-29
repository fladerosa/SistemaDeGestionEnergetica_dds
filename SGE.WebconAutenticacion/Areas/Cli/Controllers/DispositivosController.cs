using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SGE.Entidades.Contexto;
using SGE.Entidades.Dispositivos;
using SGE.Entidades.Repositorio;
using SGE.Entidades.Sesion;
using SGE.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web.Mvc;

namespace SGE.WebconAutenticacion.Areas.Cli.Controllers {
    public class DispositivosController : Controller
    {

        // GET: Cliente/Inteligentes
        public ActionResult Index() {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = UserManager.FindById(User.Identity.GetUserId());

            BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>();

            var inteligentes = repoInteligente.Filter(i => i.Clientes.Any(c => c.NombreUsuario == user.UserName));

            return View(inteligentes);
        }

        // GET: Cliente/Inteligentes/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>();
            Inteligente inteligente = repoInteligente.Single(i => i.Id == id);
            if (inteligente == null) {
                return HttpNotFound();
            }
            return View(inteligente);
        }

        // GET: Cliente/Inteligentes/Create
        public ActionResult Agregar() {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = UserManager.FindById(User.Identity.GetUserId());

            BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>();
            var Inteligentes = repoInteligente.Filter(i => !i.Clientes.Any(c => c.NombreUsuario == user.UserName));
            return View(Inteligentes.ToList());
        }

        [HttpPost]
        public JsonResult Agregar(int idInteligente) {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = UserManager.FindById(User.Identity.GetUserId());

            SGEContext contexto = new SGEContext();

            BaseRepositorio<Cliente> repoCliente = new BaseRepositorio<Cliente>(contexto);
            Cliente cliente = repoCliente.Single(c => c.NombreUsuario == user.UserName);

            BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>(contexto);
            Inteligente inteligente = repoInteligente.Single(i => i.Id == idInteligente);

            inteligente.Clientes.Add(cliente);
            repoInteligente.Update(inteligente);

            return Json(new { success = true});
        }

        [HttpPost]
        public JsonResult CambiarEstado(int idInteligente, EstadoDispositivo estado) {
            SGEContext context = new SGEContext();

            BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>(context);
            Inteligente inteligente = repoInteligente.Single(i => i.Id == idInteligente);
            inteligente.context = context;

            switch (estado) {
                case EstadoDispositivo.AhorroEnergia:
                    inteligente.ColocarEnAhorroEnergia();
                    break;
                case EstadoDispositivo.Apagado:
                    inteligente.Apagar();
                    break;
                case EstadoDispositivo.Encendido:
                    inteligente.Encender();
                    break;
                default:
                    return Json(new { success = false, error = "Estado desconocido" });
            }
            

            repoInteligente.Update(inteligente);

            return Json(new { success = true });
        }

        // GET: Cliente/Inteligentes/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>();
            Inteligente inteligente = repoInteligente.Single(i => i.Id == id);
            if (inteligente == null) {
                return HttpNotFound();
            }
            return View(inteligente);
        }

        // POST: Cliente/Inteligentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = UserManager.FindById(User.Identity.GetUserId());

            SGEContext contexto = new SGEContext();
            
            BaseRepositorio<Cliente> repoCliente = new BaseRepositorio<Cliente>(contexto);
            Cliente cliente = repoCliente.Single(c => c.NombreUsuario == user.UserName);

            BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>(contexto);
            var includesInteligente = new List<Expression<Func<Inteligente, object>>>() {
                i => i.Clientes
            };
            Inteligente inteligente = repoInteligente.Single(i => i.Id == id, includesInteligente);

            inteligente.Clientes.Remove(cliente);
            repoInteligente.Update(inteligente);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                SGEContext.instancia().Dispose();
            }
            base.Dispose(disposing);
        }
    }
}