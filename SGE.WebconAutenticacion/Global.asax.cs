using SGE.Entidades.Dispositivos;
using SGE.Entidades.Transformadores;
using SGE.Entidades.Zonas;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SGE.WebconAutenticacion {
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Zona zona = new Zona();
            zona.ProcesarDatosEnre();

            Transformador transformador = new Transformador();
            transformador.ProcesarDatosEnre();

            //Inteligente inteligente = new Inteligente();
            //inteligente.LevantarDispositivosArchivo();

          //  Catalogo catalogo = new Catalogo();
          //  catalogo.cargarCatalogoInicial();
        }
    }
}
