using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGE.Web.Controllers
{
    public class MapaController : Controller
    {
        //Inicio
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public FilePathResult ZonasJson()
        {
            return File(Server.MapPath("../Resources/zonas.json"), "text/x-json");
        }


        public FilePathResult TransformadoresJson()
        {
            return File(Server.MapPath("../Resources/transformadores.json"), "text/x-json");
        }
    }
}