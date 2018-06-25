using Consultora.Dominio;
using Consultora.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Consultora.Utilitario;
using System.IO;
namespace Consultora.Presentacion.Controllers
{
    public class IniciativaController : Controller
    {

        private IniciativaDominio oIniciativaDominio = new IniciativaDominio();

        #region "Nueva Oportunidad"

        [HttpGet]
        public ActionResult Index()
        {
            NegocioDominio oNegocioDominio = new NegocioDominio();
            ServicioDominio oServicioDominio = new ServicioDominio();
            ClienteDominio oClienteDominio = new ClienteDominio();

            ViewBag.ListaNegocio = oNegocioDominio.listarActivos();
            ViewBag.ListaTipoServicio = oServicioDominio.listarActivos();
            ViewBag.ListaCliente = oClienteDominio.listarActivos();
            ViewBag.FechaRegistro = DateTime.Now.ToString("dd/MM/yyyy");
            return View();
        }


        [HttpPost]
        public ActionResult GrabarOportunidad()
        {
            int Cod_Iniciativa = 0;
            ResponseWeb<string> oResponseWeb = new ResponseWeb<string>() { Estado = false, };
            #region "Validar Archivo RFP"
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase RFP = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    RFP = Request.Files[i];
                    if (string.IsNullOrWhiteSpace(RFP.FileName))
                    {
                        oResponseWeb.Message = "No se cargo el documento RFP";
                        oResponseWeb.Valor = "0";
                        return Json(oResponseWeb);
                    }
                }
                #region "Grabar Iniciativa"

                IniciativaEntidad entidad = new IniciativaEntidad()
                {
                    Cod_Iniciativa = int.Parse(Request.Form["Cod_Iniciativa"].ToString()),
                    Nom_Iniciativa = Request.Form["Nom_Iniciativa"].ToString(),
                    Des_Iniciativa = Request.Form["Des_Iniciativa"].ToString(),
                    Negocio = new NegocioEntidad
                    {
                        Cod_Negocio = int.Parse(Request.Form["Cod_Negocio"].ToString()),
                    },
                    Servicio = new ServicioEntidad
                    {
                        Cod_Servicio = int.Parse(Request.Form["Cod_Servicio"].ToString()),
                    },
                    Cliente = new ClienteEntidad
                    {
                        Cod_Cliente = int.Parse(Request.Form["Cod_Cliente"].ToString()),
                    },
                    Estado_Iniciativa = (int)Enumeracion.EstadoFlujo.NuevaOportunidad,
                    Fecha_Registro = DateTime.Now,
                };
                string fileRFP = entidad.Nom_Iniciativa.Replace(" ", "").ToLower() + Path.GetExtension(RFP.FileName).ToLower();
                entidad.RFP = "/RFP/" + fileRFP;
                string pathRFP = Server.MapPath("~") + entidad.RFP;

