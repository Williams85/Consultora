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
    public class ActividadRepositorio
    {
        #region "Metodos No Transaccionales"
        public List<ActividadEntidad> filtrar(ActividadEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Actividad_Filtrar", cn);
                cmd.Parameters.Add(new SqlParameter("@Nom_Actividad", SqlDbType.VarChar, 150)).Value = (entidad.Nom_Actividad != null ? entidad.Nom_Actividad : "");
                cmd.CommandType = CommandType.StoredProcedure;
                List<ActividadEntidad> ListaActividad = new List<ActividadEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ActividadEntidad oActividadEntidad = new ActividadEntidad();
                        oActividadEntidad.Cod_Actividad = Reader.GetIntValue(reader, "Cod_Actividad");
                        oActividadEntidad.Nom_Actividad = Reader.GetStringValue(reader, "Nom_Actividad");
                        ListaActividad.Add(oActividadEntidad);
                    }
                }
                return ListaActividad;
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
