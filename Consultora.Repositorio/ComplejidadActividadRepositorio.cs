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
    public class ComplejidadActividadRepositorio
    {
        #region "Metodos No Transaccionales"
        public List<ComplejidadActividadEntidad> listarActivos()
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Complejidad_Actividad_ListarActivos", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                List<ComplejidadActividadEntidad> ListaComplejidadActividad = new List<ComplejidadActividadEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ComplejidadActividadEntidad oComplejidadActividadEntidad = new ComplejidadActividadEntidad();
                        oComplejidadActividadEntidad.Cod_Complejidad_Actividad = Reader.GetIntValue(reader, "Cod_Complejidad_Actividad");
                        oComplejidadActividadEntidad.Nom_Complejidad_Actividad = Reader.GetStringValue(reader, "Nom_Complejidad_Actividad");
                        ListaComplejidadActividad.Add(oComplejidadActividadEntidad);
                    }
                }
                return ListaComplejidadActividad;
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
