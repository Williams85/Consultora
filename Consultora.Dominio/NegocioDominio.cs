using Consultora.Entidad;
using Consultora.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Dominio
{
    public class NegocioDominio
    {
        NegocioRepositorio oNegocioRepositorio = new NegocioRepositorio();

        #region "Metodos No Transaccionales"
        public List<NegocioEntidad> listarActivos()
        {
            return oNegocioRepositorio.listarActivos();
        }

        #endregion
    }
}
