using Consultora.Entidad;
using Consultora.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Dominio
{
    public class IniciativaDominio
    {
        IniciativaRepositorio oIniciativaRepositorio = new IniciativaRepositorio();

        #region "Metodos No Transaccionales"
        public decimal CalcularCostoEquipo(string Codigo)
        {
            return oIniciativaRepositorio.CalcularCostoEquipo(Codigo);
        }
        public decimal CalcularCostoServicio(string Codigo)
        {
            return oIniciativaRepositorio.CalcularCostoServicio(Codigo);
        }
        public decimal CalcularCostoEquipoCliente(string Codigo)
        {
            return oIniciativaRepositorio.CalcularCostoEquipoCliente(Codigo);
        }
        public decimal CalcularTamañoServicio(string Codigo)
        {
            return oIniciativaRepositorio.CalcularTamañoServicio(Codigo);
        }
        #endregion

        #region "Metodos Transaccionales"

        public bool RegistrarEvakuaconRentabilidad(IniciativaEntidad entidad)
        {
            return oIniciativaRepositorio.RegistrarEvakuaconRentabilidad(entidad);
        }

        #endregion
    }
}
