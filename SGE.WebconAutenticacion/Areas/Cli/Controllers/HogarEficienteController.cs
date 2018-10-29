using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SGE.Entidades.Contexto;
using SGE.Entidades.Repositorio;
using SGE.Entidades.Sesion;
using SGE.Entidades.Usuarios;
using System.Web.Mvc;

namespace SGE.WebconAutenticacion.Areas.Cli.Controllers {
    public class HogarEficienteController : Controller
    {
        // GET: Cli/HogarEficiente
        public ActionResult Index()
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = UserManager.FindById(User.Identity.GetUserId());

            SGEContext contexto = new SGEContext();

            BaseRepositorio<Cliente> repoCliente = new BaseRepositorio<Cliente>(contexto);
            Cliente cliente = repoCliente.Single(c => c.NombreUsuario == user.UserName);

            ViewBag.hogarEficiente = cliente.HogarEficiente();

            return View();
        }
    }
}
