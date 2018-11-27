using SGE.Entidades.Dispositivos;
using SGE.Entidades.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace SGE.WebconAutenticacion.Areas.Adm.Controllers {
    public class DispositivosController : Controller {

        public ActionResult Index() {
            BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>();
            var includesInteligente = new List<Expression<Func<Inteligente, object>>>() {
                i => i.RegistroDeActivaciones,
                i => i.Clientes
            };

            ViewBag.dispositivosEstado = repoInteligente.GetAll(includesInteligente); ;
            return View();
        }
    }
}
