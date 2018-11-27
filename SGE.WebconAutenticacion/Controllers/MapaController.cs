using SGE.Entidades.Contexto;
using SGE.Entidades.Repositorio;
using SGE.Entidades.Transformadores;
using SGE.Entidades.Zonas;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SGE.Web.Controllers {
    public class MapaController : Controller {
        //Inicio
        public ActionResult Index() {
            var jsonSerialiser = new JavaScriptSerializer();
            BaseRepositorio<Zona> repoZona = new BaseRepositorio<Zona>();
            SGEContext db = new SGEContext();

            List<object> objetos = new List<object>();

            foreach (Transformador transformador in db.Transformadores.Include("Clientes").Include("Clientes.Inteligentes").ToList()) {
                var objeto = Json(new { transformador.Latitud, transformador.Longitud, Consumo = transformador.ObtenerConsumo() }).Data;

                objetos.Add(objeto);
            }

            ViewBag.transformadores = jsonSerialiser.Serialize(objetos);
            ViewBag.zonas = jsonSerialiser.Serialize(repoZona.GetAll());

            return View();
        }
    }
}