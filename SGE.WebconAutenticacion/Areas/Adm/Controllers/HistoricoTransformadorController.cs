using MongoDB.Driver;
using SGE.WebconAutenticacion.App_Start;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SGE.Entidades.MongoDB.Modelo;
using MongoDB.Bson;

namespace SGE.WebconAutenticacion.Areas.Adm.Controllers
{
    public class HistoricoTransformadorController : Controller
    {
        private MongoDBContext dBContext;
        private IMongoCollection<ReporteTransformador> reporteTransformadorColeccion;

        public HistoricoTransformadorController()
        {
            dBContext = new MongoDBContext();
            reporteTransformadorColeccion = dBContext.database.GetCollection<ReporteTransformador>("transformadorPorPeriodo");
        }

        // GET: Adm/HistoricoTransformador
        public ActionResult Index()
        {
            List<ReporteTransformador> historicostransformadores = reporteTransformadorColeccion.AsQueryable<ReporteTransformador>().ToList();
            return View(historicostransformadores);          
        }

        // GET: Adm/HistoricoTransformador/Details/
        public ActionResult Details(string id)
        {
            var historicoId = new ObjectId(id);
            var historicotransformador = reporteTransformadorColeccion.AsQueryable<ReporteTransformador>().SingleOrDefault(x => x.Id == historicoId);
            return View(historicotransformador);           
        }

///////////////////////////////////////////////////////////////////////////////////////////

        // GET: HistoricoTransformador/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HistoricoTransformador/Create
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

        // GET: HistoricoTransformador/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HistoricoTransformador/Edit/5
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

        // GET: HistoricoTransformador/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HistoricoTransformador/Delete/5
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
