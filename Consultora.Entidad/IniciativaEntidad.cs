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
    }
}
