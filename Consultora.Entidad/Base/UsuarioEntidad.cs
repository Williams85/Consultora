using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Entidad
{
    public class UsuarioEntidad
    {
        public int Cod_Usuario { get; set; }
        public string Nom_Usuario { get; set; }
        public string Pas_Usuario { get; set; }
        public EmpleadoEntidad Empleado { get; set; }
        public string NombreEmpleado { get { return this.Empleado.Nom_Empleado + " " + this.Empleado.AP_Empleado + " " + this.Empleado.AM_Empleado; } }
        public bool Estado { get; set; }
    }
}
