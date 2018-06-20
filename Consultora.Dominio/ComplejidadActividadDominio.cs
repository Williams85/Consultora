using Consultora.Entidad;
using Consultora.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Dominio
{
    public class ComplejidadActividadDominio
    {
        ComplejidadActividadRepositorio oComplejidadActividadRepositorio = new ComplejidadActividadRepositorio();
        #region "Metodos No Transaccionales"
        public List<ComplejidadActividadEntidad> listarActivos()
        {
            return oComplejidadActividadRepositorio.listarActivos();
        }

        #endregion
    }
}
