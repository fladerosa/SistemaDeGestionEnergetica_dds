using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SGE.Core.Helpers;
using SGE.Entidades;
using SGE.Servicios;

namespace SGE.Web.Controllers
{
    public class UploadController : Controller
    {
        #region Campos

        ServicioUsuarios servicioUsuarios;

        #endregion

        #region Propiedades

        /// <summary>
        /// Listado de cuentas procesadas hasta el momento
        /// </summary>
        public List<Usuario> Usuarios
        {
            get
            {
                if (Session["Usuarios"] == null)
                    Session["Usuarios"] = new List<Usuario>();

                return (List<Usuario>)Session["Usuarios"];
            }
            set
            {
                Session["Usuarios"] = value;
            }
        }

        /// <summary>
        /// Indica la cantidad de archivos subidos hasta el momento
        /// </summary>
        public int CantidadArchivos
        {
            get
            {
                if (Session["CantidadArchivos"] == null)
                    Session["CantidadArchivos"] = 0;

                return (int)Session["CantidadArchivos"];
            }
            set
            {
                Session["CantidadArchivos"] = value;
            }
        }

        #endregion

        #region Constructores

        public UploadController()
        {
            this.servicioUsuarios = new ServicioUsuarios();
        }

        #endregion

        #region Acciones

        // GET: Upload
        public ActionResult Index()
        {
            ViewBag.FilesQuantity = this.CantidadArchivos;
            return View(this.Usuarios);
        }

        /// <summary>
        /// Permite eliminar los datos de los dispositivos y los usuarios procesados hasta el momento.
        /// </summary>
        public ActionResult Restablecer()
        {
            this.Usuarios = null;

            this.CantidadArchivos = 0;

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Permite subir un archivo con los dispositivos y usuarios para deserializar los datos en memoria.
        /// </summary>
        [HttpPost]
        public ActionResult CargarArchivo()
        {
            try
            {
                if (Request.Files.Count > 0)
                    this.Usuarios = this.servicioUsuarios.ObtenerUsuarios(Request.Files[0].InputStream, Request.Files[0].FileName);
                else
                    LogHelper.LogErrorMessage("Se intentando realizar la carga sin especificar ningún archivo. Por favor, verifique que el usuario este cargando bien el archivo.");
            }
            catch(Exception ex)
            {
                LogHelper.LogErrorMessage(ex.Message, ex);
                ModelState.AddModelError("Error", ex);
            }
            
            return RedirectToAction("Index");
        }

        #endregion
    }
}