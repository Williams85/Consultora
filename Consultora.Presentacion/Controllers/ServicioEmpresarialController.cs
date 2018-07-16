using Consultora.Dominio;
using Consultora.Entidad;
using Consultora.Utilitario;
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
                Cod_Usuario = 1,
                Empleado = new EmpleadoEntidad
                {
                    Cod_Empleado = 1,
                    Nom_Empleado = "Williams",
                    AP_Empleado = "Morales",
                    AM_Empleado = "Caballero"
                },
                Nom_Usuario = "Williams Morales Caballero",
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
        public ActionResult AsignarRRHH(ServicioEmpresarialEntidad entidad)
        {
            ServicioEmpresarialCompetenciaDominio oServicioEmpresarialCompetenciaDominio = new ServicioEmpresarialCompetenciaDominio();
            entidad.Empleado = new EmpleadoEntidad
            {
                Cod_Empleado = SessionManager.Usuario.Empleado.Cod_Empleado,
            };
            var respuesta = oServicioEmpresarialCompetenciaDominio.GrabarAsignacionAutomatica(SessionManager.ListaConsultoresAsignados.Where(x => x.Consultor.Cod_Consultor != 0).ToList(), entidad);
            ServicioEmpresarialDominio oServicioEmpresarialDominio = new ServicioEmpresarialDominio();
            var objeto = oServicioEmpresarialDominio.FiltrarxCodigo(entidad.Cod_Servicio_Empresarial.ToString());
            SendEmail.NotificacionAsignacionConsultores(AppSettings.valueString("EmailGerenteOperaciones"), objeto);
            return Json(respuesta);
        }

        [HttpPost]
        public ActionResult BuscarConsultor(ConsultorEntidad entidad)
        {
            ConsultorDominio oConsultorDominio = new ConsultorDominio();
            SessionManager.ListaConsultoresFiltrar = oConsultorDominio.filtrar(entidad);

            if (SessionManager.ListaConsultoresAsignados != null && SessionManager.ListaConsultoresAsignados.Count > 0)
            {

                for (int i = 0; i <= SessionManager.ListaConsultoresFiltrar.Count - 1; i++)
                {
                    if (SessionManager.ListaConsultoresAsignados.Exists(x => x.Consultor.Cod_Consultor == SessionManager.ListaConsultoresFiltrar[i].Cod_Consultor))
                    {
                        SessionManager.ListaConsultoresFiltrar.RemoveAt(i);
                        i = i - 1;
                    }
                }
            }
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

        #region "Aprobacion de Asignacion Automatica"
        [HttpGet]
        public ActionResult AprobacionAsignacionAutomatica()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BuscarAsignacionesAutomatica()
        {
            ServicioEmpresarialDominio oServicioEmpresarialDominio = new ServicioEmpresarialDominio();
            var Lista = oServicioEmpresarialDominio.BuscarAsignaciones();

            return PartialView("_ResultadoAsignacionesAutomaticas", Lista);
        }

        [HttpPost]
        public ActionResult AprobacionAsignacionConsultores(ServicioEmpresarialEntidad entidad)
        {
            if (entidad == null) return RedirectToAction("AprobacionAsignacionAutomatica", "ServicioEmpresarial");
            ServicioEmpresarialDominio oServicioEmpresarialDominio = new ServicioEmpresarialDominio();
            ServicioEmpresarialCompetenciaDominio oServicioEmpresarialCompetenciaDominio = new ServicioEmpresarialCompetenciaDominio();
            var oServiciosEmpresarial = oServicioEmpresarialDominio.FiltrarxCodigo(entidad.Cod_Servicio_Empresarial.ToString());
            return View(oServiciosEmpresarial);
        }


        [HttpPost]
        public ActionResult BuscarConsultoresAsignados(string Codigo)
        {
            ServicioEmpresarialCompetenciaDominio oServicioEmpresarialCompetenciaDominio = new ServicioEmpresarialCompetenciaDominio();
            var Lista = oServicioEmpresarialCompetenciaDominio.BuscarConsultoresAsignados(Codigo);

            return PartialView("_ResultadoConsultoresAsignados", Lista);
        }

        [HttpPost]
        public ActionResult GrabarAprobacionAsignacionConsultores(string Codigo)
        {
            ServicioEmpresarialDominio oServicioEmpresarialDominio = new ServicioEmpresarialDominio();
            ServicioEmpresarialCompetenciaDominio oServicioEmpresarialCompetenciaDominio=new ServicioEmpresarialCompetenciaDominio();
            var respuesta = oServicioEmpresarialDominio.GrabarAprobacionAsignacionConsultores(Codigo);
            var servicio = oServicioEmpresarialDominio.FiltrarxCodigo(Codigo);
            var ListaAsignados = oServicioEmpresarialCompetenciaDominio.BuscarRRHHAsignados(Codigo);
            SendEmail.NotificacionAprobacionAsignacion(servicio, ListaAsignados);
            return Json(respuesta);
        }

        [HttpPost]
        public ActionResult GrabarRechazoAsignacionConsultores(ServicioEmpresarialEntidad entidad)
        {
            ServicioEmpresarialDominio oServicioEmpresarialDominio = new ServicioEmpresarialDominio();
            var respuesta = oServicioEmpresarialDominio.GrabarRechazoAsignacionConsultores(entidad.Cod_Servicio_Empresarial.ToString());
            return Json(respuesta);
        }

        #endregion


    }
}