using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Entidad
{
    public class RequerimientoEntidad
    {
        public int Cod_Requerimiento { get; set; }
        public string Nom_Requerimiento { get; set; }
        public IniciativaEntidad Iniciativa { get; set; }
        public ComplejidadRequerimientoEntidad ComplejidadRequerimiento { get; set; }
    }
}
