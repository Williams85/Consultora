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
    public class IniciativaCompetenciaRepositorio
    {
        #region "Metodos No Transaccionales"

        public List<IniciativaCompetenciaEntidad> listarxIniciativa(IniciativaCompetenciaEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Iniciativa_Competencia_ListarxIniciativa", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa", SqlDbType.Int)).Value = entidad.Iniciativa.Cod_Iniciativa;
                cmd.CommandType = CommandType.StoredProcedure;
                List<IniciativaCompetenciaEntidad> ListaIniciativaCompetencia = new List<IniciativaCompetenciaEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        IniciativaCompetenciaEntidad oIniciativaCompetenciaEntidad = new IniciativaCompetenciaEntidad();
                        oIniciativaCompetenciaEntidad.Cod_Iniciativa_Competencia = Reader.GetIntValue(reader, "Cod_Iniciativa_Competencia");
                        oIniciativaCompetenciaEntidad.Competencia = new CompetenciaEntidad
                        {
                            Nom_Competencia = Reader.GetStringValue(reader, "Nom_Competencia"),
                        };
                        oIniciativaCompetenciaEntidad.NivelCompetencia = new NivelCompetenciaEntidad
                        {
                            Nom_Nivel_Competencia = Reader.GetStringValue(reader, "Nom_Nivel_Competencia"),
                        };
                        oIniciativaCompetenciaEntidad.Porcentaje_Participacion = Reader.GetDoubleValue(reader, "Porcentaje_Participacion");
                        oIniciativaCompetenciaEntidad.Horas_Participacion = Reader.GetDoubleValue(reader, "Horas_Participacion");
                        ListaIniciativaCompetencia.Add(oIniciativaCompetenciaEntidad);
                    }
                }
                return ListaIniciativaCompetencia;
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

        public bool validarGrabacion(IniciativaCompetenciaEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            SqlTransaction trans = null;
            try
            {
                bool estado = false;
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Iniciativa_Competencia_ValidarGrabacion", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa", SqlDbType.Int)).Value = entidad.Iniciativa.Cod_Iniciativa;
                cmd.Parameters.Add(new SqlParameter("@Cod_Competencia", SqlDbType.Int)).Value = entidad.Competencia.Cod_Competencia;
                cmd.Parameters.Add(new SqlParameter("@Cod_Nivel_Competencia", SqlDbType.Int)).Value = entidad.NivelCompetencia.Cod_Nivel_Competencia;
                cmd.CommandType = CommandType.StoredProcedure;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        estado = true;
                }

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

        #region "Metodos Transaccionales"

        public bool Grabar(IniciativaCompetenciaEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            SqlTransaction trans = null;
            try
            {
                bool estado = true;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_Iniciativa_Competencia_Grabar", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa", SqlDbType.Int)).Value = entidad.Iniciativa.Cod_Iniciativa;
                cmd.Parameters.Add(new SqlParameter("@Cod_Competencia", SqlDbType.Int)).Value = entidad.Competencia.Cod_Competencia;
                cmd.Parameters.Add(new SqlParameter("@Cod_Nivel_Competencia", SqlDbType.Int)).Value = entidad.NivelCompetencia.Cod_Nivel_Competencia;
                cmd.Parameters.Add(new SqlParameter("@Cod_Negocio", SqlDbType.Int)).Value = entidad.Negocio.Cod_Negocio;
                cmd.Parameters.Add(new SqlParameter("@Horas_Participacion", SqlDbType.Real)).Value = entidad.Horas_Participacion;
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

        public bool Modificar(IniciativaCompetenciaEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            SqlTransaction trans = null;
            try
            {
                bool estado = true;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_Iniciativa_Competencia_Modificar", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa_Competencia", SqlDbType.Int)).Value = entidad.Cod_Iniciativa_Competencia;
                cmd.Parameters.Add(new SqlParameter("@Horas_Participacion", SqlDbType.Real)).Value = entidad.Horas_Participacion;
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
                SqlCommand cmd = new SqlCommand("usp_Iniciativa_Competencia_Eliminar", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa_Competencia", SqlDbType.Int)).Value = Int32.Parse(Codigo);
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
