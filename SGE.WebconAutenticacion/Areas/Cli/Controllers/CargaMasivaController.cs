using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using SGE.Entidades.Contexto;
using SGE.Entidades.Dispositivos;
using SGE.Entidades.Repositorio;
using SGE.Entidades.Sesion;
using SGE.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace SGE.WebconAutenticacion.Areas.Cli.Controllers {
    public class CargaMasivaController : Controller {
        // GET: Cli/CargaMasiva
        public ActionResult Index() {
            return View();
        }

        // GET: Cli/CargaMasiva/Details/5
        public JsonResult SubirArchivo(HttpPostedFileBase File) {
            if (!File.ContentType.Contains("json")) {
                return Json(new { success = false, error = "El archivo debe ser de formato JSON" });
            }

            try {
                using (StreamReader r = new StreamReader(File.InputStream)) {
                    string json = r.ReadToEnd();
                    List<Inteligente> inteligentes = JsonConvert.DeserializeObject<List<Inteligente>>(json);

                    var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                    var user = UserManager.FindById(User.Identity.GetUserId());

                    SGEContext contexto = new SGEContext();

                    BaseRepositorio<Cliente> repoCliente = new BaseRepositorio<Cliente>(contexto);
                    Cliente cliente = repoCliente.Single(c => c.NombreUsuario == user.UserName);

                    BaseRepositorio<Catalogo> repoCatalogo = new BaseRepositorio<Catalogo>(contexto);
                    BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>(contexto);

                    foreach (Inteligente inteligente in inteligentes) {
                        Catalogo Catalogo = repoCatalogo.Single(c => c.Id == inteligente.CatalogoId);

                        if(Catalogo == null) {
                            return Json(new { success = false, error = "El dispositivo '" + inteligente.Nombre + "' esta asociado a un catálogo inexistente" });
                        }
                        string nombreInteligente = Catalogo.Nombre + "_" + DateTime.Now.ToString("ddMMyyHHmmss");
                        nombreInteligente = nombreInteligente.Replace(" ", "_");
                        inteligente.Nombre = nombreInteligente;
                        inteligente.Clientes.Clear();
                        inteligente.Clientes.Add(cliente);

                        if (inteligente.Id != 0) {
                            repoInteligente.Update(inteligente);
                        } else {
                            repoInteligente.Create(inteligente);
                        }
                    }
                }
            } catch (Exception ex) {
                return Json(new { success = false, error = "El archivo JSON no es valido, por favor verifique el mismo", mensaje = ex.Message });
            }

            return Json(new { success = true });
        }
    }
}
