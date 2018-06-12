using Consultora.Entidad;
using Consultora.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Dominio
{
    public class CompetenciaTecnicaDominio
    {
        CompetenciaTecnicaRepositorio oCompetenciaTecnicaRepositorio = new CompetenciaTecnicaRepositorio();

        #region "Metodos No Transaccionales"
        public List<CompetenciaTecnicaEntidad> filtrar(IniciativaCompetenciaTecnicaEntidad entidad)
        {
            return oCompetenciaTecnicaRepositorio.filtrar(entidad);
        }

        #endregion
    }
}
