using Consultora.Entidad;
using Consultora.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Dominio
{
    public class NivelCompetenciaDominio
    {
        NivelCompetenciaRepositorio oNivelCompetenciaRepositorio = new NivelCompetenciaRepositorio();

        #region "Metodos No Transaccionales"
        public List<NivelCompetenciaEntidad> listarActivos()
        {
            return oNivelCompetenciaRepositorio.listarActivos();
        }

        #endregion
    }
}
