using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Entidad
{
    public class IniciativaCompetenciaTecnicaEntidad
    {
        public int Cod_Iniciativa_Competencia_Tecnica { get; set; }
        public IniciativaCompetenciaEntidad IniciativaCompetencia { get; set; }
        public CompetenciaTecnicaEntidad CompetenciaTecnica { get; set; }
    }
}
