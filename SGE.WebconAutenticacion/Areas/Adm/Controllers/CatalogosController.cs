using SGE.Entidades.Acciones;
using SGE.Entidades.Contexto;
using SGE.Entidades.Dispositivos;
using SGE.Entidades.Sensores;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SGE.WebconAutenticacion.Areas.Adm.Controllers {
    public class CatalogosController : Controller {
        private SGEContext db = new SGEContext();

        // GET: Adm/Dispositivos
        public ActionResult Index() {
            var catalogo = db.Catalogos.Include(c => c.Administrador);
            return View(catalogo.ToList());
        }

        // GET: Adm/Dispositivos/Details/
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalogo catalogo = db.Catalogos.Include("Acciones").Include("Sensores").FirstOrDefault(c => c.Id == id);
            if (catalogo == null) {
                return HttpNotFound();
            }
            return View(catalogo);
        }

        // GET: Adm/Dispositivos/Create
        public ActionResult Create() {
            ViewBag.AdministradorId = new SelectList(db.Usuarios, "Id", "Nombre");
            ViewBag.Acciones = new MultiSelectList(db.Acciones, "Id", "Descripcion");
            ViewBag.Sensores = new MultiSelectList(db.Sensores, "Id", "Descripcion");
            return View();
        }

        // POST: Adm/Dispositivos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Catalogo catalogo, int[] sensoresIds, int[] accionesIds) {
            catalogo.Sensores = setearSensores(sensoresIds);
            catalogo.Acciones = setearAcciones(accionesIds);

            if (ModelState.IsValid) {
                db.Catalogos.Add(catalogo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdministradorId = new SelectList(db.Usuarios, "Id", "Nombre", catalogo.AdministradorId);
            return View(catalogo);
        }

        // GET: Adm/Dispositivos/Edit/
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalogo catalogo = db.Catalogos.Find(id);
            if (catalogo == null) {
                return HttpNotFound();
            }
            ViewBag.AdministradorId = new SelectList(db.Usuarios, "Id", "Nombre", catalogo.AdministradorId);
            ViewBag.Acciones = new MultiSelectList(db.Acciones, "Id", "Descripcion", db.Acciones.Where(a => a.Catalogos.Any(c => c.Id == id)).Select(a => a.Id).ToArray());
            ViewBag.Sensores = new MultiSelectList(db.Sensores, "Id", "Descripcion", db.Sensores.Where(s => s.Catalogos.Any(c => c.Id == id)).Select(s => s.Id).ToArray());
            return View(catalogo);
        }

        // POST: Adm/Dispositivos/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Catalogo catalogo, int[] sensoresIds, int[] accionesIds) {
            catalogo.Sensores = setearSensores(sensoresIds);
            catalogo.Acciones = setearAcciones(accionesIds);
            if (ModelState.IsValid) {
                Catalogo catalogoBase = db.Catalogos.Include("Acciones").Include("Sensores").First(c => c.Id == catalogo.Id);

                catalogoBase.Sensores = catalogo.Sensores;
                catalogoBase.Acciones = catalogo.Acciones;

                db.Entry(catalogoBase).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.AdministradorId = new SelectList(db.Usuarios, "Id", "Nombre", catalogo.AdministradorId);
            ViewBag.Acciones = new MultiSelectList(db.Acciones, "Id", "Descripcion", catalogo.Acciones.Select(a => a.Id).ToArray());
            ViewBag.Sensores = new MultiSelectList(db.Sensores, "Id", "Descripcion", catalogo.Sensores.Select(s => s.Id).ToArray());
            return View(catalogo);
        }

        // GET: Adm/Dispositivos/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalogo catalogo = db.Catalogos.Find(id);
            if (catalogo == null) {
                return HttpNotFound();
            }
            return View(catalogo);
        }

        // POST: Adm/Dispositivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Catalogo catalogo = db.Catalogos.Find(id);
            db.Catalogos.Remove(catalogo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private List<Sensor> setearSensores(int[] sensoresIds) {
            List<Sensor> sensores = new List<Sensor>();
            Sensor sensor = null;

            foreach(int sensorId in sensoresIds) {
                sensor = db.Sensores.FirstOrDefault(s => s.Id == sensorId);
                if(sensor != null) {
                    sensores.Add(sensor);
                }
            }

            return sensores;
        }

        private List<Accion> setearAcciones(int[] accionesIds) {
            List<Accion> acciones = new List<Accion>();
            Accion accion = null;

            foreach (int accionId in accionesIds) {
                accion = db.Acciones.FirstOrDefault(a => a.Id == accionId);
                if(accion != null) {
                    acciones.Add(accion);
                }
            }

            return acciones;
        }
    }
}
