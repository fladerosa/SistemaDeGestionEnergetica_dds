using MongoDB.Driver;
using SGE.WebconAutenticacion.App_Start;
using SGE.Entidades.MongoDB.Modelo;
using SGE.Entidades.Reportes;
using SGE.Entidades.Repositorio;
using SGE.Entidades.Transformadores;
using SGE.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MongoDB.Driver.Linq;

namespace SGE.WebconAutenticacion.Areas.Adm.Controllers
{
    public class ReportesController : Controller
    {    
        private MongoDBContext dBContext;
        private IMongoCollection<ReporteTransformador> reporteTransformadorColeccion;
        public ReporteTransformador reporteCreado { get; set; }

        private IMongoCollection<ReporteCliente> reporteClienteColeccion;
        public ReporteCliente reporteCliente { get; set; }

        private IMongoCollection<ReporteDispositivo> reporteDispositivoColeccion;
        public ReporteDispositivo reporteDispositivo { get; set; }

        public ReportesController() {

            dBContext = new MongoDBContext();
            reporteTransformadorColeccion = dBContext.database.GetCollection<ReporteTransformador>("transformadorPorPeriodo");
            reporteCreado = new ReporteTransformador();

            reporteClienteColeccion = dBContext.database.GetCollection<ReporteCliente>("clientePorPeriodo");
            reporteCliente = new ReporteCliente();

            reporteDispositivoColeccion = dBContext.database.GetCollection<ReporteDispositivo>("dispositivoPorPeriodo");
            reporteDispositivo = new ReporteDispositivo();
        }
        // GET: Admin/Reportes
        public ActionResult Index()
        {
            BaseRepositorio<Cliente> repoCliente = new BaseRepositorio<Cliente>();
            ICollection<Cliente> clientes = repoCliente.GetAll();
            ViewBag.clientes = clientes.Select(c => new SelectListItem() {
                Text = c.NombreUsuario,
                Value = c.Id.ToString(),
            });

            BaseRepositorio<Transformador> repoTransformador = new BaseRepositorio<Transformador>();
            ICollection<Transformador> transformadores = repoTransformador.GetAll();
            ViewBag.transformadores = transformadores.Select(t => new SelectListItem() {
                Text = t.codigo.ToString(),
                Value = t.Id.ToString(),
            });

            ViewBag.tiposDispositivos = new SelectList(new[]
                {
                    new { Value="inteligente", Text="Inteligente" },
                    new { Value="estandar", Text="Estandar" },
                }, "Value", "Text");

            return View();
        }

        [HttpPost]
        public JsonResult Consultar(string fechaDesde, string fechaHasta, string tipoReporte, string idObjeto) {
            DateTime fDesde = Convert.ToDateTime(fechaDesde);
            DateTime fHasta = DateTime.Now;

            if (!String.IsNullOrEmpty(fechaHasta)) {
                fHasta = Convert.ToDateTime(fechaHasta);
            }
            decimal consumo = 0;
            switch (tipoReporte.ToLower()) {
                case "hogar":
                    consumo = Reporte.consumoPorHogarYPeriodo(Convert.ToInt32(idObjeto), fDesde, fHasta);

                    dBContext = new MongoDBContext();
                    reporteClienteColeccion = dBContext.database.GetCollection<ReporteCliente>("clientePorPeriodo");

                    int busquedaHistoricoHogar = 0;
                    busquedaHistoricoHogar = (from t in reporteClienteColeccion.AsQueryable<ReporteCliente>()
                                              where t.IdUsuario == idObjeto
                                                      && t.FechaDesde == fDesde.ToShortDateString()
                                                      && (t.FechaHasta == fHasta.ToShortDateString() 
                                                      || t.FechaHasta == DateTime.Now.ToShortDateString())
                                                      && t.Consumo == consumo
                                              select t).Count();

                    if (busquedaHistoricoHogar == 0)
                    {
                        reporteCliente.IdUsuario = idObjeto;
                        reporteCliente.FechaDesde = fDesde.ToShortDateString();
                        reporteCliente.FechaHasta = fHasta.ToShortDateString();
                        reporteCliente.Consumo = consumo;

                        reporteClienteColeccion.InsertOne(reporteCliente);
                    }
                    break;
                case "tiposdisp":
                    consumo = Reporte.consumoPorTipoDeDispositivoPorPeriodo(idObjeto, fDesde, fHasta);

                    dBContext = new MongoDBContext();
                    reporteDispositivoColeccion = dBContext.database.GetCollection<ReporteDispositivo>("dispositivoPorPeriodo");

                    int busquedaHistoricoDispo = 0;
                    busquedaHistoricoDispo = (from t in reporteDispositivoColeccion.AsQueryable<ReporteDispositivo>()
                                where t.Tipo == idObjeto
                                        && t.FechaDesde == fDesde.ToShortDateString()
                                        && (t.FechaHasta == fHasta.ToShortDateString()
                                        || t.FechaHasta == DateTime.Now.ToShortDateString())
                                        && t.Consumo == consumo
                                select t).Count();

                    if (busquedaHistoricoDispo == 0)
                    {
                        reporteDispositivo.Tipo = idObjeto;
                        reporteDispositivo.FechaDesde = fDesde.ToShortDateString();
                        reporteDispositivo.FechaHasta = fHasta.ToShortDateString();
                        reporteDispositivo.Consumo = consumo;

                        reporteDispositivoColeccion.InsertOne(reporteDispositivo);
                    }
                    break;
                case "transformador":
                    consumo = Reporte.consumoTransformadorPorPeriodo(Convert.ToInt32(idObjeto), fDesde, fHasta);

                    dBContext = new MongoDBContext();
                    reporteTransformadorColeccion = dBContext.database.GetCollection<ReporteTransformador>("transformadorPorPeriodo");
                    int busquedaHistoricoTrans = 0;
                    busquedaHistoricoTrans = (from t in reporteTransformadorColeccion.AsQueryable<ReporteTransformador>()
                                    where  t.Codigo == Convert.ToInt32(idObjeto)
                                            && t.FechaDesde == fDesde.ToShortDateString()
                                            && (t.FechaHasta == fHasta.ToShortDateString()
                                            || t.FechaHasta == DateTime.Now.ToShortDateString())
                                            && t.Consumo == consumo
                                    select t).Count();

                    if (busquedaHistoricoTrans == 0)
                    {
                        reporteCreado.Codigo = Convert.ToInt32(idObjeto);
                        reporteCreado.FechaDesde = fDesde.ToShortDateString();
                        reporteCreado.FechaHasta = fHasta.ToShortDateString();
                        reporteCreado.Consumo = consumo;

                        reporteTransformadorColeccion.InsertOne(reporteCreado); //inserta documentos a la coleccion especificada
                    }
                   
                    break;
                default:
                    return Json(new { success = false, error = "No se reconoce el tipo de reporte" });
            }
            
            return Json(new { success = true, resultado = consumo, tipoReporte = tipoReporte });
        }
      
    }
}