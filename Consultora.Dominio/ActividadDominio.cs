using Consultora.Entidad;
using Consultora.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Dominio
{
    public class ActividadDominio
    {
        ActividadRepositorio oActividadRepositorio = new ActividadRepositorio();
        #region "Metodos No Transaccionales"
        public List<ActividadEntidad> filtrar(ActividadEntidad entidad)
        {
            return oActividadRepositorio.filtrar(entidad);
        }

        #endregion
    }
}
