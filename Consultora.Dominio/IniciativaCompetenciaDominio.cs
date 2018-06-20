using Consultora.Entidad;
using Consultora.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Dominio
{
    public class IniciativaCompetenciaDominio
    {
        IniciativaCompetenciaRepositorio oIniciativaCompetenciaRepositorio = new IniciativaCompetenciaRepositorio();
        #region "Metodos No Transaccionales"

        public List<IniciativaCompetenciaEntidad> listarxIniciativa(IniciativaCompetenciaEntidad entidad)
        {
            return oIniciativaCompetenciaRepositorio.listarxIniciativa(entidad);
        }


        #endregion

        #region "Metodos Transaccionales"

        public bool Grabar(IniciativaCompetenciaEntidad entidad)
        {
            return oIniciativaCompetenciaRepositorio.Grabar(entidad); ;
        }

        public bool Modificar(IniciativaCompetenciaEntidad entidad)
        {
            return oIniciativaCompetenciaRepositorio.Modificar(entidad);
        }

        public bool Eliminar(string Codigo)
        {
            return oIniciativaCompetenciaRepositorio.Eliminar(Codigo);
        }

        #endregion
    }
}
