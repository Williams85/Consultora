using Consultora.Entidad;
using Consultora.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Dominio
{
    public class RequerimientoActividadDominio
    {
        RequerimientoActividadRepositorio oRequerimientoActividadRepositorio = new RequerimientoActividadRepositorio();

        #region "Metodos Transaccionales"
        public List<RequerimientoActividadEntidad> listarxIniciativa(RequerimientoActividadEntidad entidad)
        {
            return oRequerimientoActividadRepositorio.listarxIniciativa(entidad);
        }
        public List<RequerimientoActividadEntidad> listarxRequerimiento(RequerimientoActividadEntidad entidad)
        {
            return oRequerimientoActividadRepositorio.listarxRequerimiento(entidad);
        }
        public bool existeActividad(RequerimientoActividadEntidad entidad)
        {
            return oRequerimientoActividadRepositorio.existeActividad(entidad);
        }

        #endregion

        #region "Metodos Transaccionales"



        public bool Grabar(RequerimientoActividadEntidad entidad)
        {
            bool estado = false;
            if (oRequerimientoActividadRepositorio.validarGrabacion(entidad) == false)
                estado = oRequerimientoActividadRepositorio.Grabar(entidad);
            return estado;
        }

        public bool Modificar(RequerimientoActividadEntidad entidad)
        {
            bool estado = false;
            if (oRequerimientoActividadRepositorio.validarModificacion(entidad) == false)
                estado = oRequerimientoActividadRepositorio.Modificar(entidad);
            return estado;
        }

        public bool Eliminar(string Codigo)
        {
            return oRequerimientoActividadRepositorio.Eliminar(Codigo);
        }

        #endregion
    }
}
