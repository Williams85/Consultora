using Consultora.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Consultora.Presentacion
{
    public class SessionManager
    {
        public static HttpSessionState Session
        {
            get { return HttpContext.Current.Session; }
        }

        #region "Atributos"
        const string _Usuario = "Usuario";
        const string _ListaConsultoresAsignados = "ListaConsultoresAsignados";
        #endregion

        #region "Propiedades"
        public static UsuarioEntidad Usuario
        {
            get { return (UsuarioEntidad)Session[_Usuario]; }
            set { Session[_Usuario] = value; }
        }
        public static List<ServicioEmpresarialCompetenciaEntidad> ListaConsultoresAsignados
        {
            get { return (List<ServicioEmpresarialCompetenciaEntidad>)Session[_ListaConsultoresAsignados]; }
            set { Session[_ListaConsultoresAsignados] = value; }
        }

        #endregion
    }
}