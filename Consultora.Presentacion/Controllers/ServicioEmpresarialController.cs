using Consultora.Dominio;
using Consultora.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Consultora.Presentacion.Controllers
{
    public class ServicioEmpresarialController : Controller
    {
        // GET: ServicioEmpresarial
        public ActionResult Index()
        {
            ServicioEmpresarialDominio oServicioEmpresarialDominio = new ServicioEmpresarialDominio();
            var ListaServiciosEmpresarial = oServicioEmpresarialDominio.listarActivos();
            ViewBag.ListaServiciosEmpresarial = ListaServiciosEmpresarial;
            ClienteDominio oClienteDominio = new ClienteDominio();
            ServicioDominio oServicioDominio = new ServicioDominio();
            var ListaClientes = oClienteDominio.listarActivos();
            var ListaServicios = oServicioDominio.listarActivos();
            ViewBag.ListaClientes = ListaClientes;
            ViewBag.ListaServicios = ListaServicios;
            return View();
        }

        [HttpPost]
        public ActionResult AsignacionRRHH(ServicioEmpresarialEntidad entidad)
        {
            if (entidad == null) return RedirectToAction("Index", "ServicioEmpresarial");
            ServicioEmpresarialDominio oServicioEmpresarialDominio = new ServicioEmpresarialDominio();
            var oServiciosEmpresarial = oServicioEmpresarialDominio.FiltrarxCodigo(entidad.Cod_Servicio_Empresarial.ToString());
            return View(oServiciosEmpresarial);
        }

        [HttpPost]
        public ActionResult BuscarRRHH(string Codigo)
        {
            ServicioEmpresarialCompetenciaDominio oServicioEmpresarialCompetenciaDominio = new ServicioEmpresarialCompetenciaDominio();
            SessionManager.ListaConsultoresAsignados = oServicioEmpresarialCompetenciaDominio.BuscarRRHH(Codigo);
            return PartialView("_ResultadoBusqueda", SessionManager.ListaConsultoresAsignados);
        }

        [HttpPost]
        public ActionResult BuscarServiciosEmpresarial(ServicioEmpresarialEntidad entidad)
        {
            ServicioEmpresarialDominio oServicioEmpresarialDominio = new ServicioEmpresarialDominio();
            var ListaServiciosEmpresarial = oServicioEmpresarialDominio.Filtrar(entidad);
            return PartialView("_ResultadoBusquedaServEmpresarial", ListaServiciosEmpresarial);
        }

        [HttpPost]
        public ActionResult ListarRequerimientos(string Codigo)
        {
            ServicioEmpresarialCompetenciaDominio oServicioEmpresarialCompetenciaDominio = new ServicioEmpresarialCompetenciaDominio();
            var ListaConsultoresCompetencias = oServicioEmpresarialCompetenciaDominio.ListarRequerimientos(Codigo);
            return PartialView("_ResultadoListaRequerimientosModal", ListaConsultoresCompetencias);
        }
        

    }
}