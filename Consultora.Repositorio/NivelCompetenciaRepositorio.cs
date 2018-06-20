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
    public class NivelCompetenciaRepositorio
    {
        #region "Metodos No Transaccionales"
        public List<NivelCompetenciaEntidad> listarActivos()
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Nivel_Competencia_ListarActivos", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                List<NivelCompetenciaEntidad> ListaNivelCompetencia = new List<NivelCompetenciaEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        NivelCompetenciaEntidad oNivelCompetenciaEntidad = new NivelCompetenciaEntidad();
                        oNivelCompetenciaEntidad.Cod_Nivel_Competencia = Reader.GetIntValue(reader, "Cod_Nivel_Competencia");
                        oNivelCompetenciaEntidad.Nom_Nivel_Competencia = Reader.GetStringValue(reader, "Nom_Nivel_Competencia");
                        ListaNivelCompetencia.Add(oNivelCompetenciaEntidad);
                    }
                }
                return ListaNivelCompetencia;
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
