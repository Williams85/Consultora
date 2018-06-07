using Consultora.Entidad;
using Consultora.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Dominio
{
    public class IniciativaFaseDominio
    {
        IniciativaFaseRepositorio oIniciativaFaseRepositorio = new IniciativaFaseRepositorio();

        #region "Metodos No Transaccionales"

        public List<IniciativaFaseEntidad> listarxIniciativa(string Codigo)
        {
            return oIniciativaFaseRepositorio.listarxIniciativa(Codigo);
        }

        #endregion

        #region "Metodos Transaccionales"

        public bool GenerarTiempoFase(string Codigo)
        {
            if (oIniciativaFaseRepositorio.existe(Codigo))
                return oIniciativaFaseRepositorio.Modificar(Codigo);
            else
                return oIniciativaFaseRepositorio.Grabar(Codigo);
        }

        #endregion
    }
}
