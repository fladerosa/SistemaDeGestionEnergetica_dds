using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SGE.Core.Helpers;
using SGE.Entidades;

namespace SGE.Web.Controllers
{
    public class UploadController : Controller
    {
        #region Propiedades

        /// <summary>
        /// Listado de cuentas procesadas hasta el momento
        /// </summary>
        public List<Dispositivo> Dispositivos
        {
            get
            {
                if (Session["Dispositivos"] == null)
                    Session["Dispositivos"] = new List<Dispositivo>();

                return (List<Dispositivo>)Session["Dispositivos"];
            }
            set
            {
                Session["Dispositivos"] = value;
            }
        }

        /// <summary>
        /// Indica la cantidad de archivos subidos hasta el momento
        /// </summary>
        public int FilesQuantity
        {
            get
            {
                if (Session["FilesQuantity"] == null)
                    Session["FilesQuantity"] = 0;

                return (int)Session["FilesQuantity"];
            }
            set
            {
                Session["FilesQuantity"] = value;
            }
        }

        #endregion

        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Permite eliminar los datos de los dispositivos y los usuarios procesados hasta el momento.
        /// </summary>
        public ActionResult Limpiar()
        {
            this.Dispositivos = null;

            this.FilesQuantity = 0;

            DirectoryInfo di = new DirectoryInfo(ConfiguracionHelper.AccountFilesPath);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Procesar()
        {
            try
            {
                List<Dispositivo> accountList = new List<Dispositivo>();
                LogHelper.LogInformationMessage("Prueba");
            }
            catch (Exception ex)
            {
                LogHelper.LogErrorMessage(ex.Message, ex);
                ModelState.AddModelError("Error", ex);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Permite subir un archivo de cuentas a la carpeta destinada a los mismos.
        /// </summary>
        [HttpPost]
        public ActionResult CargarArchivo()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0 && file.FileName.EndsWith(".json"))
                {
                    CargarArchivo(file);
                }
            }
            
            return RedirectToAction("Index");
        }

        public void CargarArchivo(HttpPostedFileBase file)
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);
            string fileExtension = Path.GetExtension(file.FileName);
            var fileName = string.Format("{0}{1}{2}", fileNameWithoutExtension, this.FilesQuantity, fileExtension);
            var path = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
            file.SaveAs(path);
            this.FilesQuantity++;
        }
    }
}