using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Entidad
{
    public partial class IniciativaCompetenciaEntidad
    {
        public int Cod_Iniciativa_Competencia { get; set; }
        public IniciativaEntidad Iniciativa { get; set; }
        public CompetenciaEntidad Competencia { get; set; }
        public NivelCompetenciaEntidad NivelCompetencia { get; set; }
        public NegocioEntidad Negocio { get; set; }
        public double Porcentaje_Participacion { get; set; }
        public double Horas_Participacion { get; set; }

    }
}
