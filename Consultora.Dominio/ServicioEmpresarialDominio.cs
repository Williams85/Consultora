﻿using Consultora.Entidad;
using Consultora.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Dominio
{
    public class ServicioEmpresarialDominio
    {
        ServicioEmpresarialRepositorio oServicioEmpresarialRepositorio = new ServicioEmpresarialRepositorio();
        #region "Metodos No Transaccionales"
        public List<ServicioEmpresarialEntidad> listarActivos()
        {
            return oServicioEmpresarialRepositorio.listarActivos();
        }
        public ServicioEmpresarialEntidad FiltrarxCodigo(string Codigo)
        {
            return oServicioEmpresarialRepositorio.FiltrarxCodigo(Codigo);
        }

        public List<ServicioEmpresarialEntidad> Filtrar(ServicioEmpresarialEntidad entidad)
        {
            return oServicioEmpresarialRepositorio.Filtrar(entidad);
        }

        #endregion
    }
}
