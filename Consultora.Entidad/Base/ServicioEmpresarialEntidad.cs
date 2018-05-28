using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Entidad
{
     public partial class ServicioEmpresarialEntidad
    {
        public int Cod_Servicio_Empresarial { get; set; }
        public string Nom_Servicio_Empresarial { get; set; }
        public byte Num_Consultores { get; set; }
        public DateTime Fecha_Inicio { get; set; }
        public DateTime Fecha_Fin { get; set; }
        public NegocioEntidad Negocio { get; set; }
        public ServicioEntidad Servicio { get; set; }
        public ClienteEntidad Cliente { get; set; }
        public EmpleadoEntidad Empleado { get; set; }
        public bool Estado { get; set; }

        public string Responsable { get { return this.Empleado.Nom_Empleado + "" + this.Empleado.AP_Empleado + "" + this.Empleado.AM_Empleado; } }

    }
}
