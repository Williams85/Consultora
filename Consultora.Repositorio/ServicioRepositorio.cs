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
    public class ServicioRepositorio
    {
        #region "Metodos No Transaccionales"
        public List<ServicioEntidad> listarActivos()
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Servicio_listaractivos", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                List<ServicioEntidad> ListaServicio = new List<ServicioEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ServicioEntidad oServicioEntidad = new ServicioEntidad();
                        oServicioEntidad.Cod_Servicio = Reader.GetIntValue(reader, "Cod_Servicio");
                        oServicioEntidad.Nom_Servicio= Reader.GetStringValue(reader, "Nom_Servicio");
                        ListaServicio.Add(oServicioEntidad);
                    }
                }
                return ListaServicio;
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
