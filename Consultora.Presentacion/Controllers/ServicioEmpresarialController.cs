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
            ClienteDominio oClienteDominio = new ClienteDominio();
            ServicioDominio oServicioDominio = new ServicioDominio();
            SessionManager.Usuario = new UsuarioEntidad
            {
                Nom_Usuario = "Williams Morales Caballero"
            };
            var ListaServiciosEmpresarial = oServicioEmpresarialDominio.listarActivos();
            var ListaClientes = oClienteDominio.listarActivos();
            var ListaServicios = oServicioDominio.listarActivos();
            ViewBag.ListaServiciosEmpresarial = ListaServiciosEmpresarial;
            ViewBag.ListaClientes = ListaClientes;
            ViewBag.ListaServicios = ListaServicios;
            return View();
        }

        [HttpPost]
        public ActionResult AsignacionRRHH(ServicioEmpresarialEntidad entidad)
        {
            if (entidad == null) return RedirectToAction("Index", "ServicioEmpresarial");
            ServicioEmpresarialDominio oServicioEmpresarialDominio = new ServicioEmpresarialDominio();
            ServicioEmpresarialCompetenciaDominio oServicioEmpresarialCompetenciaDominio = new ServicioEmpresarialCompetenciaDominio();
            var oServiciosEmpresarial = oServicioEmpresarialDominio.FiltrarxCodigo(entidad.Cod_Servicio_Empresarial.ToString());
            SessionManager.ListaConsultoresAsignados = oServicioEmpresarialCompetenciaDominio.BuscarRRHHAsignados(entidad.Cod_Servicio_Empresarial.ToString());
            ViewBag.ExisteConsultoresAsignados = 0;
            ViewBag.MaximoConsultores = SessionManager.ListaConsultoresAsignados.Count;
            if (SessionManager.ListaConsultoresAsignados.Exists(x => x.Consultor.Cod_Consultor != 0))
            {
                if (SessionManager.ListaConsultoresAsignados.Exists(x => x.Consultor.Cod_Consultor == 0))
                {
                    ViewBag.ExisteConsultoresAsignados = 1;
                    //SessionManager.ListaConsultoresAsignados = SessionManager.ListaConsultoresAsignados.Where(x => x.Consultor.Cod_Consultor != 0).ToList();
                }

                else
                    ViewBag.ExisteConsultoresAsignados = 2;
            }
            else
            {
                SessionManager.ListaConsultoresAsignados = null;
            }
            return View(oServiciosEmpresarial);
        }

        [HttpPost]
        public ActionResult BuscarRRHH(string Codigo)
        {
            ServicioEmpresarialCompetenciaDominio oServicioEmpresarialCompetenciaDominio = new ServicioEmpresarialCompetenciaDominio();
            if (SessionManager.ListaConsultoresAsignados == null)
                SessionManager.ListaConsultoresAsignados = oServicioEmpresarialCompetenciaDominio.BuscarRRHH(Codigo);

            return PartialView("_ResultadoBusqueda", SessionManager.ListaConsultoresAsignados.Where(x => x.Consultor.Cod_Consultor != 0).ToList());
        }

        [HttpPost]
        public ActionResult SearchRRHH(string Codigo)
        {
            ServicioEmpresarialCompetenciaDominio oServicioEmpresarialCompetenciaDominio = new ServicioEmpresarialCompetenciaDominio();
            SessionManager.ListaConsultoresAsignados = oServicioEmpresarialCompetenciaDominio.BuscarRRHH(Codigo);

            return PartialView("_ResultadoBusqueda", SessionManager.ListaConsultoresAsignados.Where(x => x.Consultor.Cod_Consultor != 0).ToList());
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

        [HttpPost]
        public ActionResult AsignarRRHH()
        {
            ServicioEmpresarialCompetenciaDominio oServicioEmpresarialCompetenciaDominio = new ServicioEmpresarialCompetenciaDominio();
            var respuesta = oServicioEmpresarialCompetenciaDominio.AsignarRRHH(SessionManager.ListaConsultoresAsignados.Where(x => x.Consultor.Cod_Consultor != 0).ToList());
            return Json(respuesta);
        }

        [HttpPost]
        public ActionResult BuscarConsultor(ConsultorEntidad entidad)
        {
            ConsultorDominio oConsultorDominio = new ConsultorDominio();
            SessionManager.ListaConsultoresFiltrar = oConsultorDominio.filtrar(entidad);
            return PartialView("_ResultadoFiltroConsultoresModal", SessionManager.ListaConsultoresFiltrar);
        }

        [HttpPost]
        public ActionResult BuscarConsultorxCodigo(string Codigo)
        {
            ConsultorDominio oConsultorDominio = new ConsultorDominio();
            var DetalleConsultor = oConsultorDominio.filtrarxCodigo(Codigo);
            return PartialView("_ResultadoDetalleConsultorModal", DetalleConsultor);
        }


        [HttpPost]
        public ActionResult AgregarConsultor(ConsultorEntidad entidad)
        {
            bool estado = false;
            for (int i = 0; i <= SessionManager.ListaConsultoresAsignados.Count - 1; i++)
            {
                if (SessionManager.ListaConsultoresAsignados[i].Consultor.Cod_Consultor == 0)
                {
                    var consultor = SessionManager.ListaConsultoresFiltrar.Where(x => x.Cod_Consultor == entidad.Cod_Consultor).ToList()[0];
                    SessionManager.ListaConsultoresAsignados[i].Consultor = consultor;
                    SessionManager.ListaConsultoresAsignados[i].Consultor.Disponible = true;
                    estado = true;
                    break;
                }
            }
            return Json(estado);
        }

        [HttpPost]
        public ActionResult EliminarConsultor(string Codigo)
        {
            bool estado = false;
            for (int i = 0; i <= SessionManager.ListaConsultoresAsignados.Count - 1; i++)
            {
                if (SessionManager.ListaConsultoresAsignados[i].Cod_Servicio_Empresarial_Competencias == int.Parse(Codigo))
                {
                    SessionManager.ListaConsultoresAsignados[i].Consultor.Cod_Consultor = 0;
                    SessionManager.ListaConsultoresAsignados[i].Consultor.Disponible = true;
                    estado = true;
                    break;
                }
            }
            return Json(estado);
        }


    }
}