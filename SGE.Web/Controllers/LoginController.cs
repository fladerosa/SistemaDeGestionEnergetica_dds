using SGE.Entidades.Contexto;
using SGE.Entidades.Usuarios;
using System.Linq;
using System.Web.Mvc;

namespace SGE.Web.Controllers {
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        //Login Ok
        public ActionResult LoginOk()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autorizado(Usuario modeloUsuario)
        {
            var detalleUsuario = SGEContext.instancia().Usuarios.Where(x => x.NombreUsuario == modeloUsuario.NombreUsuario && x.Password == modeloUsuario.Password).FirstOrDefault();
            if(detalleUsuario == null)
            {
                modeloUsuario.MensajeDeErrorLogueo = "Usuario y/o contraseña erronea.";
                return View("Index", modeloUsuario);
            }
            else
            {
                Session["Id"] = detalleUsuario.Id;
                Session["NombreUsuario"] = detalleUsuario.NombreUsuario;
                return RedirectToAction("LoginOk", "Login");
            }
        }
        public ActionResult LogOut()
        {
            int UsuarioId = (int)Session["Id"];
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}