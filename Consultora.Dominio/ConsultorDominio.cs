using Consultora.Entidad;
using Consultora.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Dominio
{
    public class ConsultorDominio
    {
        private ConsultorRepositorio oConsultorRepositorio = new ConsultorRepositorio();
        #region "Metodos No Transaccionales"
        public List<ConsultorEntidad> filtrar(ConsultorEntidad entidad)
        {
            return oConsultorRepositorio.filtrar(entidad);
        }
        public ConsultorEntidad filtrarxCodigo(string Codigo)
        {
            return oConsultorRepositorio.filtrarxCodigo(Codigo);
        }
        #endregion
    }
}
