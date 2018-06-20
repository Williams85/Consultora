using Consultora.Entidad;
using Consultora.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Dominio
{
    public class ServicioEmpresarialCompetenciaDominio
    {

        ServicioEmpresarialCompetenciaRepositorio oServicioEmpresarialCompetenciaRepositorio = new ServicioEmpresarialCompetenciaRepositorio();
        #region "Metodos No Transaccionales"

        public List<ServicioEmpresarialCompetenciaEntidad> BuscarRRHH(string Codigo)
        {
            return oServicioEmpresarialCompetenciaRepositorio.BuscarRRHH(Codigo);
        }
        public List<ServicioEmpresarialCompetenciaEntidad> ListarRequerimientos(string Codigo)
        {
            return oServicioEmpresarialCompetenciaRepositorio.ListarRequerimientos(Codigo);
        }
        public List<ServicioEmpresarialCompetenciaEntidad> BuscarRRHHAsignados(string Codigo)
        {
            return oServicioEmpresarialCompetenciaRepositorio.BuscarRRHHAsignados(Codigo);
        }

        #endregion

        #region "Metodos Transaccionales"

        public bool AsignarRRHH(List<ServicioEmpresarialCompetenciaEntidad> lista)
        {
            return oServicioEmpresarialCompetenciaRepositorio.AsignarRRHH(lista);
        }



        #endregion
    }
}
