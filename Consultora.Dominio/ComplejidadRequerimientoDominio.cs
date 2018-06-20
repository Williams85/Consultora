using Consultora.Entidad;
using Consultora.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Dominio
{
    public class ComplejidadRequerimientoDominio
    {
        ComplejidadRequerimientoRepositorio oComplejidadRequerimientoRepositorio = new ComplejidadRequerimientoRepositorio();
        #region "Metodos No Transaccionales"
        public List<ComplejidadRequerimientoEntidad> listarActivos()
        {
            return oComplejidadRequerimientoRepositorio.listarActivos();
        }

        #endregion
    }
}
