using Consultora.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Consultora.Presentacion.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            SessionManager.Usuario = new UsuarioEntidad
            {
                Cod_Usuario=1,
                Nom_Usuario="Carlos Gonzales",
            };
            return RedirectToAction("Index", "ServicioEmpresarial");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}