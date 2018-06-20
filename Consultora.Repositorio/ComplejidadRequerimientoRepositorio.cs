using Consultora.Entidad;
using Consultora.Repositorio.BaseDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Repositorio
{
    public class ComplejidadRequerimientoRepositorio
    {
        #region "Metodos No Transaccionales"
        public List<ComplejidadRequerimientoEntidad> listarActivos()
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Complejidad_Requerimiento_ListarActivos", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                List<ComplejidadRequerimientoEntidad> ListaComplejidadRequerimiento = new List<ComplejidadRequerimientoEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ComplejidadRequerimientoEntidad oComplejidadRequerimientoEntidad = new ComplejidadRequerimientoEntidad();
                        oComplejidadRequerimientoEntidad.Cod_Complejidad_Requerimiento = Reader.GetIntValue(reader, "Cod_Complejidad_Requerimiento");
                        oComplejidadRequerimientoEntidad.Nom_Complejidad_Requerimiento = Reader.GetStringValue(reader, "Nom_Complejidad_Requerimiento");
                        ListaComplejidadRequerimiento.Add(oComplejidadRequerimientoEntidad);
                    }
                }
                return ListaComplejidadRequerimiento;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                Conexion.cerrarConexion(cn);
            }
        }

        #endregion
    }
}
