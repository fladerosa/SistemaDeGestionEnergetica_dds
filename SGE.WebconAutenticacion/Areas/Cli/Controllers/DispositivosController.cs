using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SGE.Entidades.Contexto;
using SGE.Entidades.Dispositivos;
using SGE.Entidades.Repositorio;
using SGE.Entidades.Sensores;
using SGE.Entidades.Sesion;
using SGE.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web.Mvc;

namespace SGE.WebconAutenticacion.Areas.Cli.Controllers {
    public class DispositivosController : Controller {
        private SGEContext db = new SGEContext();

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
            return View(db.Catalogos.Include("Acciones").Include("Sensores").ToList());
        }

        [HttpPost]
        public JsonResult Agregar(int idCatalogo) {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = UserManager.FindById(User.Identity.GetUserId());

            SGEContext db = new SGEContext();

            BaseRepositorio<Cliente> repoCliente = new BaseRepositorio<Cliente>(db);
            Cliente cliente = repoCliente.Single(c => c.NombreUsuario == user.UserName);

            BaseRepositorio<Catalogo> repoCatalogo = new BaseRepositorio<Catalogo>(db);
            Catalogo Catalogo = repoCatalogo.Single(c => c.Id == idCatalogo);

            Inteligente inteligente = new Inteligente() {
                ConsumoEnergia = Catalogo.ConsumoEnergia,
                IdentificadorFabrica = Catalogo.IdentificadorFabrica,
                Catalogo = Catalogo,
                CatalogoId = Catalogo.Id
            };
            string nombreInteligente = Catalogo.Nombre + "_" + DateTime.Now.ToString("ddMMyyHHmmss");
            if (nombreInteligente.Length > 25) { nombreInteligente = nombreInteligente.Substring(0, 25); };
            inteligente.Nombre = nombreInteligente;

            inteligente.Clientes.Add(cliente);
            BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>(db);
            repoInteligente.Create(inteligente);

            db = new SGEContext();
            List<Sensor> sensores = db.Sensores.Where(s => s.Catalogos.Any(c => c.Id == Catalogo.Id)).ToList();
            BaseRepositorio<SensorFisico> repoSensorFisico = new BaseRepositorio<SensorFisico>(db);
            foreach (Sensor sensor in sensores) {
                SGEContext db2 = new SGEContext();
                SensorFisico sensorFisico = new SensorFisico() {
                    //TipoSensor = sensor,
                    //Dispositivo = inteligente,
                    IdDispositivo = inteligente.Id,
                    IdTipoSensor = sensor.Id,
                    Descripcion = sensor.Descripcion
                };
                sensorFisico.Mediciones = null;
                //repoSensorFisico.Create(sensorFisico);
                db2.SensoresFisicos.Add(sensorFisico);
                db2.SaveChanges();
            }

            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult CambiarEstado(int idInteligente, EstadoDispositivo estado) {
            SGEContext context = new SGEContext();

            BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>(context);
            Inteligente inteligente = repoInteligente.Single(i => i.Id == idInteligente);
            inteligente.Context = context;

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

            }
            base.Dispose(disposing);
        }
    }
}