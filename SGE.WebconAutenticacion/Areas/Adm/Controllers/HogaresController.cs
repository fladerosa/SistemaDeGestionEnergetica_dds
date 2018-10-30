using SGE.Entidades.Reportes;
using SGE.Entidades.Repositorio;
using SGE.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGE.WebconAutenticacion.Areas.Adm.Controllers
{
    public class HogaresController : Controller
    {
        // GET: Admin/Hogares
        public ActionResult Index()
        {
            BaseRepositorio<Cliente> repoCliente = new BaseRepositorio<Cliente>();
            ICollection<Cliente>clientes = repoCliente.GetAll();

            ViewBag.clientes = clientes.Select(c => new SelectListItem() {
                Text = c.NombreUsuario,
                Value = Reporte.consumoPorHogarYPeriodo(c.Id, DateTime.Now.AddYears(-10), DateTime.Now).ToString(),
            });

            return View();
        }
    }
}