using Consultora.Entidad;
using Consultora.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Dominio
{
    public class ServicioDominio
    {
        ServicioRepositorio oServicioRepositorio = new ServicioRepositorio();

        #region "Metodos No Transaccionales"
        public List<ServicioEntidad> listarActivos()
        {
            return oServicioRepositorio.listarActivos();
        }
        #endregion
    }
}
