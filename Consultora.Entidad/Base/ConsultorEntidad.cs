using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Entidad
{
    public partial class ConsultorEntidad
    {
        public int Cod_Consultor { get; set; }
        public EmpleadoEntidad Empleado { get; set; }
        public bool? Disponible { get; set; }
        public bool Estado { get; set; }
    }
}
