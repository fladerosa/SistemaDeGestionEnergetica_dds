using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SGE.Entidades.Contexto;
using SGE.Entidades.Reportes;
using SGE.Entidades.Repositorio;
using SGE.Entidades.Sesion;
using SGE.Entidades.Usuarios;
using System;
using System.Web.Mvc;

namespace SGE.WebconAutenticacion.Areas.Cli.Controllers {
    public class ConsumoController : Controller
    {
        // GET: Cliente/Consumo
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Buscar(string fechaDesde, string fechaHasta) {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = UserManager.FindById(User.Identity.GetUserId());

            SGEContext contexto = new SGEContext();

            BaseRepositorio<Cliente> repoCliente = new BaseRepositorio<Cliente>(contexto);
            Cliente cliente = repoCliente.Single(c => c.NombreUsuario == user.UserName);

            DateTime fDesde = Convert.ToDateTime(fechaDesde);
            DateTime fHasta = DateTime.Now;

            if (!String.IsNullOrEmpty(fechaHasta)) {
                fHasta = Convert.ToDateTime(fechaHasta);
            }

            var consumo = Reporte.consumoPorHogarYPeriodo(cliente.Id, fDesde, fHasta);
            return Json(new { success = true, resultado = consumo });
        }
    }
}