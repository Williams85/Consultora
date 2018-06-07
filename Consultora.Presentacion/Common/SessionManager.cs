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
        const string _ListaConsultoresFiltrar = "ListaConsultoresFiltrar";
        const string _ListaCompetencia = "ListaCompetencia";
        const string _ListaNivelCompetencia = "ListaNivelCompetencia";
        const string _IniciativaEntidad = "IniciativaEntidad";
        const string _MensajeErrARS = "MensajeErrARS";
        const string _MensajeOKARS = "MensajeOKARS";
        const string _MensajeErrRFP = "MensajeErrRFP";
        const string _MensajeOKRFP = "MensajeOKRFP";
        const string _MensajeErrACL = "MensajeErrACL";
        const string _MensajeOKACL = "MensajeOKACL";
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
        public static List<ConsultorEntidad> ListaConsultoresFiltrar
        {
            get { return (List<ConsultorEntidad>)Session[_ListaConsultoresFiltrar]; }
            set { Session[_ListaConsultoresFiltrar] = value; }
        }
        public static List<CompetenciaEntidad> ListaCompetencia
        {
            get { return (List<CompetenciaEntidad>)Session[_ListaCompetencia]; }
            set { Session[_ListaCompetencia] = value; }
        }
        public static List<NivelCompetenciaEntidad> ListaNivelCompetencia
        {
            get { return (List<NivelCompetenciaEntidad>)Session[_ListaNivelCompetencia]; }
            set { Session[_ListaNivelCompetencia] = value; }
        }
        public static IniciativaEntidad IniciativaEntidad
        {
            get { return (IniciativaEntidad)Session[_IniciativaEntidad]; }
            set { Session[_IniciativaEntidad] = value; }
        }
        public static string MensajeErrARS
        {
            get { return (string)Session[_MensajeErrARS]; }
            set { Session[_MensajeErrARS] = value; }
        }
        public static string MensajeOKARS
        {
            get { return (string)Session[_MensajeOKARS]; }
            set { Session[_MensajeOKARS] = value; }
        }
        public static string MensajeErrRFP
        {
            get { return (string)Session[_MensajeErrRFP]; }
            set { Session[_MensajeErrRFP] = value; }
        }
        public static string MensajeOKRFP
        {
            get { return (string)Session[_MensajeOKRFP]; }
            set { Session[_MensajeOKRFP] = value; }
        }
        public static string MensajeErrACL
        {
            get { return (string)Session[_MensajeErrACL]; }
            set { Session[_MensajeErrACL] = value; }
        }
        public static string MensajeOKACL
        {
            get { return (string)Session[_MensajeOKACL]; }
            set { Session[_MensajeOKACL] = value; }
        }

        #endregion
    }
}