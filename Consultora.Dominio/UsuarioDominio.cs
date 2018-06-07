using Consultora.Entidad;
using Consultora.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Dominio
{
    public class UsuarioDominio
    {
        UsuarioRepositorio oUsuarioRepositorio = new UsuarioRepositorio();

        #region "Metodos No Transaccionales"
        public List<UsuarioEntidad> ListarxPerfil(UsuarioEntidad entidad)
        {
            return oUsuarioRepositorio.ListarxPerfil(entidad);
        }

        #endregion
    }
}
