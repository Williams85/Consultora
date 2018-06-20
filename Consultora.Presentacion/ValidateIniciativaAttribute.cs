using Consultora.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Consultora.Presentacion
{
    public class ValidateIniciativaAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionParameters.ContainsKey("id"))
            {

                var id = filterContext.ActionParameters["id"] as Int32?;
                IniciativaDominio oIniciativaDominio = new IniciativaDominio();
                var entidad = oIniciativaDominio.ObtenerxCodigo(id.ToString());
                #region "Validar Estado"
                switch (entidad.Estado_Iniciativa)
                {
                    case 1:
                        filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {
                                { "Controller", "Iniciativa" },
                                { "Action", "AsignarResposnableServicio" },
                        });
                        break;
                    case 2:
                        filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {
                                { "Controller", "Iniciativa" },
                                { "Action", "RevisarRFP" },
                        });
                        break;
                    case 3:
                        filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {
                                { "Controller", "Iniciativa" },
                                { "Action", "AsignarConsultorLider" },
                        });
                        break;
                    case 4:
                        filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {
                                { "Controller", "Iniciativa" },
                                { "Action", "EstimarTiemposProyecto" },
                        });
                        break;
                    case 5:
                        filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {
                                { "Controller", "Iniciativa" },
                                { "Action", "EstimarConsultoresProyecto" },
                        });
                        break;
                    case 6:
                        filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {
                                { "Controller", "Iniciativa" },
                                { "Action", "EvaluarRentabilidad" },
                        });
                        break;
                }

                #endregion
            }
        }
    }
}