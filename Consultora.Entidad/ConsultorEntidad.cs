using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Entidad
{
    public partial class ConsultorEntidad
    {
        public string NombreCompleto { get {return this.Empleado.Nom_Empleado + " " + this.Empleado.AP_Empleado + " " + this.Empleado.AM_Empleado; } }
    }
}
