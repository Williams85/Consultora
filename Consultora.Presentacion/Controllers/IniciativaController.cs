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
        // GET: /Iniciativa/
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
                #region "Validar Estado"
                if (iniciativa.Estado_Iniciativa != 1)
                {
                    switch (iniciativa.Estado_Iniciativa)
                    {
                        case 2:
                            SessionManager.MensajeErrRFP = "";
                            SessionManager.MensajeOKRFP = "";
                            return RedirectToAction("RevisarRFP", "Iniciativa", new { id = iniciativa.Cod_Iniciativa });
                        case 3:
                            SessionManager.MensajeErrACL = "";
                            SessionManager.MensajeOKACL = "";
                            return RedirectToAction("AsignarConsultorLider", "Iniciativa", new { id = iniciativa.Cod_Iniciativa });
                        default:
                            return RedirectToAction("Index", "Iniciativa");
                    }
                }
                #endregion
                return View(iniciativa);
            }
            else
            {
                return RedirectToAction("Index", "Iniciativa");
            }

        }

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
                #region "Validar Estado"
                if (iniciativa.Estado_Iniciativa != 2)
                {
                    switch (iniciativa.Estado_Iniciativa)
                    {
                        case 1:
                            SessionManager.MensajeErrARS = "";
                            SessionManager.MensajeOKARS = "";
                            return RedirectToAction("AsignarResposnableServicio", "Iniciativa", new { id = iniciativa.Cod_Iniciativa });
                        case 3:
                            SessionManager.MensajeErrACL = "";
                            SessionManager.MensajeOKACL = "";
                            return RedirectToAction("AsignarConsultorLider", "Iniciativa", new { id = iniciativa.Cod_Iniciativa });
                        default:
                            return RedirectToAction("Index", "Iniciativa");
                    }
                }
                #endregion
                return View(iniciativa);
            }
            else
            {
                return RedirectToAction("Index", "Iniciativa");
            }
        }

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
                #region "Validar Estado"
                if (iniciativa.Estado_Iniciativa != 3)
                {
                    switch (iniciativa.Estado_Iniciativa)
                    {
                        case 1:
                            SessionManager.MensajeErrARS = "";
                            SessionManager.MensajeOKARS = "";
                            return RedirectToAction("AsignarResposnableServicio", "Iniciativa", new { id = iniciativa.Cod_Iniciativa });
                        case 2:
                            SessionManager.MensajeErrRFP = "";
                            SessionManager.MensajeOKRFP = "";
                            return RedirectToAction("RevisarRFP", "Iniciativa", new { id = iniciativa.Cod_Iniciativa });
                        default:
                            return RedirectToAction("Index", "Iniciativa");
                    }
                }
                #endregion
                return View(iniciativa);
            }
            else
            {
                return RedirectToAction("Index", "Iniciativa");
            }

        }

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

        [HttpGet]
        public ActionResult EstimarConsultoresProyecto(int id)
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

        [HttpGet]
        public ActionResult EvaluarRentabilidad(int id)
        {
            if (id != null && id > 0)
            {
                id = 1;
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
        public ActionResult GrabarOportunidad()
        {
            int Cod_Iniciativa = 0;
            ResponseWeb<string> oResponseWeb = new ResponseWeb<string>() { Estado = false, };
            #region "Validar Archivo Adjunto"
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase RFP = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    RFP = Request.Files[i];
                    if (string.IsNullOrWhiteSpace(RFP.FileName))
                    {
                        oResponseWeb.Message = "No se cargo el documento RFP";
                        return Json(oResponseWeb);
                    }
                }
                #region "Grabar Iniciativa"

                IniciativaEntidad entidad = new IniciativaEntidad()
                {
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
                    oResponseWeb.Message = "Se creo la oportunidad";
                    var entity = oIniciativaDominio.ObtenerxCodigo(Cod_Iniciativa.ToString());
                    oResponseWeb.Valor = entity.Cod_Oportunidad + "|" + Cod_Iniciativa.ToString();
                    RFP.SaveAs(pathRFP);
                }
                else
                {
                    oResponseWeb.Estado = false;
                    oResponseWeb.Message = "No se creo la oportunidad";
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
                }
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
                oResponseWeb.Message = "Se asignó el responsable del servicio";
                if (string.IsNullOrWhiteSpace(RFP.FileName) == false) { RFP.SaveAs(pathRFP); }
            }
            else
            {
                oResponseWeb.Message = "No se asignó el responsable del servicio";
            }

            return Json(oResponseWeb);
            #endregion
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
                }
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
                oResponseWeb.Message = "Se reviso el documento RFP";
                if (string.IsNullOrWhiteSpace(RFP.FileName) == false) { RFP.SaveAs(pathRFP); }
            }
            else
            {
                oResponseWeb.Message = "No se reviso el documento RFP";
            }

            return Json(oResponseWeb);
            #endregion
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
                }
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
                oResponseWeb.Message = "Se asignó el consultor lider";
                if (string.IsNullOrWhiteSpace(RFP.FileName) == false) { RFP.SaveAs(pathRFP); }
            }
            else
            {
                oResponseWeb.Message = "No se asignó el consultor lider";
            }

            return Json(oResponseWeb);
            #endregion
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
                }
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
                oResponseWeb.Message = "Se estimo el tiempo del proyecto";
                if (string.IsNullOrWhiteSpace(RFP.FileName) == false) { RFP.SaveAs(pathRFP); }
            }
            else
            {
                oResponseWeb.Message = "No se estimo el tiempo del proyecto";
            }

            return Json(oResponseWeb);
            #endregion
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
                }
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
                oResponseWeb.Message = "Se estimo los consultores del proyecto";
                if (string.IsNullOrWhiteSpace(RFP.FileName) == false) { RFP.SaveAs(pathRFP); }
            }
            else
            {
                oResponseWeb.Message = "No se estimo los consultores del proyecto";
            }

            return Json(oResponseWeb);
            #endregion
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
                }
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
                oResponseWeb.Message = "Se evaluo la rentabilidad";
                if (string.IsNullOrWhiteSpace(RFP.FileName) == false) { RFP.SaveAs(pathRFP); }
            }
            else
            {
                oResponseWeb.Message = "No se evaluo la rentabilidad";
            }

            return Json(oResponseWeb);
            #endregion
        }

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
            if( oRequerimientoDominio.Eliminar(entidad.Cod_Requerimiento.ToString()))
            {
                oResponseWeb.Estado = true;
                oResponseWeb.Message = "Se elimino el requerimiento";
            }
            else
            {
                oResponseWeb.Estado = false;
                oResponseWeb.Message = "No se elimino el requerimiento";
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
                oResponseWeb.Message = "Se elimino la actividad del requerimiento";
            }
            else
            {
                oResponseWeb.Estado = false;
                oResponseWeb.Message = "No se elimino la actividad del requerimiento";
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
            return PartialView("_ResultadoTiempoFases", Lista);
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