                if (oIniciativaDominio.Grabar(entidad, ref Cod_Iniciativa))
                {
                    oResponseWeb.Estado = true;
                    oResponseWeb.Message = "Los cambios fueron guardados";
                    var entity = oIniciativaDominio.ObtenerxCodigo(Cod_Iniciativa.ToString());
                    oResponseWeb.Valor = Cod_Iniciativa.ToString() + "|" + entidad.RFP;
                    RFP.SaveAs(pathRFP);
                }
                else
                {
                    oResponseWeb.Estado = false;
                    oResponseWeb.Message = "Los cambios no fueron guardados";
                }
                #endregion
                return Json(oResponseWeb);
            }
            else
            {
                oResponseWeb.Message = "No se cargo el documento RFP";
                return Json(oResponseWeb);
            }
            #endregion
        }

        #endregion

        #region "Consultar Oportunidad"
        public ActionResult ConsultarIniciativa()
        {
            NegocioDominio oNegocioDominio = new NegocioDominio();
            ServicioDominio oServicioDominio = new ServicioDominio();
            ClienteDominio oClienteDominio = new ClienteDominio();

            ViewBag.ListaNegocio = oNegocioDominio.listarActivos();
            ViewBag.ListaTipoServicio = oServicioDominio.listarActivos();
            ViewBag.ListaCliente = oClienteDominio.listarActivos();
            return View();
        }


        [HttpPost]
        public ActionResult BuscarIniciativa(IniciativaEntidad entidad)
        {
            IniciativaDominio oIniciativaDominio = new IniciativaDominio();
            var Lista = oIniciativaDominio.filtrar(entidad);
            return PartialView("_ResultadoIniciativa", Lista);
        }
        #endregion

        #region "Asignacion de Responsable de Servicio"

        [HttpGet]
        public ActionResult AsignarResposnableServicio(int id)
        {
            if (id != null && id > 0)
            {


                NegocioDominio oNegocioDominio = new NegocioDominio();
                ServicioDominio oServicioDominio = new ServicioDominio();
                ClienteDominio oClienteDominio = new ClienteDominio();
                UsuarioDominio oUsuarioDominio = new UsuarioDominio();
                var iniciativa = oIniciativaDominio.ObtenerxCodigo(id.ToString());
                ViewBag.ListaNegocio = oNegocioDominio.listarActivos();
                ViewBag.ListaTipoServicio = oServicioDominio.listarActivos();
                ViewBag.ListaCliente = oClienteDominio.listarActivos();
                ViewBag.ListaResponsableServicio = oUsuarioDominio.ListarxPerfil(new UsuarioEntidad
                {
                    Perfil = new PerfilEntidad
                    {
                        Cod_Perfil = (int)Enumeracion.PerfilUsuario.ResponsableServicio,
                    }
                });
                return View(iniciativa);
            }
            else
            {
                return RedirectToAction("Index", "Iniciativa");
            }

        }

        [HttpPost]
        public ActionResult GrabarResponsableServicio()
        {
            ResponseWeb<string> oResponseWeb = new ResponseWeb<string>() { Estado = false, };
            #region "Modificar Iniciativa"
            HttpPostedFileBase RFP = null;
            string pathRFP = "";
            IniciativaDominio oIniciativaDominio = new IniciativaDominio();
            IniciativaEntidad entidad = new IniciativaEntidad()
            {
                Cod_Iniciativa = int.Parse(Request.Form["Cod_Iniciativa"].ToString()),
                Nom_Iniciativa = Request.Form["Nom_Iniciativa"].ToString(),
                Des_Iniciativa = Request.Form["Des_Iniciativa"].ToString(),
                Negocio = new NegocioEntidad
                {
                    Cod_Negocio = int.Parse(Request.Form["Negocio.Cod_Negocio"].ToString()),
                },
                Servicio = new ServicioEntidad
                {
                    Cod_Servicio = int.Parse(Request.Form["Servicio.Cod_Servicio"].ToString()),
                },
                Cliente = new ClienteEntidad
                {
                    Cod_Cliente = int.Parse(Request.Form["Cliente.Cod_Cliente"].ToString()),
                },
                RFP = Request.Form["RFP"].ToString(),
                Estado_Iniciativa = (int)Enumeracion.EstadoFlujo.AsignarResponsableServicio,
                ResponsableServicio = new UsuarioEntidad
                {
                    Cod_Usuario = int.Parse(Request.Form["ResponsableServicio.Cod_Usuario"].ToString()),
                },
                Propuesta_Tecnica = "",
                Correo_Propuesta_Tecnica = "",
                Aceptacion_Propuesta_Tecnica = "",
                Fecha_Inicio_Servicio = DateTime.Now,
                Fecha_Fin_Servicio = DateTime.Now
            };
            #region "Validar Archivo Adjunto"
            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    RFP = Request.Files[i];
                    if (string.IsNullOrWhiteSpace(RFP.FileName) == false)
                    {
                        pathRFP = Server.MapPath("~") + entidad.RFP;
                        System.IO.File.Delete(pathRFP);
                        string fileRFP = entidad.Nom_Iniciativa.Replace(" ", "").ToLower() + Path.GetExtension(RFP.FileName).ToLower();
                        entidad.RFP = "/RFP/" + fileRFP;
                        pathRFP = Server.MapPath("~") + entidad.RFP;
                    }
                }
            }
            #endregion
            if (oIniciativaDominio.AsignarResponsableServicio(entidad))
            {
                oResponseWeb.Estado = true;
                oResponseWeb.Message = "Los cambios fueron guardados";
                if (string.IsNullOrWhiteSpace(RFP.FileName) == false) { RFP.SaveAs(pathRFP); }
            }
            else
            {
                oResponseWeb.Message = "Los cambios no fueron guardados";
            }

            return Json(oResponseWeb);
            #endregion
        }

        #endregion

        #region "Revisar RFP"

        [HttpGet]
        public ActionResult RevisarRFP(int id)
        {
            if (id != null && id > 0)
            {
                NegocioDominio oNegocioDominio = new NegocioDominio();
                ServicioDominio oServicioDominio = new ServicioDominio();
                ClienteDominio oClienteDominio = new ClienteDominio();
                UsuarioDominio oUsuarioDominio = new UsuarioDominio();
                var iniciativa = oIniciativaDominio.ObtenerxCodigo(id.ToString());
                ViewBag.ListaNegocio = oNegocioDominio.listarActivos();
                ViewBag.ListaTipoServicio = oServicioDominio.listarActivos();
                ViewBag.ListaCliente = oClienteDominio.listarActivos();
                ViewBag.ListaResponsableServicio = oUsuarioDominio.ListarxPerfil(new UsuarioEntidad
                {
                    Perfil = new PerfilEntidad
                    {
                        Cod_Perfil = (int)Enumeracion.PerfilUsuario.ResponsableServicio,
                    }
                });
                return View(iniciativa);
            }
            else
            {
                return RedirectToAction("Index", "Iniciativa");
            }
        }

        [HttpPost]
        public ActionResult GrabarRevisionRFP()
        {
            ResponseWeb<string> oResponseWeb = new ResponseWeb<string>() { Estado = false, };
            #region "Modificar Iniciativa"
            HttpPostedFileBase RFP = null;
            string pathRFP = "";
            IniciativaDominio oIniciativaDominio = new IniciativaDominio();
            IniciativaEntidad entidad = new IniciativaEntidad()
            {
                Cod_Iniciativa = int.Parse(Request.Form["Cod_Iniciativa"].ToString()),
                Nom_Iniciativa = Request.Form["Nom_Iniciativa"].ToString(),
                Des_Iniciativa = Request.Form["Des_Iniciativa"].ToString(),
                Negocio = new NegocioEntidad
                {
                    Cod_Negocio = int.Parse(Request.Form["Negocio.Cod_Negocio"].ToString()),
                },
                Servicio = new ServicioEntidad
                {
                    Cod_Servicio = int.Parse(Request.Form["Servicio.Cod_Servicio"].ToString()),
                },
                Cliente = new ClienteEntidad
                {
                    Cod_Cliente = int.Parse(Request.Form["Cliente.Cod_Cliente"].ToString()),
                },
                RFP = Request.Form["RFP"].ToString(),
                Estado_Iniciativa = (int)Enumeracion.EstadoFlujo.RevisarRFP,
                ResponsableServicio = new UsuarioEntidad
                {
                    Cod_Usuario = int.Parse(Request.Form["ResponsableServicio.Cod_Usuario"].ToString()),
                },
                Propuesta_Tecnica = "",
                Correo_Propuesta_Tecnica = "",
                Aceptacion_Propuesta_Tecnica = "",
                Fecha_Inicio_Servicio = DateTime.Now,
                Fecha_Fin_Servicio = DateTime.Now
            };
            #region "Validar Archivo Adjunto"
            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    RFP = Request.Files[i];
                    if (string.IsNullOrWhiteSpace(RFP.FileName) == false)
                    {
                        pathRFP = Server.MapPath("~") + entidad.RFP;
                        System.IO.File.Delete(pathRFP);
                        string fileRFP = entidad.Nom_Iniciativa.Replace(" ", "").ToLower() + Path.GetExtension(RFP.FileName).ToLower();
                        entidad.RFP = "/RFP/" + fileRFP;
                        pathRFP = Server.MapPath("~") + entidad.RFP;
                    }
                }
            }
            #endregion
            if (oIniciativaDominio.RevisarRFP(entidad))
            {
                oResponseWeb.Estado = true;
                oResponseWeb.Message = "Los cambios fueron guardados";
                if (string.IsNullOrWhiteSpace(RFP.FileName) == false) { RFP.SaveAs(pathRFP); }
            }
            else
            {
                oResponseWeb.Message = "Los cambios no fueron guardados";
            }

            return Json(oResponseWeb);
            #endregion
        }
        #endregion

        #region "Asignacion de Consultor Lider"

        [HttpGet]
        public ActionResult AsignarConsultorLider(int id)
        {
            if (id != null && id > 0)
            {
                NegocioDominio oNegocioDominio = new NegocioDominio();
                ServicioDominio oServicioDominio = new ServicioDominio();
                ClienteDominio oClienteDominio = new ClienteDominio();
                UsuarioDominio oUsuarioDominio = new UsuarioDominio();
                var iniciativa = oIniciativaDominio.ObtenerxCodigo(id.ToString());
                ViewBag.ListaNegocio = oNegocioDominio.listarActivos();
                ViewBag.ListaTipoServicio = oServicioDominio.listarActivos();
                ViewBag.ListaCliente = oClienteDominio.listarActivos();
                ViewBag.ListaResponsableServicio = oUsuarioDominio.ListarxPerfil(new UsuarioEntidad
                {
                    Perfil = new PerfilEntidad
                    {
                        Cod_Perfil = (int)Enumeracion.PerfilUsuario.ResponsableServicio,
                    }
                });
                ViewBag.ListaConsultorLider = oUsuarioDominio.ListarxPerfil(new UsuarioEntidad
                {
                    Perfil = new PerfilEntidad
                    {
                        Cod_Perfil = (int)Enumeracion.PerfilUsuario.ConsultorLider,
                    }
                });
                return View(iniciativa);
            }
            else
            {
                return RedirectToAction("Index", "Iniciativa");
            }

        }

        [HttpPost]
        public ActionResult GrabarConsultorLider()
        {
            ResponseWeb<string> oResponseWeb = new ResponseWeb<string>() { Estado = false, };
            #region "Modificar Iniciativa"
            HttpPostedFileBase RFP = null;
            string pathRFP = "";
            IniciativaDominio oIniciativaDominio = new IniciativaDominio();
            IniciativaEntidad entidad = new IniciativaEntidad()
            {
                Cod_Iniciativa = int.Parse(Request.Form["Cod_Iniciativa"].ToString()),
                Nom_Iniciativa = Request.Form["Nom_Iniciativa"].ToString(),
                Des_Iniciativa = Request.Form["Des_Iniciativa"].ToString(),
                Negocio = new NegocioEntidad
                {
                    Cod_Negocio = int.Parse(Request.Form["Negocio.Cod_Negocio"].ToString()),
                },
                Servicio = new ServicioEntidad
                {
                    Cod_Servicio = int.Parse(Request.Form["Servicio.Cod_Servicio"].ToString()),
                },
                Cliente = new ClienteEntidad
                {
                    Cod_Cliente = int.Parse(Request.Form["Cliente.Cod_Cliente"].ToString()),
                },
                RFP = Request.Form["RFP"].ToString(),
                Estado_Iniciativa = (int)Enumeracion.EstadoFlujo.AsignarConsultorLider,
                ResponsableServicio = new UsuarioEntidad
                {
                    Cod_Usuario = int.Parse(Request.Form["ResponsableServicio.Cod_Usuario"].ToString()),
                },
                ConsultorLider = new UsuarioEntidad
                {
                    Cod_Usuario = int.Parse(Request.Form["ConsultorLider.Cod_Usuario"].ToString()),
                },
                Propuesta_Tecnica = "",
                Correo_Propuesta_Tecnica = "",
                Aceptacion_Propuesta_Tecnica = "",
                Fecha_Inicio_Servicio = DateTime.Now,
                Fecha_Fin_Servicio = DateTime.Now
            };
            #region "Validar Archivo Adjunto"
            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    RFP = Request.Files[i];
                    if (string.IsNullOrWhiteSpace(RFP.FileName) == false)
                    {
                        pathRFP = Server.MapPath("~") + entidad.RFP;
                        System.IO.File.Delete(pathRFP);
                        string fileRFP = entidad.Nom_Iniciativa.Replace(" ", "").ToLower() + Path.GetExtension(RFP.FileName).ToLower();
                        entidad.RFP = "/RFP/" + fileRFP;
                        pathRFP = Server.MapPath("~") + entidad.RFP;
                    }
                }
            }
            #endregion
            if (oIniciativaDominio.RevisarRFP(entidad))
            {
                oResponseWeb.Estado = true;
                oResponseWeb.Message = "Los cambios fueron guardados";
                if (string.IsNullOrWhiteSpace(RFP.FileName) == false) { RFP.SaveAs(pathRFP); }
            }
            else
            {
                oResponseWeb.Message = "Los cambios no fueron guardados";
            }

            return Json(oResponseWeb);
            #endregion
        }

        #endregion

        #region "Estimacion de Tiempo del Proyecto"

        #region "Principal"

        [HttpGet]
        public ActionResult EstimarTiemposProyecto(int id)
        {
            if (id != null && id > 0)
            {
                NegocioDominio oNegocioDominio = new NegocioDominio();
                ServicioDominio oServicioDominio = new ServicioDominio();
                ClienteDominio oClienteDominio = new ClienteDominio();
                UsuarioDominio oUsuarioDominio = new UsuarioDominio();
                ComplejidadRequerimientoDominio oComplejidadRequerimientoDominio = new ComplejidadRequerimientoDominio();
                ComplejidadActividadDominio oComplejidadActividadDominio = new ComplejidadActividadDominio();
                var iniciativa = oIniciativaDominio.ObtenerxCodigo(id.ToString());
                ViewBag.ListaNegocio = oNegocioDominio.listarActivos();
                ViewBag.ListaTipoServicio = oServicioDominio.listarActivos();
                ViewBag.ListaCliente = oClienteDominio.listarActivos();
                ViewBag.ListaComplejidadRequerimiento = oComplejidadRequerimientoDominio.listarActivos();
                ViewBag.ListaComplejidadActividad = oComplejidadActividadDominio.listarActivos();
                ViewBag.ListaResponsableServicio = oUsuarioDominio.ListarxPerfil(new UsuarioEntidad
                {
                    Perfil = new PerfilEntidad
                    {
                        Cod_Perfil = (int)Enumeracion.PerfilUsuario.ResponsableServicio,
                    }
                });
                ViewBag.ListaConsultorLider = oUsuarioDominio.ListarxPerfil(new UsuarioEntidad
                {
                    Perfil = new PerfilEntidad
                    {
                        Cod_Perfil = (int)Enumeracion.PerfilUsuario.ConsultorLider,
                    }
                });
                return View(iniciativa);
            }
            else
            {
                return RedirectToAction("Index", "Iniciativa");
            }

        }


        [HttpPost]
        public ActionResult GrabarEstimacionTiempoProyectos()
        {
            ResponseWeb<string> oResponseWeb = new ResponseWeb<string>() { Estado = false, };
            #region "Modificar Iniciativa"
            HttpPostedFileBase RFP = null;
            string pathRFP = "";
            IniciativaDominio oIniciativaDominio = new IniciativaDominio();
            IniciativaEntidad entidad = new IniciativaEntidad()
            {
                Cod_Iniciativa = int.Parse(Request.Form["Cod_Iniciativa"].ToString()),
                Nom_Iniciativa = Request.Form["Nom_Iniciativa"].ToString(),
                Des_Iniciativa = Request.Form["Des_Iniciativa"].ToString(),
                Negocio = new NegocioEntidad
                {
                    Cod_Negocio = int.Parse(Request.Form["Negocio.Cod_Negocio"].ToString()),
                },
                Servicio = new ServicioEntidad
                {
                    Cod_Servicio = int.Parse(Request.Form["Servicio.Cod_Servicio"].ToString()),
                },
                Cliente = new ClienteEntidad
                {
                    Cod_Cliente = int.Parse(Request.Form["Cliente.Cod_Cliente"].ToString()),
                },
                RFP = Request.Form["RFP"].ToString(),
                Estado_Iniciativa = (int)Enumeracion.EstadoFlujo.EstimarTiempoProyecto,
                ResponsableServicio = new UsuarioEntidad
                {
                    Cod_Usuario = int.Parse(Request.Form["ResponsableServicio.Cod_Usuario"].ToString()),
                },
                ConsultorLider = new UsuarioEntidad
                {
                    Cod_Usuario = int.Parse(Request.Form["ConsultorLider.Cod_Usuario"].ToString()),
                },
                Propuesta_Tecnica = "",
                Correo_Propuesta_Tecnica = "",
                Aceptacion_Propuesta_Tecnica = "",
                Fecha_Inicio_Servicio = DateTime.Now,
                Fecha_Fin_Servicio = DateTime.Now
            };
            #region "Validar Archivo Adjunto"
            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    RFP = Request.Files[i];
                    if (string.IsNullOrWhiteSpace(RFP.FileName) == false)
                    {
                        pathRFP = Server.MapPath("~") + entidad.RFP;
                        System.IO.File.Delete(pathRFP);
                        string fileRFP = entidad.Nom_Iniciativa.Replace(" ", "").ToLower() + Path.GetExtension(RFP.FileName).ToLower();
                        entidad.RFP = "/RFP/" + fileRFP;
                        pathRFP = Server.MapPath("~") + entidad.RFP;
                    }
                }
            }
            #endregion
            if (oIniciativaDominio.EstimarTiempoProyecto(entidad))
            {
                oResponseWeb.Estado = true;
                oResponseWeb.Message = "Los cambios fueron guardados";
                if (string.IsNullOrWhiteSpace(RFP.FileName) == false) { RFP.SaveAs(pathRFP); }
            }
            else
            {
                oResponseWeb.Message = "Los cambios no fueron guardados";
            }

            return Json(oResponseWeb);
            #endregion
        }
        #endregion

        #region "Popup"
        [HttpPost]
        public ActionResult ListarRequerimientoxIniciativa(RequerimientoEntidad entidad)
        {
            RequerimientoDominio oRequerimientoDominio = new RequerimientoDominio();
            var Lista = oRequerimientoDominio.listarxIniciativa(entidad);
            return PartialView("_ResultadoRequermientos", Lista);
        }

        [HttpPost]
        public ActionResult GrabarRequerimiento(RequerimientoEntidad entidad)
        {
            ResponseWeb<int> oResponseWeb = new ResponseWeb<int> { Estado = false, };
            RequerimientoDominio oRequerimientoDominio = new RequerimientoDominio();
            if (oRequerimientoDominio.Grabar(entidad))
            {
                oResponseWeb.Estado = true;
                oResponseWeb.Message = "Se agrego el requerimiento";
            }
            else
            {
                oResponseWeb.Estado = false;
                oResponseWeb.Message = "No se agrego el requerimiento";
            }
            return Json(oResponseWeb);
        }

        [HttpPost]
        public ActionResult EliminarRequerimiento(RequerimientoEntidad entidad)
        {
            ResponseWeb<int> oResponseWeb = new ResponseWeb<int> { Estado = false, };
            RequerimientoDominio oRequerimientoDominio = new RequerimientoDominio();
            if (oRequerimientoDominio.Eliminar(entidad.Cod_Requerimiento.ToString()))
            {
                oResponseWeb.Estado = true;
                oResponseWeb.Message = "Se eliminó el requerimiento";
            }
            else
            {
                oResponseWeb.Estado = false;
                oResponseWeb.Message = "No se eliminó el requerimiento";
            }
            return Json(oResponseWeb);
        }

        [HttpPost]
        public ActionResult BuscarActividades(ActividadEntidad entidad)
        {
            ActividadDominio oActividadDominio = new ActividadDominio();
            var Lista = oActividadDominio.filtrar(entidad);
            return PartialView("_ResultadoActividades", Lista);
        }
        [HttpPost]
        public ActionResult GrabarRequerimientoActividad(RequerimientoActividadEntidad entidad)
        {
            ResponseWeb<int> oResponseWeb = new ResponseWeb<int> { Estado = false, };
            RequerimientoActividadDominio oRequerimientoActividadDominio = new RequerimientoActividadDominio();
            if (oRequerimientoActividadDominio.Grabar(entidad))
            {
                oResponseWeb.Estado = true;
                oResponseWeb.Message = "Se agrego la actividad al requerimiento";
            }
            else
            {
                oResponseWeb.Estado = false;
                oResponseWeb.Message = "No se agrego la actividad al requerimiento";
            }
            return Json(oResponseWeb);
        }

        [HttpPost]
        public ActionResult EliminarRequerimientoActividad(RequerimientoActividadEntidad entidad)
        {
            ResponseWeb<int> oResponseWeb = new ResponseWeb<int> { Estado = false, };
            RequerimientoActividadDominio oRequerimientoActividadDominio = new RequerimientoActividadDominio();
            if (oRequerimientoActividadDominio.Eliminar(entidad.Cod_Requerimiento_Actividad.ToString()))
            {
                oResponseWeb.Estado = true;
                oResponseWeb.Message = "Se eliminó la actividad del requerimiento";
            }
            else
            {
                oResponseWeb.Estado = false;
                oResponseWeb.Message = "No se eliminó la actividad del requerimiento";
            }
            return Json(oResponseWeb);
        }

        [HttpPost]
        public ActionResult ListarRequerimientoActividadxRequerimiento(RequerimientoActividadEntidad entidad)
        {
            RequerimientoActividadDominio oRequerimientoActividadDominio = new RequerimientoActividadDominio();
            var Lista = oRequerimientoActividadDominio.listarxRequerimiento(entidad);
            return PartialView("_ResultadoRequermientoActividades", Lista);
        }

        [HttpPost]
        public ActionResult RequerimientoActividadExisteActividad(RequerimientoActividadEntidad entidad)
        {
            RequerimientoActividadDominio oRequerimientoActividadDominio = new RequerimientoActividadDominio();
            ResponseWeb<int> oResponseWeb = new ResponseWeb<int> { Estado = false, };
            var estado = oRequerimientoActividadDominio.existeActividad(entidad);
            if (estado == true)
                oResponseWeb.Estado = estado;
            else
            {
                oResponseWeb.Estado = estado;
                oResponseWeb.Message = "Falta agregar las actividades a todos los requerimientos para continuar...";
            }
            return Json(oResponseWeb);
        }

        [HttpPost]
        public ActionResult BuscarRequerimientoxIniciativa(RequerimientoActividadEntidad entidad)
        {
            RequerimientoActividadDominio oRequerimientoActividadDominio = new RequerimientoActividadDominio();
            var Lista = oRequerimientoActividadDominio.listarxIniciativa(entidad);
            return PartialView("_ResultadosTiempoConstruccion", Lista);
        }

        [HttpPost]
        public ActionResult GenerarTiempoFases(IniciativaFaseEntidad entidad)
        {
            IniciativaFaseDominio oIniciativaFaseDominio = new IniciativaFaseDominio();
            ResponseWeb<int> oResponseWeb = new ResponseWeb<int> { Estado = false };
            oResponseWeb.Estado = oIniciativaFaseDominio.GenerarTiempoFase(entidad.Iniciativa.Cod_Iniciativa.ToString());
            if (oResponseWeb.Estado == false)
                oResponseWeb.Message = "No se puedo calcular el tiempo de las fases del proyecto...";
            return Json(oResponseWeb);
        }

        [HttpPost]
        public ActionResult ListarTiempoFases(IniciativaFaseEntidad entidad)
        {
            IniciativaFaseDominio oIniciativaFaseDominio = new IniciativaFaseDominio();
            var Lista = oIniciativaFaseDominio.listarxIniciativa(entidad.Iniciativa.Cod_Iniciativa.ToString());
            int TotalHoras = int.Parse((from x in Lista select x.Horas).Sum().ToString());
            ViewBag.TotalHoras = TotalHoras;
            return PartialView("_ResultadoTiempoFases", Lista);
        }
        #endregion

        #endregion

        #region "Estimacion de Consultores del Proyecto"

        #region "Principal"

        [HttpGet]
        public ActionResult EstimarConsultoresProyecto(int id)
        {
            if (id != null && id > 0)
            {
                SessionManager.ListaIniciativaCompetencia = null;
                NegocioDominio oNegocioDominio = new NegocioDominio();
                ServicioDominio oServicioDominio = new ServicioDominio();
                ClienteDominio oClienteDominio = new ClienteDominio();
                UsuarioDominio oUsuarioDominio = new UsuarioDominio();
                CompetenciaDominio oCompetenciaDominio = new CompetenciaDominio();
                NivelCompetenciaDominio oNivelCompetenciaDominio = new NivelCompetenciaDominio();
                var iniciativa = oIniciativaDominio.ObtenerxCodigo(id.ToString());
                ViewBag.ListaNegocio = oNegocioDominio.listarActivos();
                ViewBag.ListaTipoServicio = oServicioDominio.listarActivos();
                ViewBag.ListaCliente = oClienteDominio.listarActivos();
                ViewBag.ListaCompetencias = oCompetenciaDominio.listarActivos();
                ViewBag.ListaNivelCompetencias = oNivelCompetenciaDominio.listarActivos();
                ViewBag.ListaResponsableServicio = oUsuarioDominio.ListarxPerfil(new UsuarioEntidad
                {
                    Perfil = new PerfilEntidad
                    {
                        Cod_Perfil = (int)Enumeracion.PerfilUsuario.ResponsableServicio,
                    }
                });
                ViewBag.ListaConsultorLider = oUsuarioDominio.ListarxPerfil(new UsuarioEntidad
                {
                    Perfil = new PerfilEntidad
                    {
                        Cod_Perfil = (int)Enumeracion.PerfilUsuario.ConsultorLider,
                    }
                });
                return View(iniciativa);
            }
            else
            {
                return RedirectToAction("Index", "Iniciativa");
            }

        }

        [HttpPost]
        public ActionResult GrabarEstimacionConsultores()
        {
            ResponseWeb<string> oResponseWeb = new ResponseWeb<string>() { Estado = false, };
            #region "Modificar Iniciativa"
            HttpPostedFileBase RFP = null;
            string pathRFP = "";
            IniciativaDominio oIniciativaDominio = new IniciativaDominio();
            IniciativaEntidad entidad = new IniciativaEntidad()
            {
                Cod_Iniciativa = int.Parse(Request.Form["Cod_Iniciativa"].ToString()),
                Nom_Iniciativa = Request.Form["Nom_Iniciativa"].ToString(),
                Des_Iniciativa = Request.Form["Des_Iniciativa"].ToString(),
                Negocio = new NegocioEntidad
                {
                    Cod_Negocio = int.Parse(Request.Form["Negocio.Cod_Negocio"].ToString()),
                },
                Servicio = new ServicioEntidad
                {
                    Cod_Servicio = int.Parse(Request.Form["Servicio.Cod_Servicio"].ToString()),
                },
                Cliente = new ClienteEntidad
                {
                    Cod_Cliente = int.Parse(Request.Form["Cliente.Cod_Cliente"].ToString()),
                },
                RFP = Request.Form["RFP"].ToString(),
                Estado_Iniciativa = (int)Enumeracion.EstadoFlujo.EstimarConsultores,
                ResponsableServicio = new UsuarioEntidad
                {
                    Cod_Usuario = int.Parse(Request.Form["ResponsableServicio.Cod_Usuario"].ToString()),
                },
                ConsultorLider = new UsuarioEntidad
                {
                    Cod_Usuario = int.Parse(Request.Form["ConsultorLider.Cod_Usuario"].ToString()),
                },
                Propuesta_Tecnica = "",
                Correo_Propuesta_Tecnica = "",
                Aceptacion_Propuesta_Tecnica = "",
                Fecha_Inicio_Servicio = DateTime.Now,
                Fecha_Fin_Servicio = DateTime.Now
            };
            #region "Validar Archivo Adjunto"
            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    RFP = Request.Files[i];
                    if (string.IsNullOrWhiteSpace(RFP.FileName) == false)
                    {
                        pathRFP = Server.MapPath("~") + entidad.RFP;
                        System.IO.File.Delete(pathRFP);
                        string fileRFP = entidad.Nom_Iniciativa.Replace(" ", "").ToLower() + Path.GetExtension(RFP.FileName).ToLower();
                        entidad.RFP = "/RFP/" + fileRFP;
                        pathRFP = Server.MapPath("~") + entidad.RFP;
                    }
                }
            }
            #endregion
            if (oIniciativaDominio.EstimarConsultores(entidad))
            {
                oResponseWeb.Estado = true;
                oResponseWeb.Message = "Los cambios fueron guardados";
                if (string.IsNullOrWhiteSpace(RFP.FileName) == false) { RFP.SaveAs(pathRFP); }
            }
            else
            {
                oResponseWeb.Message = "Los cambios no fueron guardados";
            }

            return Json(oResponseWeb);
            #endregion
        }

        #endregion

        #region "Popup"

        [HttpPost]
        public ActionResult ListarCompetenciasxIniciativa(IniciativaCompetenciaEntidad entidad)
        {
            IniciativaCompetenciaDominio oIniciativaCompetenciaDominio = new IniciativaCompetenciaDominio();
            IniciativaFaseDominio oIniciativaFaseDominio = new IniciativaFaseDominio();
            var ListaFases = oIniciativaFaseDominio.listarxIniciativa(entidad.Iniciativa.Cod_Iniciativa.ToString());
            var Lista = oIniciativaCompetenciaDominio.listarxIniciativa(entidad);
            int TotalHorasProyecto = int.Parse((from x in ListaFases select x.Horas).Sum().ToString());
            int HorasParticipacion = int.Parse((from x in Lista select x.Horas_Participacion).Sum().ToString());
            ViewBag.TotalHorasProyecto = TotalHorasProyecto;
            ViewBag.HorasParticipacion = HorasParticipacion;
            ViewBag.RestantesHorasProyecto = (TotalHorasProyecto - HorasParticipacion);
            ViewBag.IndiCompetencia = "0";
            if ((TotalHorasProyecto - HorasParticipacion) == 0)
                ViewBag.IndiCompetencia = "1";

            return PartialView("_ResultadoCompetencias", Lista);
        }


        [HttpPost]
        public ActionResult GrabarCompetencia(IniciativaCompetenciaEntidad entidad)
        {
            ResponseWeb<int> oResponseWeb = new ResponseWeb<int> { Estado = false, };
            IniciativaCompetenciaDominio oIniciativaCompetenciaDominio = new IniciativaCompetenciaDominio();
            IniciativaFaseDominio oIniciativaFaseDominio = new IniciativaFaseDominio();
            var ListaFases = oIniciativaFaseDominio.listarxIniciativa(entidad.Iniciativa.Cod_Iniciativa.ToString());
            var Lista = oIniciativaCompetenciaDominio.listarxIniciativa(entidad);
            int TotalHorasProyecto = int.Parse((from x in ListaFases select x.Horas).Sum().ToString());
            int HorasParticipacion = int.Parse((from x in Lista select x.Horas_Participacion).Sum().ToString());
            ViewBag.IndiCompetencia = "0";
            if ((TotalHorasProyecto - HorasParticipacion) == 0)
                ViewBag.IndiCompetencia = "1";
            if ((HorasParticipacion + int.Parse(entidad.Horas_Participacion.ToString())) > TotalHorasProyecto)
            {
                oResponseWeb.Estado = false;
                oResponseWeb.Message = "El Total de Horas de Participación ingresadas no coincide con el Total de Horas Estimadas...";
            }
            else
            {
                if (oIniciativaCompetenciaDominio.Grabar(entidad))
                {
                    oResponseWeb.Estado = true;
                    oResponseWeb.Message = "Se agrego la competencia";
                }
                else
                {
                    oResponseWeb.Estado = false;
                    oResponseWeb.Message = "No se agrego la competencia";
                }
            }


            return Json(oResponseWeb);
        }

        [HttpPost]
        public ActionResult ModificarCompetencia(IniciativaCompetenciaEntidad entidad)
        {
            ResponseWeb<string> oResponseWeb = new ResponseWeb<string> { Estado = false, };
            IniciativaCompetenciaDominio oIniciativaCompetenciaDominio = new IniciativaCompetenciaDominio();
            IniciativaFaseDominio oIniciativaFaseDominio = new IniciativaFaseDominio();
            var ListaFases = oIniciativaFaseDominio.listarxIniciativa(entidad.Iniciativa.Cod_Iniciativa.ToString());
            var Lista = oIniciativaCompetenciaDominio.listarxIniciativa(entidad);
            int TotalHorasProyecto = int.Parse((from x in ListaFases select x.Horas).Sum().ToString());
            int HorasParticipacion = int.Parse((from x in Lista select x.Horas_Participacion).Sum().ToString());
            if (((HorasParticipacion - int.Parse(entidad.Horas_Participacion_Antes.ToString()) + (int.Parse(entidad.Horas_Participacion.ToString())))) > TotalHorasProyecto)
            {

                oResponseWeb.Estado = false;
                oResponseWeb.Message = "El Total de Horas de Participación ingresadas no coincide con el Total de Horas Estimadas...";
            }
            else
            {
                if (oIniciativaCompetenciaDominio.Modificar(entidad))
                {
                    var TotalHorasParticipacion = ((HorasParticipacion - int.Parse(entidad.Horas_Participacion_Antes.ToString()) + (int.Parse(entidad.Horas_Participacion.ToString())))).ToString();
                    var TotalHorasRestantes = (TotalHorasProyecto - int.Parse(TotalHorasParticipacion)).ToString();

                    oResponseWeb.Valor = TotalHorasParticipacion + "|" + TotalHorasRestantes;
                    oResponseWeb.Estado = true;
                    oResponseWeb.Message = "Cambio realizado..";
                }
                else
                {
                    oResponseWeb.Estado = false;
                    oResponseWeb.Message = "No se realizo el cambio...";
                }
            }

            return Json(oResponseWeb);
        }

        [HttpPost]
        public ActionResult EliminarCompetencia(IniciativaCompetenciaEntidad entidad)
        {
            ResponseWeb<int> oResponseWeb = new ResponseWeb<int> { Estado = false, };
            IniciativaCompetenciaDominio oIniciativaCompetenciaDominio = new IniciativaCompetenciaDominio();
            IniciativaFaseDominio oIniciativaFaseDominio = new IniciativaFaseDominio();
            var ListaFases = oIniciativaFaseDominio.listarxIniciativa(entidad.Iniciativa.Cod_Iniciativa.ToString());
            var Lista = oIniciativaCompetenciaDominio.listarxIniciativa(entidad);
            int TotalHorasProyecto = int.Parse((from x in ListaFases select x.Horas).Sum().ToString());
            int HorasParticipacion = int.Parse((from x in Lista select x.Horas_Participacion).Sum().ToString());
            ViewBag.TotalHorasProyecto = TotalHorasProyecto;
            ViewBag.HorasParticipacion = HorasParticipacion;
            ViewBag.RestantesHorasProyecto = (TotalHorasProyecto - HorasParticipacion);
            ViewBag.IndiCompetencia = "0";
            if ((TotalHorasProyecto - HorasParticipacion) == 0)
                ViewBag.IndiCompetencia = "1";

            if (oIniciativaCompetenciaDominio.Eliminar(entidad.Cod_Iniciativa_Competencia.ToString()))
            {
                oResponseWeb.Estado = true;
                oResponseWeb.Message = "Se eliminó la competencia";
            }
            else
            {
                oResponseWeb.Estado = false;
                oResponseWeb.Message = "No se eliminó la competencia";
            }
            return Json(oResponseWeb);
        }

        #endregion


        #endregion

        #region "Evaluacion de Rentabilidad"

        #region "Principal"

        [HttpGet]
        public ActionResult EvaluarRentabilidad(int id)
        {
            if (id != null && id > 0)
            {
                //id = 1;
                NegocioDominio oNegocioDominio = new NegocioDominio();
                ServicioDominio oServicioDominio = new ServicioDominio();
                ClienteDominio oClienteDominio = new ClienteDominio();
                UsuarioDominio oUsuarioDominio = new UsuarioDominio();
                var iniciativa = oIniciativaDominio.ObtenerxCodigo(id.ToString());
                ViewBag.ListaNegocio = oNegocioDominio.listarActivos();
                ViewBag.ListaTipoServicio = oServicioDominio.listarActivos();
                ViewBag.ListaCliente = oClienteDominio.listarActivos();
                ViewBag.ListaResponsableServicio = oUsuarioDominio.ListarxPerfil(new UsuarioEntidad
                {
                    Perfil = new PerfilEntidad
                    {
                        Cod_Perfil = (int)Enumeracion.PerfilUsuario.ResponsableServicio,
                    }
                });
                ViewBag.ListaConsultorLider = oUsuarioDominio.ListarxPerfil(new UsuarioEntidad
                {
                    Perfil = new PerfilEntidad
                    {
                        Cod_Perfil = (int)Enumeracion.PerfilUsuario.ConsultorLider,
                    }
                });
                SessionManager.IniciativaEntidad = new IniciativaEntidad();
                return View(iniciativa);
            }
            else
            {
                return RedirectToAction("Index", "Iniciativa");
            }

        }


        [HttpPost]
        public ActionResult GrabarEvaluacionRentabilidad()
        {
            ResponseWeb<string> oResponseWeb = new ResponseWeb<string>() { Estado = false, };
            #region "Modificar Iniciativa"
            HttpPostedFileBase RFP = null;
            string pathRFP = "";
            IniciativaDominio oIniciativaDominio = new IniciativaDominio();
            IniciativaEntidad entidad = new IniciativaEntidad()
            {
                Cod_Iniciativa = int.Parse(Request.Form["Cod_Iniciativa"].ToString()),
                Nom_Iniciativa = Request.Form["Nom_Iniciativa"].ToString(),
                Des_Iniciativa = Request.Form["Des_Iniciativa"].ToString(),
                Negocio = new NegocioEntidad
                {
                    Cod_Negocio = int.Parse(Request.Form["Negocio.Cod_Negocio"].ToString()),
                },
                Servicio = new ServicioEntidad
                {
                    Cod_Servicio = int.Parse(Request.Form["Servicio.Cod_Servicio"].ToString()),
                },
                Cliente = new ClienteEntidad
                {
                    Cod_Cliente = int.Parse(Request.Form["Cliente.Cod_Cliente"].ToString()),
                },
                RFP = Request.Form["RFP"].ToString(),
                Estado_Iniciativa = (int)Enumeracion.EstadoFlujo.EvaluacionRentabilidad,
                ResponsableServicio = new UsuarioEntidad
                {
                    Cod_Usuario = int.Parse(Request.Form["ResponsableServicio.Cod_Usuario"].ToString()),
                },
                ConsultorLider = new UsuarioEntidad
                {
                    Cod_Usuario = int.Parse(Request.Form["ConsultorLider.Cod_Usuario"].ToString()),
                },
                Propuesta_Tecnica = "",
                Correo_Propuesta_Tecnica = "",
                Aceptacion_Propuesta_Tecnica = "",
                Fecha_Inicio_Servicio = DateTime.Now,
                Fecha_Fin_Servicio = DateTime.Now
            };
            #region "Validar Archivo Adjunto"
            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    RFP = Request.Files[i];
                    if (string.IsNullOrWhiteSpace(RFP.FileName) == false)
                    {
                        pathRFP = Server.MapPath("~") + entidad.RFP;
                        System.IO.File.Delete(pathRFP);
                        string fileRFP = entidad.Nom_Iniciativa.Replace(" ", "").ToLower() + Path.GetExtension(RFP.FileName).ToLower();
                        entidad.RFP = "/RFP/" + fileRFP;
                        pathRFP = Server.MapPath("~") + entidad.RFP;
                    }
                }
            }
            #endregion
            if (oIniciativaDominio.EvaluarRentabilidad(entidad))
            {
                oResponseWeb.Estado = true;
                oResponseWeb.Message = "Los cambios fueron guardados";
                if (string.IsNullOrWhiteSpace(RFP.FileName) == false) { RFP.SaveAs(pathRFP); }
            }
            else
            {
                oResponseWeb.Message = "Los cambios no fueron guardados";
            }

            return Json(oResponseWeb);
            #endregion
        }


        [HttpPost]
        public ActionResult ValidarRentabilidad(IniciativaEntidad entidad)
        {
            ResponseWeb<string> oResponseWeb = new ResponseWeb<string>() { Estado = false, };
            IniciativaDominio oIniciativaDominio = new IniciativaDominio();
            var response = oIniciativaDominio.ValidaRentabilidad(entidad.Cod_Iniciativa.ToString());
            oResponseWeb.Estado = response.Valor;
            oResponseWeb.Message = response.Mensaje;
            return Json(oResponseWeb);

        }

        #endregion

        #region "Popup"

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

        #endregion

        #endregion

        #region "Desarrollar Propuesta Tecnica"
        [HttpGet]
        public ActionResult DesarrollarPropuestaTecnica(int id)
        {
            if (id != null && id > 0)
            {
                NegocioDominio oNegocioDominio = new NegocioDominio();
                ServicioDominio oServicioDominio = new ServicioDominio();
                ClienteDominio oClienteDominio = new ClienteDominio();
                UsuarioDominio oUsuarioDominio = new UsuarioDominio();
                var iniciativa = oIniciativaDominio.ObtenerxCodigo(id.ToString());
                ViewBag.ListaNegocio = oNegocioDominio.listarActivos();
                ViewBag.ListaTipoServicio = oServicioDominio.listarActivos();
                ViewBag.ListaCliente = oClienteDominio.listarActivos();
                ViewBag.ListaResponsableServicio = oUsuarioDominio.ListarxPerfil(new UsuarioEntidad
                {
                    Perfil = new PerfilEntidad
                    {
                        Cod_Perfil = (int)Enumeracion.PerfilUsuario.ResponsableServicio,
                    }
                });
                ViewBag.ListaConsultorLider = oUsuarioDominio.ListarxPerfil(new UsuarioEntidad
                {
                    Perfil = new PerfilEntidad
                    {
                        Cod_Perfil = (int)Enumeracion.PerfilUsuario.ConsultorLider,
                    }
                });
                return View(iniciativa);
            }
            else
            {
                return RedirectToAction("Index", "Iniciativa");
            }
        }


        [HttpPost]
        public ActionResult GrabarDesarrollarPropuestaTecnica()
        {
            ResponseWeb<string> oResponseWeb = new ResponseWeb<string>() { Estado = false, };
            #region "Validar Archivo Propuesta Tecnica"
            var PropuestaTecnica = Request.Form["Propuesta_Tecnica"].ToString();
            var Doc_RFP = Request.Form["RFP"].ToString();

            if (Request.Files.Count > 0 || string.IsNullOrWhiteSpace(PropuestaTecnica) == false)
            {
                HttpPostedFileBase PT = null;
                HttpPostedFileBase RFP = null;
                PT = Request.Files["PT"];
                RFP = Request.Files["RFP"];

                if (string.IsNullOrWhiteSpace(PropuestaTecnica) && (PT == null || string.IsNullOrWhiteSpace(PT.FileName)))
                {
                    oResponseWeb.Message = "No se cargo la Propuesta Técnica";
                    oResponseWeb.Valor = "0";
                    return Json(oResponseWeb);
                }
                #region "Modificar Iniciativa"

                string pathRFP = "";
                string pathPT = "";
                IniciativaDominio oIniciativaDominio = new IniciativaDominio();
                IniciativaEntidad entidad = new IniciativaEntidad()
                {
                    Cod_Iniciativa = int.Parse(Request.Form["Cod_Iniciativa"].ToString()),
                    Nom_Iniciativa = Request.Form["Nom_Iniciativa"].ToString(),
                    Des_Iniciativa = Request.Form["Des_Iniciativa"].ToString(),
                    Negocio = new NegocioEntidad
                    {
                        Cod_Negocio = int.Parse(Request.Form["Negocio.Cod_Negocio"].ToString()),
                    },
                    Servicio = new ServicioEntidad
                    {
                        Cod_Servicio = int.Parse(Request.Form["Servicio.Cod_Servicio"].ToString()),
                    },
                    Cliente = new ClienteEntidad
                    {
                        Cod_Cliente = int.Parse(Request.Form["Cliente.Cod_Cliente"].ToString()),
                    },
                    RFP = Request.Form["RFP"].ToString(),
                    Estado_Iniciativa = (int)Enumeracion.EstadoFlujo.DesarrollarPropuestaTecnica,
                    ResponsableServicio = new UsuarioEntidad
                    {
                        Cod_Usuario = int.Parse(Request.Form["ResponsableServicio.Cod_Usuario"].ToString()),
                    },
                    ConsultorLider = new UsuarioEntidad
                    {
                        Cod_Usuario = int.Parse(Request.Form["ConsultorLider.Cod_Usuario"].ToString()),
                    },
                    Propuesta_Tecnica = Request.Form["Propuesta_Tecnica"].ToString(),
                    Correo_Propuesta_Tecnica = "",
                    Aceptacion_Propuesta_Tecnica = "",
                    Fecha_Inicio_Servicio = DateTime.Now,
                    Fecha_Fin_Servicio = DateTime.Now
                };
                //if (string.IsNullOrWhiteSpace(entidad.Propuesta_Tecnica) == false)
                //{
                //    filePT = Server.MapPath("~") + entidad.Propuesta_Tecnica;
                //    System.IO.File.Delete(filePT);
                //}
                //filePT = entidad.Nom_Iniciativa.Replace(" ", "").ToLower() + Path.GetExtension(PT.FileName).ToLower();
                //entidad.Propuesta_Tecnica = "/PT/" + filePT;
                //string pathPT = Server.MapPath("~") + entidad.Propuesta_Tecnica;

                #region "Validar Archivo Adjunto Propuesta Tecnica"
                if (PT != null)
                {
                    if (string.IsNullOrWhiteSpace(PT.FileName) == false)
                    {
                        pathPT = Server.MapPath("~") + entidad.Propuesta_Tecnica;
                        if (System.IO.File.Exists(pathPT)) System.IO.File.Delete(pathPT);
                        string filePT = entidad.Nom_Iniciativa.Replace(" ", "").ToLower() + Path.GetExtension(PT.FileName).ToLower();
                        entidad.Propuesta_Tecnica = "/PT/" + filePT;
                        pathPT = Server.MapPath("~") + entidad.Propuesta_Tecnica;
                    }
                }

                #endregion

                #region "Validar Archivo Adjunto RFP"
                if (RFP != null)
                {
                    if (string.IsNullOrWhiteSpace(RFP.FileName) == false)
                    {
                        pathRFP = Server.MapPath("~") + entidad.RFP;
                        if (System.IO.File.Exists(pathRFP)) System.IO.File.Delete(pathRFP);
                        string fileRFP = entidad.Nom_Iniciativa.Replace(" ", "").ToLower() + Path.GetExtension(RFP.FileName).ToLower();
                        entidad.RFP = "/RFP/" + fileRFP;
                        pathRFP = Server.MapPath("~") + entidad.RFP;
                    }
                }

                #endregion
                if (oIniciativaDominio.ActualizarEstado(entidad))
                {
                    oResponseWeb.Estado = true;
                    oResponseWeb.Message = "Los cambios fueron guardados";
                    if (string.IsNullOrWhiteSpace(RFP.FileName) == false) { RFP.SaveAs(pathRFP); }
                    if (string.IsNullOrWhiteSpace(PT.FileName) == false) { PT.SaveAs(pathPT); }

                }
                else
                {
                    oResponseWeb.Message = "Los cambios no fueron guardados";
                }

                return Json(oResponseWeb);
                #endregion

            }
            else
            {
                oResponseWeb.Message = "No se cargo la Propuesta Tecnica";
                return Json(oResponseWeb);
            }

            #endregion



        }

        #endregion

        #region "Revisar Propuesta Tecnica"
        [HttpGet]
        public ActionResult RevisarPropuestaTecnica(int id)
        {
            if (id != null && id > 0)
            {
                NegocioDominio oNegocioDominio = new NegocioDominio();
                ServicioDominio oServicioDominio = new ServicioDominio();
                ClienteDominio oClienteDominio = new ClienteDominio();
                UsuarioDominio oUsuarioDominio = new UsuarioDominio();
                var iniciativa = oIniciativaDominio.ObtenerxCodigo(id.ToString());
                ViewBag.ListaNegocio = oNegocioDominio.listarActivos();
                ViewBag.ListaTipoServicio = oServicioDominio.listarActivos();
                ViewBag.ListaCliente = oClienteDominio.listarActivos();
                ViewBag.ListaResponsableServicio = oUsuarioDominio.ListarxPerfil(new UsuarioEntidad
                {
                    Perfil = new PerfilEntidad
                    {
                        Cod_Perfil = (int)Enumeracion.PerfilUsuario.ResponsableServicio,
                    }
                });
                ViewBag.ListaConsultorLider = oUsuarioDominio.ListarxPerfil(new UsuarioEntidad
                {
                    Perfil = new PerfilEntidad
                    {
                        Cod_Perfil = (int)Enumeracion.PerfilUsuario.ConsultorLider,
                    }
                });
                return View(iniciativa);
            }
            else
            {
                return RedirectToAction("Index", "Iniciativa");
            }
        }

        [HttpPost]
        public ActionResult GrabarRevisarPropuestaTecnica()
        {
            ResponseWeb<string> oResponseWeb = new ResponseWeb<string>() { Estado = false, };
            #region "Validar Archivo Propuesta Tecnica"

            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase PT = null;
                HttpPostedFileBase RFP = null;
                PT = Request.Files["PT"];
                RFP = Request.Files["RFP"];

                #region "Modificar Iniciativa"

                string pathRFP = "";
                string pathPT = "";
                IniciativaDominio oIniciativaDominio = new IniciativaDominio();
                IniciativaEntidad entidad = new IniciativaEntidad()
                {
                    Cod_Iniciativa = int.Parse(Request.Form["Cod_Iniciativa"].ToString()),
                    Nom_Iniciativa = Request.Form["Nom_Iniciativa"].ToString(),
                    Des_Iniciativa = Request.Form["Des_Iniciativa"].ToString(),
                    Negocio = new NegocioEntidad
                    {
                        Cod_Negocio = int.Parse(Request.Form["Negocio.Cod_Negocio"].ToString()),
                    },
                    Servicio = new ServicioEntidad
                    {
                        Cod_Servicio = int.Parse(Request.Form["Servicio.Cod_Servicio"].ToString()),
                    },
                    Cliente = new ClienteEntidad
                    {
                        Cod_Cliente = int.Parse(Request.Form["Cliente.Cod_Cliente"].ToString()),
                    },
                    RFP = Request.Form["RFP"].ToString(),
                    Estado_Iniciativa = (int)Enumeracion.EstadoFlujo.RevisarPropuestaTecnica,
                    ResponsableServicio = new UsuarioEntidad
                    {
                        Cod_Usuario = int.Parse(Request.Form["ResponsableServicio.Cod_Usuario"].ToString()),
                    },
                    ConsultorLider = new UsuarioEntidad
                    {
                        Cod_Usuario = int.Parse(Request.Form["ConsultorLider.Cod_Usuario"].ToString()),
                    },
                    Propuesta_Tecnica = Request.Form["Propuesta_Tecnica"].ToString(),
                    Correo_Propuesta_Tecnica = "",
                    Aceptacion_Propuesta_Tecnica = "",
                    Fecha_Inicio_Servicio = DateTime.Now,
                    Fecha_Fin_Servicio = DateTime.Now
                };


                #region "Validar Archivo Adjunto RFP"
                if (RFP != null)
                {
                    if (string.IsNullOrWhiteSpace(RFP.FileName) == false)
                    {
                        pathRFP = Server.MapPath("~") + entidad.RFP;
                        if (System.IO.File.Exists(pathRFP)) System.IO.File.Delete(pathRFP);
                        string fileRFP = entidad.Nom_Iniciativa.Replace(" ", "").ToLower() + Path.GetExtension(RFP.FileName).ToLower();
                        entidad.RFP = "/RFP/" + fileRFP;
                        pathRFP = Server.MapPath("~") + entidad.RFP;
                    }
                }

                #endregion

                #region "Validar Archivo Adjunto Propuesta Tecnica"
                if (PT != null)
                {
                    if (string.IsNullOrWhiteSpace(PT.FileName) == false)
                    {
                        pathPT = Server.MapPath("~") + entidad.Propuesta_Tecnica;
                        if (System.IO.File.Exists(pathPT)) System.IO.File.Delete(pathPT);
                        string filePT = entidad.Nom_Iniciativa.Replace(" ", "").ToLower() + Path.GetExtension(PT.FileName).ToLower();
                        entidad.Propuesta_Tecnica = "/PT/" + filePT;
                        pathPT = Server.MapPath("~") + entidad.Propuesta_Tecnica;
                    }
                }

                #endregion

                if (oIniciativaDominio.ActualizarEstado(entidad))
                {
                    oResponseWeb.Estado = true;
                    oResponseWeb.Message = "Los cambios fueron guardados";
                    if (string.IsNullOrWhiteSpace(RFP.FileName) == false) { RFP.SaveAs(pathRFP); }
                    if (string.IsNullOrWhiteSpace(PT.FileName) == false) { PT.SaveAs(pathPT); }

                }
                else
                {
                    oResponseWeb.Message = "Los cambios no fueron guardados";
                }

                return Json(oResponseWeb);
                #endregion

            }
            else
            {
                oResponseWeb.Message = "No se cargo la Propuesta Tecnica";
                return Json(oResponseWeb);
            }

            #endregion



        }
        #endregion

        #region "Esperando Respuesta del Cliente"
        [HttpGet]
        public ActionResult RespuestaCliente(int id)
        {
            if (id != null && id > 0)
            {
                NegocioDominio oNegocioDominio = new NegocioDominio();
                ServicioDominio oServicioDominio = new ServicioDominio();
                ClienteDominio oClienteDominio = new ClienteDominio();
                UsuarioDominio oUsuarioDominio = new UsuarioDominio();
                var iniciativa = oIniciativaDominio.ObtenerxCodigo(id.ToString());
                ViewBag.ListaNegocio = oNegocioDominio.listarActivos();
                ViewBag.ListaTipoServicio = oServicioDominio.listarActivos();
                ViewBag.ListaCliente = oClienteDominio.listarActivos();
                ViewBag.ListaResponsableServicio = oUsuarioDominio.ListarxPerfil(new UsuarioEntidad
                {
                    Perfil = new PerfilEntidad
                    {
                        Cod_Perfil = (int)Enumeracion.PerfilUsuario.ResponsableServicio,
                    }
                });
                ViewBag.ListaConsultorLider = oUsuarioDominio.ListarxPerfil(new UsuarioEntidad
                {
                    Perfil = new PerfilEntidad
                    {
                        Cod_Perfil = (int)Enumeracion.PerfilUsuario.ConsultorLider,
                    }
                });
                return View(iniciativa);
            }
            else
            {
                return RedirectToAction("Index", "Iniciativa");
            }
        }


        [HttpPost]
        public ActionResult GrabarRespuestaCliente()
        {
            ResponseWeb<string> oResponseWeb = new ResponseWeb<string>() { Estado = false, };
            #region "Validar Archivo Propuesta Tecnica"

            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase PT = null;
                HttpPostedFileBase RFP = null;
                PT = Request.Files["PT"];
                RFP = Request.Files["RFP"];

                #region "Modificar Iniciativa"

                string pathRFP = "";
                string pathPT = "";
                IniciativaDominio oIniciativaDominio = new IniciativaDominio();
                IniciativaEntidad entidad = new IniciativaEntidad()
                {
                    Cod_Iniciativa = int.Parse(Request.Form["Cod_Iniciativa"].ToString()),
                    Nom_Iniciativa = Request.Form["Nom_Iniciativa"].ToString(),
                    Des_Iniciativa = Request.Form["Des_Iniciativa"].ToString(),
                    Negocio = new NegocioEntidad
                    {
                        Cod_Negocio = int.Parse(Request.Form["Negocio.Cod_Negocio"].ToString()),
                    },
                    Servicio = new ServicioEntidad
                    {
                        Cod_Servicio = int.Parse(Request.Form["Servicio.Cod_Servicio"].ToString()),
                    },
                    Cliente = new ClienteEntidad
                    {
                        Cod_Cliente = int.Parse(Request.Form["Cliente.Cod_Cliente"].ToString()),
                    },
                    RFP = Request.Form["RFP"].ToString(),
                    Estado_Iniciativa = (int)Enumeracion.EstadoFlujo.EsperarRespuestaCliente,
                    ResponsableServicio = new UsuarioEntidad
                    {
                        Cod_Usuario = int.Parse(Request.Form["ResponsableServicio.Cod_Usuario"].ToString()),
                    },
                    ConsultorLider = new UsuarioEntidad
                    {
                        Cod_Usuario = int.Parse(Request.Form["ConsultorLider.Cod_Usuario"].ToString()),
                    },
                    Propuesta_Tecnica = Request.Form["Propuesta_Tecnica"].ToString(),
                    Correo_Propuesta_Tecnica = "",
                    Aceptacion_Propuesta_Tecnica = "",
                    Fecha_Inicio_Servicio = DateTime.Now,
                    Fecha_Fin_Servicio = DateTime.Now
                };


                #region "Validar Archivo Adjunto RFP"
                if (RFP != null)
                {
                    if (string.IsNullOrWhiteSpace(RFP.FileName) == false)
                    {
                        pathRFP = Server.MapPath("~") + entidad.RFP;
                        if (System.IO.File.Exists(pathRFP)) System.IO.File.Delete(pathRFP);
                        string fileRFP = entidad.Nom_Iniciativa.Replace(" ", "").ToLower() + Path.GetExtension(RFP.FileName).ToLower();
                        entidad.RFP = "/RFP/" + fileRFP;
                        pathRFP = Server.MapPath("~") + entidad.RFP;
                    }
                }

                #endregion

                #region "Validar Archivo Adjunto Propuesta Tecnica"
                if (PT != null)
                {
                    if (string.IsNullOrWhiteSpace(PT.FileName) == false)
                    {
                        pathPT = Server.MapPath("~") + entidad.Propuesta_Tecnica;
                        if (System.IO.File.Exists(pathPT)) System.IO.File.Delete(pathPT);
                        string filePT = entidad.Nom_Iniciativa.Replace(" ", "").ToLower() + Path.GetExtension(PT.FileName).ToLower();
                        entidad.Propuesta_Tecnica = "/PT/" + filePT;
                        pathPT = Server.MapPath("~") + entidad.Propuesta_Tecnica;
                    }
                }

                #endregion


                if (oIniciativaDominio.ActualizarEstado(entidad))
                {
                    oResponseWeb.Estado = true;
                    oResponseWeb.Message = "Los cambios fueron guardados";
                    if (string.IsNullOrWhiteSpace(RFP.FileName) == false) { RFP.SaveAs(pathRFP); }
                    if (string.IsNullOrWhiteSpace(PT.FileName) == false) { PT.SaveAs(pathPT); }
                }
                else
                {
                    oResponseWeb.Message = "Los cambios no fueron guardados";
                }

                return Json(oResponseWeb);
                #endregion

            }
            else
            {
                oResponseWeb.Message = "No se cargo la Propuesta Tecnica";
                return Json(oResponseWeb);
            }

            #endregion



        }
        #endregion

        #region "Gestionar Inicio de Servicio"
        [HttpGet]
        public ActionResult GestionarInicioServicio(int id)
        {
            if (id != null && id > 0)
            {
                NegocioDominio oNegocioDominio = new NegocioDominio();
                ServicioDominio oServicioDominio = new ServicioDominio();
                ClienteDominio oClienteDominio = new ClienteDominio();
                UsuarioDominio oUsuarioDominio = new UsuarioDominio();
                var iniciativa = oIniciativaDominio.ObtenerxCodigo(id.ToString());
                ViewBag.ListaNegocio = oNegocioDominio.listarActivos();
                ViewBag.ListaTipoServicio = oServicioDominio.listarActivos();
                ViewBag.ListaCliente = oClienteDominio.listarActivos();
                ViewBag.ListaResponsableServicio = oUsuarioDominio.ListarxPerfil(new UsuarioEntidad
                {
                    Perfil = new PerfilEntidad
                    {
                        Cod_Perfil = (int)Enumeracion.PerfilUsuario.ResponsableServicio,
                    }
                });
                ViewBag.ListaConsultorLider = oUsuarioDominio.ListarxPerfil(new UsuarioEntidad
                {
                    Perfil = new PerfilEntidad
                    {
                        Cod_Perfil = (int)Enumeracion.PerfilUsuario.ConsultorLider,
                    }
                });
                return View(iniciativa);
            }
            else
            {
                return RedirectToAction("Index", "Iniciativa");
            }
        }

        [HttpPost]
        public ActionResult GrabarGestionarInicioServicio()
        {
            ResponseWeb<string> oResponseWeb = new ResponseWeb<string>() { Estado = false, };
            #region "Validar Archivo Propuesta Tecnica"

            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase APT = null;
                HttpPostedFileBase PT = null;
                HttpPostedFileBase RFP = null;
                APT = Request.Files["APT"];
                PT = Request.Files["PT"];
                RFP = Request.Files["RFP"];
                if (APT == null || string.IsNullOrWhiteSpace(APT.FileName))
                {
                    oResponseWeb.Message = "No se cargo la Aceptacion de la Propuesta Técnica";
                    oResponseWeb.Valor = "0";
                    return Json(oResponseWeb);
                }
                #region "Modificar Iniciativa"

                string pathRFP = "";
                string pathPT = "";
                string pathAPT = "";
                string fileAPT = "";
                IniciativaDominio oIniciativaDominio = new IniciativaDominio();
                IniciativaEntidad entidad = new IniciativaEntidad()
                {
                    Cod_Iniciativa = int.Parse(Request.Form["Cod_Iniciativa"].ToString()),
                    Nom_Iniciativa = Request.Form["Nom_Iniciativa"].ToString(),
                    Des_Iniciativa = Request.Form["Des_Iniciativa"].ToString(),
                    Negocio = new NegocioEntidad
                    {
                        Cod_Negocio = int.Parse(Request.Form["Negocio.Cod_Negocio"].ToString()),
                    },
                    Servicio = new ServicioEntidad
                    {
                        Cod_Servicio = int.Parse(Request.Form["Servicio.Cod_Servicio"].ToString()),
                    },
                    Cliente = new ClienteEntidad
                    {
                        Cod_Cliente = int.Parse(Request.Form["Cliente.Cod_Cliente"].ToString()),
                    },
                    RFP = Request.Form["RFP"].ToString(),
                    Estado_Iniciativa = (int)Enumeracion.EstadoFlujo.CerrarOportunidad,
                    ResponsableServicio = new UsuarioEntidad
                    {
                        Cod_Usuario = int.Parse(Request.Form["ResponsableServicio.Cod_Usuario"].ToString()),
                    },
                    ConsultorLider = new UsuarioEntidad
                    {
                        Cod_Usuario = int.Parse(Request.Form["ConsultorLider.Cod_Usuario"].ToString()),
                    },
                    Propuesta_Tecnica = Request.Form["Propuesta_Tecnica"].ToString(),
                    Correo_Propuesta_Tecnica = "",
                    Aceptacion_Propuesta_Tecnica = Request.Form["Aceptacion_Propuesta_Tecnica"].ToString(),
                    Fecha_Inicio_Servicio = DateTime.Parse(Request.Form["FechaInicioServicio"].ToString()),
                    Fecha_Fin_Servicio = DateTime.Parse(Request.Form["FechaFinServicio"].ToString()),
                };

                if (string.IsNullOrWhiteSpace(entidad.Aceptacion_Propuesta_Tecnica) == false)
                {
                    pathAPT = Server.MapPath("~") + entidad.Aceptacion_Propuesta_Tecnica;
                    if (System.IO.File.Exists(pathAPT)) System.IO.File.Delete(pathAPT);
                }
                fileAPT = entidad.Nom_Iniciativa.Replace(" ", "").ToLower() + Path.GetExtension(APT.FileName).ToLower();
                entidad.Aceptacion_Propuesta_Tecnica = "/APT/" + fileAPT;
                pathAPT = Server.MapPath("~") + entidad.Aceptacion_Propuesta_Tecnica;

                #region "Validar Archivo Adjunto RFP"
                if (RFP != null)
                {
                    if (string.IsNullOrWhiteSpace(RFP.FileName) == false)
                    {
                        pathRFP = Server.MapPath("~") + entidad.RFP;
                        if (System.IO.File.Exists(pathRFP)) System.IO.File.Delete(pathRFP);
                        string fileRFP = entidad.Nom_Iniciativa.Replace(" ", "").ToLower() + Path.GetExtension(RFP.FileName).ToLower();
                        entidad.RFP = "/RFP/" + fileRFP;
                        pathRFP = Server.MapPath("~") + entidad.RFP;
                    }
                }

                #endregion

                #region "Validar Archivo Adjunto Propuesta Tecnica"
                if (PT != null)
                {
                    if (string.IsNullOrWhiteSpace(PT.FileName) == false)
                    {
                        pathPT = Server.MapPath("~") + entidad.Propuesta_Tecnica;
                        if (System.IO.File.Exists(pathPT)) System.IO.File.Delete(pathPT);
                        string filePT = entidad.Nom_Iniciativa.Replace(" ", "").ToLower() + Path.GetExtension(PT.FileName).ToLower();
                        entidad.Propuesta_Tecnica = "/PT/" + filePT;
                        pathPT = Server.MapPath("~") + entidad.Propuesta_Tecnica;
                    }
                }

                #endregion


                if (oIniciativaDominio.CerrarOportunidad(entidad))
                {
                    oResponseWeb.Estado = true;
                    oResponseWeb.Message = "Los cambios fueron guardados";
                    if (string.IsNullOrWhiteSpace(RFP.FileName) == false) { RFP.SaveAs(pathRFP); }
                    if (string.IsNullOrWhiteSpace(PT.FileName) == false) { PT.SaveAs(pathPT); }
                    if (string.IsNullOrWhiteSpace(APT.FileName) == false) { APT.SaveAs(pathAPT); }

                }
                else
                {
                    oResponseWeb.Message = "Los cambios no fueron guardados";
                }

                return Json(oResponseWeb);
                #endregion

            }
            else
            {
                oResponseWeb.Message = "No se cargo la Propuesta Tecnica";
                return Json(oResponseWeb);
            }

            #endregion



        }
        #endregion

        #region "Iniciativa Ganada"

        [HttpGet]
        public ActionResult CerrarOportunidad(int id)
        {
            if (id != null && id > 0)
            {
                NegocioDominio oNegocioDominio = new NegocioDominio();
                ServicioDominio oServicioDominio = new ServicioDominio();
                ClienteDominio oClienteDominio = new ClienteDominio();
                UsuarioDominio oUsuarioDominio = new UsuarioDominio();
                var iniciativa = oIniciativaDominio.ObtenerxCodigo(id.ToString());
                ViewBag.ListaNegocio = oNegocioDominio.listarActivos();
                ViewBag.ListaTipoServicio = oServicioDominio.listarActivos();
                ViewBag.ListaCliente = oClienteDominio.listarActivos();
                ViewBag.ListaResponsableServicio = oUsuarioDominio.ListarxPerfil(new UsuarioEntidad
                {
                    Perfil = new PerfilEntidad
                    {
                        Cod_Perfil = (int)Enumeracion.PerfilUsuario.ResponsableServicio,
                    }
                });
                ViewBag.ListaConsultorLider = oUsuarioDominio.ListarxPerfil(new UsuarioEntidad
                {
                    Perfil = new PerfilEntidad
                    {
                        Cod_Perfil = (int)Enumeracion.PerfilUsuario.ConsultorLider,
                    }
                });
                return View(iniciativa);
            }
            else
            {
                return RedirectToAction("Index", "Iniciativa");
            }

        }


        #endregion

        #region "Genericos"
        [HttpGet, FileDownload]
        public FilePathResult DescargarRFP(int id)
        {
            IniciativaDominio oIniciativaDominio = new IniciativaDominio();
            var entidad = oIniciativaDominio.ObtenerxCodigo(id.ToString());
            var extension = entidad.RFP.Split('.');
            return File("~" + entidad.RFP, "application/octet-stream", "RFP." + extension[extension.Length - 1]);
            //return File("~" + entidad.RFP, "application/pdf", "RFP.pdf");
        }
        [HttpGet, FileDownload]
        public FilePathResult DescargarPT(int id)
        {
            IniciativaDominio oIniciativaDominio = new IniciativaDominio();
            var entidad = oIniciativaDominio.ObtenerxCodigo(id.ToString());
            var extension = entidad.Propuesta_Tecnica.Split('.');
            return File("~" + entidad.Propuesta_Tecnica, "application/octet-stream", "PropuestaTecnica." + extension[extension.Length - 1]);
        }
        [HttpGet, FileDownload]
        public FilePathResult DescargarCPT(int id)
        {
            IniciativaDominio oIniciativaDominio = new IniciativaDominio();
            var entidad = oIniciativaDominio.ObtenerxCodigo(id.ToString());
            var extension = entidad.Correo_Propuesta_Tecnica.Split('.');
            return File("~" + entidad.Correo_Propuesta_Tecnica, "application/octet-stream", "CorreoPropuestaTecnica." + extension[extension.Length - 1]);
        }
        [HttpGet, FileDownload]
        public FilePathResult DescargarAPT(int id)
        {
            IniciativaDominio oIniciativaDominio = new IniciativaDominio();
            var entidad = oIniciativaDominio.ObtenerxCodigo(id.ToString());
            var extension = entidad.Aceptacion_Propuesta_Tecnica.Split('.');
            return File("~" + entidad.Aceptacion_Propuesta_Tecnica, "application/octet-stream", "AceptacionPropuestaTecnica." + extension[extension.Length - 1]);
        }

        #endregion









    }
}