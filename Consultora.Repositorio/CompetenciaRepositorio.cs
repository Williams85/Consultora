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
    public class CompetenciaRepositorio
    {
        #region "Metodos No Transaccionales"
        public List<CompetenciaEntidad> listarActivos()
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Competencia_ListarActivos", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                List<CompetenciaEntidad> ListaCompetencia = new List<CompetenciaEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CompetenciaEntidad oCompetenciaEntidad = new CompetenciaEntidad();
                        oCompetenciaEntidad.Cod_Competencia = Reader.GetIntValue(reader, "Cod_Competencia");
                        oCompetenciaEntidad.Nom_Competencia = Reader.GetStringValue(reader, "Nom_Competencia");
                        ListaCompetencia.Add(oCompetenciaEntidad);
                    }
                }
                return ListaCompetencia;
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
