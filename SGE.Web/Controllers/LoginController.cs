using SGE.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGE.Web.Controllers
{
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
        public ActionResult Autorizacion(SGE.Web.Models.Usuario modeloUsuario)
        {
            using(SGEDbEntities db = new SGEDbEntities())
            {
                var detalleUsuario = db.Usuario.Where(x => x.NombreUsuario == modeloUsuario.NombreUsuario && x.Password == modeloUsuario.Password).FirstOrDefault();
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

        }
        public ActionResult LogOut()
        {
            int UsuarioId = (int)Session["Id"];
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}