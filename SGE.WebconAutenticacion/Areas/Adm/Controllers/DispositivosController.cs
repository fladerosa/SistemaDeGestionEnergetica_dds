using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SGE.Entidades.Contexto;
using SGE.Entidades.Dispositivos;

namespace SGE.WebconAutenticacion.Areas.Adm.Controllers
{
    public class DispositivosController : Controller
    {
        private SGEContext db = new SGEContext();

        // GET: Adm/Dispositivos
        public ActionResult Index()
        {
            var catalogo = db.Catalogos.Include(c => c.Administrador);
            return View(catalogo.ToList());
        }

        // GET: Adm/Dispositivos/Details/
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalogo catalogo = db.Catalogos.Find(id);
            if (catalogo == null)
            {
                return HttpNotFound();
            }
            return View(catalogo);
        }

        // GET: Adm/Dispositivos/Create
        public ActionResult Create()
        {
            ViewBag.AdministradorId = new SelectList(db.Usuarios, "Id", "Nombre");
            return View();
        }

        // POST: Adm/Dispositivos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,ConsumoEnergia,IdentificadorFabrica,AdministradorId")] Catalogo catalogo)
        {
            if (ModelState.IsValid)
            {
                db.Catalogos.Add(catalogo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdministradorId = new SelectList(db.Usuarios, "Id", "Nombre", catalogo.AdministradorId);
            return View(catalogo);
        }

        // GET: Adm/Dispositivos/Edit/
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalogo catalogo = db.Catalogos.Find(id);
            if (catalogo == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdministradorId = new SelectList(db.Usuarios, "Id", "Nombre", catalogo.AdministradorId);
            return View(catalogo);
        }

        // POST: Adm/Dispositivos/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,ConsumoEnergia,IdentificadorFabrica,AdministradorId")] Catalogo catalogo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(catalogo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdministradorId = new SelectList(db.Usuarios, "Id", "Nombre", catalogo.AdministradorId);
            return View(catalogo);
        }

        // GET: Adm/Dispositivos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalogo catalogo = db.Catalogos.Find(id);
            if (catalogo == null)
            {
                return HttpNotFound();
            }
            return View(catalogo);
        }

        // POST: Adm/Dispositivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Catalogo catalogo = db.Catalogos.Find(id);
            db.Catalogos.Remove(catalogo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
