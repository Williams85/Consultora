using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Entidad
{
    public partial class ServicioEmpresarialCompetenciaEntidad
    {
        public int Cod_Servicio_Empresarial_Competencias { get; set; }
        public ServicioEmpresarialEntidad ServicioEmpresarial { get; set; }
        public CompetenciaEntidad Competencia { get; set; }
        public NivelCompetenciaEntidad NivelCompetencia { get; set; }
        public ConsultorEntidad Consultor { get; set; }
    }
}
