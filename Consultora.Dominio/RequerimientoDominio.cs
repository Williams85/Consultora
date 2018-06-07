using Consultora.Entidad;
using Consultora.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Dominio
{
    public class RequerimientoDominio
    {
        RequerimientoRepositorio oRequerimientoRepositorio = new RequerimientoRepositorio();

        #region "Metodos No Transaccionales"

        public List<RequerimientoEntidad> listarxIniciativa(RequerimientoEntidad entidad)
        {
            return oRequerimientoRepositorio.listarxIniciativa(entidad);
        }


        #endregion

        #region "Metodos Transaccionales"

        public bool Grabar(RequerimientoEntidad entidad)
        {
            bool estado = false;
            if (oRequerimientoRepositorio.validarGrabacion(entidad) == false)
                estado = oRequerimientoRepositorio.Grabar(entidad);
            return estado;
        }

        public bool Modificar(RequerimientoEntidad entidad)
        {
            bool estado = false;
            if (oRequerimientoRepositorio.validarModificacion(entidad) == false)
                estado = oRequerimientoRepositorio.Modificar(entidad);
            return estado;
        }

        public bool Eliminar(string Codigo)
        {
            return oRequerimientoRepositorio.Eliminar(Codigo);
        }

        #endregion
    }
}
