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
    public class HistoricoClienteController : Controller
    {
        private MongoDBContext dBContext;
        private IMongoCollection<ReporteCliente> reporteClienteColeccion;

        public HistoricoClienteController()
        {
            dBContext = new MongoDBContext();
            reporteClienteColeccion = dBContext.database.GetCollection<ReporteCliente>("clientePorPeriodo");
        }

        // GET: Adm/HistoricoCliente
        public ActionResult Index()
        {
            List<ReporteCliente> historicosclientes = reporteClienteColeccion.AsQueryable<ReporteCliente>().ToList();
            return View(historicosclientes);
        }

        // GET: Adm/HistoricoCliente/Details/
        public ActionResult Details(string id)
        {
            var historicoId = new ObjectId(id);
            var historicocliente = reporteClienteColeccion.AsQueryable<ReporteCliente>().SingleOrDefault(x => x.Id == historicoId);
            return View(historicocliente);
        }

///////////////////////////////////////////////////////////////////////////////////////////////

        // GET: Adm/HistoricoCliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Adm/HistoricoCliente/Create
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

        // GET: Adm/HistoricoCliente/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Adm/HistoricoCliente/Edit/5
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

        // GET: Adm/HistoricoCliente/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Adm/HistoricoCliente/Delete/5
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
