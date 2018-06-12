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
    public class IniciativaCompetenciaTecnicaRepositorio
    {
        #region "Metodos No Transaccionales"

        public List<IniciativaCompetenciaTecnicaEntidad> listarxIniciativaCompetencia(IniciativaCompetenciaTecnicaEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Iniciativa_Competencia_Tecnica_ListarCompetencia", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa_Competencia", SqlDbType.Int)).Value = entidad.IniciativaCompetencia.Cod_Iniciativa_Competencia;
                cmd.CommandType = CommandType.StoredProcedure;
                List<IniciativaCompetenciaTecnicaEntidad> ListaIniciativaCompetenciaTecnica = new List<IniciativaCompetenciaTecnicaEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        IniciativaCompetenciaTecnicaEntidad oIniciativaCompetenciaTecnicaEntidad = new IniciativaCompetenciaTecnicaEntidad();
                        oIniciativaCompetenciaTecnicaEntidad.CompetenciaTecnica = new CompetenciaTecnicaEntidad
                        {
                            Cod_Competencia_Tecnica = Reader.GetIntValue(reader, "Cod_Competencia_Tecnica"),
                            Nom_Competencia_Tecnica = Reader.GetStringValue(reader, "Nom_Competencia_Tecnica"),
                        };

                        ListaIniciativaCompetenciaTecnica.Add(oIniciativaCompetenciaTecnicaEntidad);
                    }
                }
                return ListaIniciativaCompetenciaTecnica;
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

        #region "Metodos Transaccionales"

        public bool Grabar(IniciativaCompetenciaTecnicaEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            SqlTransaction trans = null;
            try
            {
                bool estado = true;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_Iniciativa_Competencia_Tecnica_Grabar", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa_Competencia", SqlDbType.Int)).Value = entidad.IniciativaCompetencia.Cod_Iniciativa_Competencia;
                cmd.Parameters.Add(new SqlParameter("@Cod_Competencia_Tecnica", SqlDbType.Int)).Value = entidad.CompetenciaTecnica.Cod_Competencia_Tecnica;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                if (cmd.ExecuteNonQuery() < 1) { estado = false; }
                if (estado)
                    trans.Commit();
                else
                    trans.Rollback();

                return estado;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                Conexion.cerrarConexion(cn);
            }
        }

        public bool Eliminar(string Codigo)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            SqlTransaction trans = null;
            try
            {
                bool estado = true;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_Iniciativa_Competencia_Tecnica_Eliminar", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa_Competencia_Tecnica", SqlDbType.Int)).Value = Int32.Parse(Codigo);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                if (cmd.ExecuteNonQuery() < 1) { estado = false; }
                if (estado)
                    trans.Commit();
                else
                    trans.Rollback();

                return estado;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                Conexion.cerrarConexion(cn);
            }
        }

        #endregion
    }
}
