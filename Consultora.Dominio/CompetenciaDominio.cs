using Consultora.Entidad;
using Consultora.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Dominio
{
    public class CompetenciaDominio
    {
        private CompetenciaRepositorio oCompetenciaRepositorio = new CompetenciaRepositorio();

        #region "Metodos No Transaccionales"
        public List<CompetenciaEntidad> listarActivos()
        {
            return oCompetenciaRepositorio.listarActivos();
        }

        #endregion
    }
}
