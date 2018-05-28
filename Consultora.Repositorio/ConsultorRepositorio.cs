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
    public class ConsultorRepositorio
    {
        #region "Metodos No Transaccionales"
        public List<ConsultorEntidad> filtrar(ConsultorEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Consultor_Filtrar", cn);
                cmd.Parameters.Add(new SqlParameter("@Nombres", SqlDbType.VarChar, 150)).Value = (entidad.Empleado.Nom_Empleado == null ? "" : entidad.Empleado.Nom_Empleado);
                cmd.Parameters.Add(new SqlParameter("@Cod_Competencia", SqlDbType.Int)).Value = entidad.Competencia.Cod_Competencia;
                cmd.Parameters.Add(new SqlParameter("@Cod_Nivel_Competencia", SqlDbType.Int)).Value = entidad.NivelCompetencia.Cod_Nivel_Competencia;
                cmd.CommandType = CommandType.StoredProcedure;
                List<ConsultorEntidad> ListaConsultor = new List<ConsultorEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ConsultorEntidad oConsultorEntidad = new ConsultorEntidad();
                        oConsultorEntidad.Cod_Consultor = Reader.GetIntValue(reader, "Cod_Consultor");
                        oConsultorEntidad.Empleado = new EmpleadoEntidad
                        {
                            //Cod_Empleado = Reader.GetIntValue(reader, "Cod_Empleado"),
                            Nom_Empleado = Reader.GetStringValue(reader, "Nom_Empleado"),
                            AP_Empleado = Reader.GetStringValue(reader, "AP_Empleado"),
                            AM_Empleado = Reader.GetStringValue(reader, "AM_Empleado"),
                        };
                        oConsultorEntidad.NivelCompetencia = new NivelCompetenciaEntidad
                        {
                            Cod_Nivel_Competencia = Reader.GetIntValue(reader, "Cod_Nivel_Competencia"),
                            Nom_Nivel_Competencia = Reader.GetStringValue(reader, "Nom_Nivel_Competencia"),
                        };
                        oConsultorEntidad.Competencia = new CompetenciaEntidad
                        {
                            Cod_Competencia = Reader.GetIntValue(reader, "Cod_Competencia"),
                            Nom_Competencia = Reader.GetStringValue(reader, "Nom_Competencia"),
                        };

                        ListaConsultor.Add(oConsultorEntidad);
                    }
                }
                return ListaConsultor;
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
        public ConsultorEntidad filtrarxCodigo(string Codigo)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Consultor_FiltrarxCodigo", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Consultor", SqlDbType.Int)).Value = int.Parse(Codigo);
                cmd.CommandType = CommandType.StoredProcedure;
                ConsultorEntidad oConsultorEntidad = new ConsultorEntidad();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        oConsultorEntidad.Empleado = new EmpleadoEntidad
                        {
                            Nom_Empleado = Reader.GetStringValue(reader, "Nom_Empleado"),
                            AP_Empleado = Reader.GetStringValue(reader, "AP_Empleado"),
                            AM_Empleado = Reader.GetStringValue(reader, "AM_Empleado"),
                        };
                        oConsultorEntidad.NivelCompetencia = new NivelCompetenciaEntidad
                        {
                            Nom_Nivel_Competencia = Reader.GetStringValue(reader, "Nom_Nivel_Competencia"),
                        };
                        oConsultorEntidad.Competencia = new CompetenciaEntidad
                        {
                            Nom_Competencia = Reader.GetStringValue(reader, "Nom_Competencia"),
                        };
                    }
                }
                return oConsultorEntidad;
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
