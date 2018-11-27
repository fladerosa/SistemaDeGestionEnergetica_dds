using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SGE.Entidades.Acciones;
using SGE.Entidades.Contexto;
using SGE.Entidades.Dispositivos;
using SGE.Entidades.Reglas;
using SGE.Entidades.Repositorio;
using SGE.Entidades.Sensores;
using SGE.Entidades.Sesion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;

namespace SGE.WebconAutenticacion.Areas.Cli.Controllers {
    public class ReglasController : Controller
    {
        // GET: Cliente/Reglas
        public ActionResult Index()
        {
            ViewBag.reglas = ObtenerReglasActivas();

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = UserManager.FindById(User.Identity.GetUserId());

            BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>();
            ViewBag.tieneDispositivos = repoInteligente.Filter(i => i.Clientes.Any(c => c.NombreUsuario == user.UserName)).Count > 0;

            return View();
        }

        public ActionResult Agregar() {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = UserManager.FindById(User.Identity.GetUserId());

            BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>();
            ViewBag.inteligentes = repoInteligente.Filter(i => i.Clientes.Any(c => c.NombreUsuario == user.UserName));

            BaseRepositorio<Operador> repoOperador = new BaseRepositorio<Operador>();
            ViewBag.Operadores = new SelectList(repoOperador.GetAll(), "Id", "Descripcion");

            return View();
        }

        private ICollection<dynamic> ObtenerReglasActivas() {
            ICollection<dynamic> salida = new List<dynamic>();

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = UserManager.FindById(User.Identity.GetUserId());

            SGEContext contexto = new SGEContext();

            BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>(contexto);
            var includesInteligente = new List<Expression<Func<Inteligente, object>>>() {
                i => i.Reglas
            };

            var inteligentes = repoInteligente.Filter(i => i.Clientes.Any(c => c.NombreUsuario == user.UserName), includesInteligente);

            BaseRepositorio<Accion> repoAccion = new BaseRepositorio<Accion>(contexto);
            var includesAccion = new List<Expression<Func<Accion, object>>>() {
                a => a.Reglas
            };

            BaseRepositorio<Condicion> repoCondicion = new BaseRepositorio<Condicion>(contexto);
            foreach (Inteligente inteligente in inteligentes) {
                foreach (Regla regla in inteligente.Reglas) {
                    var reglaId = regla.ReglaId;
                    var condiciones = repoCondicion.Filter(c => c.ReglaId == reglaId);

                    if (condiciones.Count > 0) {
                        string strCondiciones = "";
                        foreach (Condicion condicion in condiciones) {
                            if (strCondiciones != "") strCondiciones += " | ";
                            string strTipoOperacion = condicion.Operador.Descripcion;
                            strCondiciones += condicion.ValorReferencia.ToString() + " " + strTipoOperacion;
                        }

                        string strAcciones = "";
                        foreach (Accion accion in regla.Acciones) {
                            if (strAcciones != "") strAcciones += " | ";
                            strAcciones += accion.Descripcion;
                        }


                        dynamic customRegla = new ExpandoObject();
                        customRegla.regla = regla.Nombre;
                        customRegla.condicion = "{" + strCondiciones + "} => {" + strAcciones + "}";

                        salida.Add(customRegla);
                    }
                }
            }

            return salida;
        }
    }
}