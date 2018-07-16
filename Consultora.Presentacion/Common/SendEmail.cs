
using Consultora.Entidad;
using Consultora.Utilitario;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
namespace Consultora.Presentacion
{
    public class SendEmail
    {
        public static bool NotificacionEstimacionHoras(IniciativaEntidad entidad)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Estimado/a:");
            sb.AppendLine("<br/><br/>");
            sb.AppendLine("Para informar que se culminó con la estimación de las horas del servicio.<br/><br/>");
            sb.AppendLine("<br/>");
            sb.AppendLine("     * Nombre: " + entidad.Nom_Iniciativa + "<br/>");
            sb.AppendLine("     * Descripción:" + entidad.Des_Iniciativa + "<br/>");
            sb.AppendLine("     * Cliente: " + entidad.Cliente.Razon_Social + "<br/>");
            sb.AppendLine("     * Unidad Negocio: " + entidad.Negocio.Nom_Negocio + "<br/>");
            sb.AppendLine("     * Tipo Servicio: " + entidad.Servicio.Nom_Servicio + "<br/>");
            sb.AppendLine("<br/><br/>");
            sb.AppendLine("Saludos cordiales");

            return EnviarCorreo(entidad.ConsultorLider.Empleado.Ema_Empleado, "Estimación de Horas del Proyecto", sb.ToString());
        }
        public static bool NotificacionAsignacionConsultorLider(IniciativaEntidad entidad)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Estimado/a:");
            sb.AppendLine("<br/><br/>");
            sb.AppendLine("Al hacer clic en Siguiente en el estado Asignar Consultor Líder. <br/>Se envía una notificación al consultor líder asignado.<br/><br/>");
            sb.AppendLine("<br/>");
            sb.AppendLine("     * Nombre: " + entidad.Nom_Iniciativa + "<br/>");
            sb.AppendLine("     * Descripción:" + entidad.Des_Iniciativa + "<br/>");
            sb.AppendLine("     * Cliente: " + entidad.Cliente.Razon_Social + "<br/>");
            sb.AppendLine("     * Unidad Negocio: " + entidad.Negocio.Nom_Negocio + "<br/>");
            sb.AppendLine("     * Tipo Servicio: " + entidad.Servicio.Nom_Servicio + "<br/>");
            sb.AppendLine("<br/><br/>");
            sb.AppendLine("Saludos cordiales");

