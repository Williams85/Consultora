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
    public class CompetenciaTecnicaRepositorio
    {
        #region "Metodos No Transaccionales"
        public List<CompetenciaTecnicaEntidad> filtrar(IniciativaCompetenciaTecnicaEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Competencia_Tecnica_Filtrar", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa_Competencia", SqlDbType.Int)).Value = entidad.Cod_Iniciativa_Competencia;
                cmd.Parameters.Add(new SqlParameter("@Nom_Competencia_Tecnica", SqlDbType.VarChar, 150)).Value = (entidad.CompetenciaTecnica.Nom_Competencia_Tecnica != null ? entidad.CompetenciaTecnica.Nom_Competencia_Tecnica : "");
                cmd.CommandType = CommandType.StoredProcedure;
                List<CompetenciaTecnicaEntidad> ListaCompetenciaTecnica = new List<CompetenciaTecnicaEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CompetenciaTecnicaEntidad oCompetenciaTecnicaEntidad = new CompetenciaTecnicaEntidad();
                        oCompetenciaTecnicaEntidad.Cod_Competencia_Tecnica = Reader.GetIntValue(reader, "Cod_Competencia_Tecnica");
                        oCompetenciaTecnicaEntidad.Nom_Competencia_Tecnica = Reader.GetStringValue(reader, "Nom_Competencia_Tecnica");
                        ListaCompetenciaTecnica.Add(oCompetenciaTecnicaEntidad);
                    }
                }
                return ListaCompetenciaTecnica;
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
