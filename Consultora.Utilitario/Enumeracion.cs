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
            CerrarOportunidad = 10,//Estado Cerrar Oportunidad
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
