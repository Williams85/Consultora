using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Entidad
{
    public partial class ServicioEmpresarialEntidad
    {
        public string FechaInicio { get { return this.Fecha_Inicio.ToString("dd/MM/yyyy"); } }
        public string FechaFin { get { return this.Fecha_Fin.ToString("dd/MM/yyyy"); } }
    }
}
