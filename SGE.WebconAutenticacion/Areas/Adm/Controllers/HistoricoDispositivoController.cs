using MongoDB.Bson;
using MongoDB.Driver;
using SGE.Entidades.MongoDB.Modelo;
using SGE.WebconAutenticacion.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGE.WebconAutenticacion.Areas.Adm.Controllers
{
    public class HistoricoDispositivoController : Controller
    {
        private MongoDBContext dBContext;
        private IMongoCollection<ReporteDispositivo> reporteDispositivoColeccion;

        public HistoricoDispositivoController()
        {
            dBContext = new MongoDBContext();
            reporteDispositivoColeccion = dBContext.database.GetCollection<ReporteDispositivo>("dispositivoPorPeriodo");
        }
        // GET: Adm/HistoricoDispositivo
        public ActionResult Index()
        {
            List<ReporteDispositivo> historicosdispositivos = reporteDispositivoColeccion.AsQueryable<ReporteDispositivo>().ToList();
            return View(historicosdispositivos);
        }

        // GET: Adm/HistoricoDispositivo/Details/5
        public ActionResult Details(string id)
        {
            var historicoId = new ObjectId(id);
            var historicodispositivo = reporteDispositivoColeccion.AsQueryable<ReporteDispositivo>().SingleOrDefault(x => x.Id == historicoId);
            return View(historicodispositivo);
        }

///////////////////////////////////////////////////////////////////////////////////////////

        // GET: Adm/HistoricoDispositivo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Adm/HistoricoDispositivo/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Adm/HistoricoDispositivo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Adm/HistoricoDispositivo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Adm/HistoricoDispositivo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Adm/HistoricoDispositivo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