            return EnviarCorreo(entidad.ConsultorLider.Empleado.Ema_Empleado, "Asignaciòn de Consultor Lider", sb.ToString());
        }
        public static bool NotificacionAsignacionResponsableServicio(IniciativaEntidad entidad)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Estimado/a:");
            sb.AppendLine("<br/><br/>");
            sb.AppendLine("Se le ha asignado como Responsable del Servicio. <br/>Por favor, ingrese al portal para continuar con el flujo de la oportunidad.<br/><br/>");
            sb.AppendLine("<br/>");
            sb.AppendLine("     * Nombre: " + entidad.Nom_Iniciativa + "<br/>");
            sb.AppendLine("     * Descripción:" + entidad.Des_Iniciativa + "<br/>");
            sb.AppendLine("     * Cliente: " + entidad.Cliente.Razon_Social + "<br/>");
            sb.AppendLine("     * Unidad Negocio: " + entidad.Negocio.Nom_Negocio + "<br/>");
            sb.AppendLine("     * Tipo Servicio: " + entidad.Servicio.Nom_Servicio + "<br/>");
            sb.AppendLine("<br/><br/>");
            sb.AppendLine("Saludos cordiales");

            return EnviarCorreo(entidad.ResponsableServicio.Empleado.Ema_Empleado, "Asignación de Responsable de Servicio", sb.ToString());
        }
        public static bool NotificacionCreacionOportunidad(string ToEmail, IniciativaEntidad entidad)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Estimado/a:");
            sb.AppendLine("<br/><br/>");
            sb.AppendLine("Se ha registrado una nueva oportunidad en el sistema de gestión de servicios. <br/>Por favor, ingrese al portal para continuar con el flujo.<br/><br/>");
            sb.AppendLine("<br/>");
            sb.AppendLine("     * Nombre: " + entidad.Nom_Iniciativa + "<br/>");
            sb.AppendLine("     * Descripción:" + entidad.Des_Iniciativa + "<br/>");
            sb.AppendLine("     * Cliente: " + entidad.Cliente.Razon_Social + "<br/>");
            sb.AppendLine("     * Unidad Negocio: " + entidad.Negocio.Nom_Negocio + "<br/>");
            sb.AppendLine("     * Tipo Servicio: " + entidad.Servicio.Nom_Servicio + "<br/>");
            sb.AppendLine("<br/><br/>");
            sb.AppendLine("Saludos cordiales");

            return EnviarCorreo(ToEmail, "Registro de Oportunidad", sb.ToString());
        }
        public static bool NotificacionAsignacionConsultores(string ToEmail, ServicioEmpresarialEntidad entidad)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Estimado/a:");
            sb.AppendLine("<br/><br/>");
            sb.AppendLine("Se ha registrado una nueva solicitud para aprobar los consultores de un servicio. <br/>Por favor, ingrese al portal para revisar la propuesta de consultores.<br/><br/>");
            sb.AppendLine("<br/>");
            sb.AppendLine("     * Nombre: " + entidad.Nom_Servicio_Empresarial + "<br/>");
            sb.AppendLine("     * Descripción:" + entidad.Descripcion_Servicio_Empresarial + "<br/>");
            sb.AppendLine("     * Cliente: " + entidad.Cliente.Razon_Social + "<br/>");
            sb.AppendLine("     * Unidad Negocio: " + entidad.Negocio.Nom_Negocio + "<br/>");
            sb.AppendLine("     * Tipo Servicio: " + entidad.Servicio.Nom_Servicio + "<br/>");
            sb.AppendLine("<br/><br/>");
            sb.AppendLine("Saludos cordiales");

            return EnviarCorreo(ToEmail, "Asignacion de Consultores", sb.ToString());
        }
        public static bool NotificacionAprobacionAsignacion(ServicioEmpresarialEntidad entidad, List<ServicioEmpresarialCompetenciaEntidad> ListaConsultores)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Estimado/a:");
            sb.AppendLine("<br/><br/>");
            sb.AppendLine("Ha sido asignado a un servicio. El responsable del servicio se contactará con usted para darle los detalles.<br/><br/>");
            sb.AppendLine("<br/>");
            sb.AppendLine("     * Nombre: " + entidad.Nom_Servicio_Empresarial + "<br/>");
            sb.AppendLine("     * Descripción:" + entidad.Descripcion_Servicio_Empresarial + "<br/>");
            sb.AppendLine("     * Cliente: " + entidad.Cliente.Razon_Social + "<br/>");
            sb.AppendLine("     * Unidad Negocio: " + entidad.Negocio.Nom_Negocio + "<br/>");
            sb.AppendLine("     * Tipo Servicio: " + entidad.Servicio.Nom_Servicio + "<br/>");
            sb.AppendLine("<br/><br/>");
            sb.AppendLine("Saludos cordiales");

            var estado = EnviarCorreo(entidad.ResponsableServicio.Empleado.Ema_Empleado, "Aprobación de Asignación de Consultores", sb.ToString());

            foreach (var item in ListaConsultores)
            {
                estado = EnviarCorreo(item.Consultor.Empleado.Ema_Empleado, "Aprobación de Asignación de Consultores", sb.ToString());
            }
            return estado;
        }
        private static bool EnviarCorreo(string ToEmail, string Asunto, string Contenido)
        {

            try
            {
                System.Net.ServicePointManager.Expect100Continue = false;
                var message = new MailMessage();
                message.To.Add(new MailAddress(ToEmail));  // replace with valid value 
                message.From = new MailAddress(AppSettings.valueString("FromEmail"));  // replace with valid value
                message.Subject = Asunto;
                message.Body = Contenido;
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = AppSettings.valueString("UserEmail"),
                        Password = AppSettings.valueString("PasswordEmail")
                    };
                    smtp.Credentials = credential;
                    smtp.Host = AppSettings.valueString("ServerEmail");
                    smtp.Port = AppSettings.valueInt("PortEmail");
                    smtp.EnableSsl = AppSettings.valueBool("SSLMail");
                    smtp.Send(message);
                    return true;
                }
            }
            catch (Exception ex)
            {
                //Log.Instance(typeof(Functions)).Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
                throw;
            }
        }
    }
}