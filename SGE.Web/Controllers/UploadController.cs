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
        /// Permite subir un archivo con los dispositivos y usuarios para deserializar los datos en memoria.
        /// </summary>
        [HttpPost]
        public ActionResult CargarArchivo()
        {
            try
            {
                if (Request.Files.Count > 0)
                {
                    var archivo = Request.Files[0];

                    string extensionArchivo = Path.GetExtension(archivo.FileName);

                    if (archivo != null && archivo.ContentLength > 0 && extensionArchivo == ".json")
                    {
                        string contenido = string.Empty;

                        using (var ms = new MemoryStream())
                        {
                            archivo.InputStream.CopyTo(ms);
                            ms.Position = 0;

                            contenido = new StreamReader(ms).ReadToEnd();
                        }

                        var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                        this.Usuarios = JsonConvert.DeserializeObject<List<Usuario>>(contenido, settings);

                        //object o = JsonConvert.DeserializeObject(json, new JsonSerializerSettings
                        //{
                        //    TypeNameHandling = TypeNameHandling.All,
                            
                        //    MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead
                        //});
                    }
                    else
                        LogHelper.LogErrorMessage("El archivo que intenta cargar el usuario está vacío o no es un archivo con extensión 'json'");
                }
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
    }
}