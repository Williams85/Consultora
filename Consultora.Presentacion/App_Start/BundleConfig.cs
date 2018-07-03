using System.Web;
using System.Web.Optimization;

namespace Consultora.Presentacion
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.ui.custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      //"~/Scripts/excanvas.min.js",
                      ////"~/Scripts/jquery.flot.min.js",
                      ////"~/Scripts/jquery.flot.resize.min.js",
                      //"~/Scripts/jquery.peity.min.js",
                      //"~/Scripts/fullcalendar.min.js",
                      ////"~/Scripts/matrix.js",
                      ////"~/Scripts/matrix.dashboard.js",
                      //"~/Scripts/jquery.gritter.min.js",
                      //"~/Scripts/matrix.interface.js",
                      //"~/Scripts/matrix.chat.js",
                      ////"~/Scripts/matrix.form_validation.js",
                      //"~/Scripts/jquery.wizard.js",
                      ////"~/Scripts/jquery.uniform.js",
                      //"~/Scripts/select2.min.js",
                      //"~/Scripts/matrix.popover.js",
                      //"~/Scripts/jquery.dataTables.min.js",
                      //"~/Scripts/matrix.tables.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      /*"~/Content/bootstrap-responsive.min.css",
                      "~/Content/fullcalendar.css",
                      "~/Content/matrix-style.css",
                      "~/Content/matrix-media.css",
                      "~/font-awesome/css/font-awesome.css",
                      "~/Content/jquery.gritter.css",
                      "~/Content/jquery.gritter.css",*/
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/script-layaout").Include(
                      "~/Scripts/Controladores/functions.js",
                      "~/Scripts/Controladores/ajax-mvc.js"));

            //Asignacion Automatica
            bundles.Add(new ScriptBundle("~/bundles/asignacion-automatica").Include(
                                  "~/Scripts/Controladores/AsignacionAutomatica/asignacion-automatica-controller.js"));

            //Evaluacion Rentabilidad
            bundles.Add(new ScriptBundle("~/bundles/evaluacion-rentabilidad").Include(
                                  "~/Scripts/Controladores/EvaluacionRentabilidad/evaluacion-rentabilidad-controller.js"));

            //Oportunidad
            bundles.Add(new ScriptBundle("~/bundles/oportunidad").Include(
                                  "~/Scripts/Controladores/Iniciativa/iniciativa-controller.js"));

            //Aprobacion de Asignacion Automatica
            bundles.Add(new ScriptBundle("~/bundles/aprobacion-asignacion-automatica").Include(
                                  "~/Scripts/Controladores/AprobacionAsignacionAutomatica/aprobacion-asignacion-automatica-controller.js"));


        }
    }
}
