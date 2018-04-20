using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGE.Core.Helpers;
using SGE.Entidades;

namespace SGE.Web.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Process()
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
    }
}