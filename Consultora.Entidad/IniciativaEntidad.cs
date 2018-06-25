using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Entidad
{
    public partial class IniciativaEntidad
    {
        public string FechaRegistro { get { return this.Fecha_Registro.ToString("dd/MM/yyyy"); } }
        public string FechaInicioServicio { get { return this.Fecha_Inicio_Servicio.ToString("dd/MM/yyyy"); } }
        public string FechaFinServicio { get { return this.Fecha_Fin_Servicio.ToString("dd/MM/yyyy"); } }
        public string EstadoIniciativa
        {
            get
            {
                string estado = "";
                switch (this.Estado_Iniciativa)
                {
                    case 1:
                        estado = "Asignación Responsable de Servicio";
                        break;
                    case 2:
                        estado = "Revisar RFP";
                        break;
                    case 3:
                        estado = "Asignación Consultor Lider";
                        break;
                    case 4:
                        estado = "Estimación Tiempo de Proyecto";
                        break;
                    case 5:
                        estado = "Estimación Consultores de Proyecto";
                        break;
                    case 6:
                        estado = "Evaluación Rentabilidad";
                        break;
                    case 7:
                        estado = "Desarrollar Propuesta Técnica";
                        break;
                    case 8:
                        estado = "Revisar Propuesta Técnica";
                        break;
                    case 9:
                        estado = "Esperando Respuesta Cliente";
                        break;
                    case 10:
                        estado = "Gestionar Inicio Servicio";
                        break;
                    case 11:
                        estado = "Oportunidad Ganada";
                        break;
                }
                return estado;

            }
        }

    }
}
