using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Utilitario
{
    public class Enumeracion
    {
        public enum EstadoFlujo
        {
            NuevaOportunidad = 1,//Estado de Nueva Oportunidad.
            AsignarResponsableServicio = 2,//Estado Asignar Responsable de Servicio
            RevisarRFP = 3,//Estado Revisar RFP
            AsignarConsultorLider = 4,//Estado Asignar Consultor Lider
            EstimarTiempoProyecto = 5,//Estado Estimar Tiempo Proyecto
            EstimarConsultores = 6,//Estado Estimar Consultores
            EvaluacionRentabilidad = 7,//Estado Asignar Consultor Lider
            DesarrollarPropuestaTecnica = 8,//Estado Desarrollar Propuesta Tecnica
            RevisarPropuestaTecnica = 9,//Estado Revisar Propuesta Tecnica
            EsperarRespuestaCliente = 10,//Estado Esperar Respuesta de Cliente
            CerrarOportunidad = 11,//Estado Gestionar Inicio de Servicio
            CancelarOportunidad = 0,//Estado Cancelar Oportunidad
        }
        public enum PerfilUsuario
        {
            GestorComercial = 1,//Gestor Comercial
            ResponsableServicio = 2,//Responsable Servicio
            ConsultorLider = 3,//Consultor Lider
            GerenteOperaciones = 4,//Gerente Operaciones
        }

    }
}
