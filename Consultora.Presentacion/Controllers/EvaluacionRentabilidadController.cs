using Consultora.Dominio;
using Consultora.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Consultora.Presentacion.Controllers
{
    public class EvaluacionRentabilidadController : Controller
    {
        //
        // GET: /EvaluacionRentabilidad/
        public ActionResult Index(int id)
        {
            ViewBag.Cod_Iniciativa = id;
            SessionManager.IniciativaEntidad = new IniciativaEntidad();
            return View();
        }

        [HttpPost]
        public ActionResult CalcularCostoEquipo(string Codigo)
        {
            IniciativaDominio oIniciativaDominio = new IniciativaDominio();
            var valor = oIniciativaDominio.CalcularCostoEquipo(Codigo);
            SessionManager.IniciativaEntidad.CostoTotalEquipo = valor;
            return PartialView("_ViewCostoTotalEquipo", SessionManager.IniciativaEntidad);
        }

        [HttpPost]
        public ActionResult CalcularCostoServicio(string Codigo)
        {
            IniciativaDominio oIniciativaDominio = new IniciativaDominio();
            var valor = oIniciativaDominio.CalcularCostoServicio(Codigo);
            SessionManager.IniciativaEntidad.CostoTotalServicio = valor;
            return PartialView("_ViewCostoTotalServicio", SessionManager.IniciativaEntidad);
        }

        [HttpPost]
        public ActionResult CalcularCostoEquipoCliente(string Codigo)
        {
            IniciativaDominio oIniciativaDominio = new IniciativaDominio();
            var valor = oIniciativaDominio.CalcularCostoEquipoCliente(Codigo);
            SessionManager.IniciativaEntidad.CostoTotalCliente = valor;
            return PartialView("_ViewCostoTotalCliente", SessionManager.IniciativaEntidad);
        }

        [HttpPost]
        public ActionResult CalcularTamañoServicio(string Codigo)
        {
            IniciativaDominio oIniciativaDominio = new IniciativaDominio();
            var valor = oIniciativaDominio.CalcularTamañoServicio(Codigo);
            SessionManager.IniciativaEntidad.MedidadServicio = valor;
            return PartialView("_ViewTamañoServicio", SessionManager.IniciativaEntidad);
        }

        [HttpPost]
        public ActionResult RegistrarEvaluacionRentabilidad(IniciativaEntidad entidad)
        {

            IniciativaDominio oIniciativaDominio = new IniciativaDominio();
            var respuesta = oIniciativaDominio.RegistrarEvaluacionRentabilidad(entidad);
            return Json(respuesta);
        }


	}
}