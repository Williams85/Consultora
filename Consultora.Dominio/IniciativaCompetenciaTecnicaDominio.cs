using Consultora.Entidad;
using Consultora.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Dominio
{
    public class IniciativaCompetenciaTecnicaDominio
    {
        IniciativaCompetenciaTecnicaRepositorio oIniciativaCompetenciaTecnicaRepositorio = new IniciativaCompetenciaTecnicaRepositorio();

        #region "Metodos No Transaccionales"

        public List<IniciativaCompetenciaTecnicaEntidad> listarxIniciativaCompetencia(IniciativaCompetenciaTecnicaEntidad entidad)
        {
            return oIniciativaCompetenciaTecnicaRepositorio.listarxIniciativaCompetencia(entidad);
        }


        #endregion

        #region "Metodos Transaccionales"

        public bool Grabar(IniciativaCompetenciaTecnicaEntidad entidad)
        {
            return oIniciativaCompetenciaTecnicaRepositorio.Grabar(entidad);
        }

        public bool Eliminar(string Codigo)
        {
            return oIniciativaCompetenciaTecnicaRepositorio.Eliminar(Codigo);
        }

        #endregion
    }
}
