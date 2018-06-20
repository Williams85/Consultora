using Consultora.Entidad;
using Consultora.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Dominio
{
    public class ClienteDominio
    {
        ClienteRepositorio oClienteRepositorio = new ClienteRepositorio();

        #region "Metodos No Transaccionales"
        public List<ClienteEntidad> listarActivos()
        {
            return oClienteRepositorio.listarActivos();
        }
        #endregion
    }
}
