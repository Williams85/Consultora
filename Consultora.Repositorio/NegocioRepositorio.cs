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
    public class NegocioRepositorio
    {
        #region "Metodos No Transaccionales"
        public List<NegocioEntidad> listarActivos()
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Negocio_listaractivos", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                List<NegocioEntidad> ListaNegocio = new List<NegocioEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        NegocioEntidad oNegocioEntidad = new NegocioEntidad();
                        oNegocioEntidad.Cod_Negocio = Reader.GetIntValue(reader, "Cod_Negocio");
                        oNegocioEntidad.Nom_Negocio = Reader.GetStringValue(reader, "Nom_Negocio");
                        ListaNegocio.Add(oNegocioEntidad);
                    }
                }
                return ListaNegocio;
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
