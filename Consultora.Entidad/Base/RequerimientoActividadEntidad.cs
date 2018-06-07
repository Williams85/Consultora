using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Entidad
{
    public class RequerimientoActividadEntidad
    {
        public int Cod_Requerimiento_Actividad { get; set; }
        public RequerimientoEntidad Requerimiento { get; set; }
        public ActividadEntidad Actividad { get; set; }
        public ComplejidadActividadEntidad ComplejidadActividad { get; set; }
        public byte Cantidad { get; set; }
        public decimal Tiempo_Estimado { get; set; }
    }
}
