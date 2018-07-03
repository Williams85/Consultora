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
        public List<ServicioEmpresarialCompetenciaEntidad> BuscarConsultoresAsignados(string Codigo)
        {
            return oServicioEmpresarialCompetenciaRepositorio.BuscarConsultoresAsignados(Codigo);
        }

        #endregion

        #region "Metodos Transaccionales"

        public bool GrabarAsignacionAutomatica(List<ServicioEmpresarialCompetenciaEntidad> lista, ServicioEmpresarialEntidad entidad)
        {
            bool estado = false;
            ServicioEmpresarialRepositorio oServicioEmpresarialRepositorio = new ServicioEmpresarialRepositorio();
            if (oServicioEmpresarialRepositorio.AsignacionAutomatica(entidad))
                estado = oServicioEmpresarialCompetenciaRepositorio.AsignarRRHH(lista);
            return estado;
        }





        #endregion
    }
}
