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
    public class RequerimientoActividadRepositorio
    {
        #region "Metodos No Transaccionales"

        public List<RequerimientoActividadEntidad> listarxIniciativa(RequerimientoActividadEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Requerimiento_Actividad_ListarxIniciativa", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa", SqlDbType.Int)).Value = entidad.Requerimiento.Iniciativa.Cod_Iniciativa;
                cmd.CommandType = CommandType.StoredProcedure;
                List<RequerimientoActividadEntidad> ListaRequerimientoActividad = new List<RequerimientoActividadEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RequerimientoActividadEntidad oRequerimientoActividadEntidad = new RequerimientoActividadEntidad();
                        oRequerimientoActividadEntidad.Requerimiento = new RequerimientoEntidad
                        {
                            Cod_Requerimiento = Reader.GetIntValue(reader, "Cod_Requerimiento"),
                            Nom_Requerimiento = Reader.GetStringValue(reader, "Nom_Requerimiento"),
                           
                        };
                        oRequerimientoActividadEntidad.Actividad = new ActividadEntidad
                        {
                                Cod_Actividad = Reader.GetIntValue(reader, "Cod_Actividad"),
                                Nom_Actividad = Reader.GetStringValue(reader, "Nom_Actividad"),
                        };
                        oRequerimientoActividadEntidad.ComplejidadActividad = new ComplejidadActividadEntidad
                        {
                                Cod_Complejidad_Actividad = Reader.GetIntValue(reader, "Cod_Complejidad_Actividad"),
                                Nom_Complejidad_Actividad = Reader.GetStringValue(reader, "Nom_Complejidad_Actividad"),
                        };
                        oRequerimientoActividadEntidad.Cantidad = Reader.GetTinyIntValue(reader, "Cantidad");
                        oRequerimientoActividadEntidad.Tiempo_Estimado = Reader.GetDecimalValue(reader, "Tiempo_Estimado");
                        ListaRequerimientoActividad.Add(oRequerimientoActividadEntidad);
                    }
                }
                return ListaRequerimientoActividad;
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

        public List<RequerimientoActividadEntidad> listarxRequerimiento(RequerimientoActividadEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Requerimiento_Actividad_ListarxRequerimiento", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Requerimiento", SqlDbType.Int)).Value = entidad.Requerimiento.Cod_Requerimiento;
                cmd.CommandType = CommandType.StoredProcedure;
                List<RequerimientoActividadEntidad> ListaRequerimientoActividad = new List<RequerimientoActividadEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RequerimientoActividadEntidad oRequerimientoActividadEntidad = new RequerimientoActividadEntidad();
                        oRequerimientoActividadEntidad.Cod_Requerimiento_Actividad = Reader.GetIntValue(reader, "Cod_Requerimiento_Actividad");
                        oRequerimientoActividadEntidad.Actividad = new ActividadEntidad
                        {
                            Nom_Actividad = Reader.GetStringValue(reader, "Nom_Actividad"),
                        };
                        oRequerimientoActividadEntidad.ComplejidadActividad = new ComplejidadActividadEntidad
                        {
                            Nom_Complejidad_Actividad = Reader.GetStringValue(reader, "Nom_Complejidad_Actividad"),
                        };
                        oRequerimientoActividadEntidad.Cantidad = Reader.GetTinyIntValue(reader, "Cantidad");
                        ListaRequerimientoActividad.Add(oRequerimientoActividadEntidad);
                    }
                }
                return ListaRequerimientoActividad;
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

        public bool existeActividad(RequerimientoActividadEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Requerimiento_Actividad_ExisteActividad", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa", SqlDbType.Int)).Value = entidad.Requerimiento.Iniciativa.Cod_Iniciativa;
                cmd.CommandType = CommandType.StoredProcedure;
                bool estado = false;
                var valor = cmd.ExecuteScalar().ToString();
                if (valor == "1")
                    estado = true;
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
        public bool validarGrabacion(RequerimientoActividadEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Requerimiento_Actividad_ValidarGrabacion", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Requerimiento", SqlDbType.Int)).Value = entidad.Requerimiento.Cod_Requerimiento;
                cmd.Parameters.Add(new SqlParameter("@Cod_Actividad", SqlDbType.Int)).Value = entidad.Actividad.Cod_Actividad;
                cmd.Parameters.Add(new SqlParameter("@Cod_Complejidad_Actividad", SqlDbType.Int)).Value = entidad.ComplejidadActividad.Cod_Complejidad_Actividad;
                cmd.CommandType = CommandType.StoredProcedure;
                bool estado = false;
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
        public bool validarModificacion(RequerimientoActividadEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Requerimiento_Actividad_ValidarModificacion", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Requerimiento_Actividad", SqlDbType.Int)).Value = entidad.Cod_Requerimiento_Actividad;
                cmd.Parameters.Add(new SqlParameter("@Cod_Requerimiento", SqlDbType.Int)).Value = entidad.Requerimiento.Cod_Requerimiento;
                cmd.Parameters.Add(new SqlParameter("@Cod_Actividad", SqlDbType.Int)).Value = entidad.Actividad.Cod_Actividad;
                cmd.Parameters.Add(new SqlParameter("@Cod_Complejidad_Actividad", SqlDbType.Int)).Value = entidad.ComplejidadActividad.Cod_Complejidad_Actividad;
                cmd.CommandType = CommandType.StoredProcedure;
                bool estado = false;
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

        public bool Grabar(RequerimientoActividadEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            SqlTransaction trans = null;
            try
            {
                bool estado = true;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_Requerimiento_Actividad_Grabar", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Requerimiento", SqlDbType.Int)).Value = entidad.Requerimiento.Cod_Requerimiento;
                cmd.Parameters.Add(new SqlParameter("@Cod_Actividad", SqlDbType.Int)).Value = entidad.Actividad.Cod_Actividad;
                cmd.Parameters.Add(new SqlParameter("@Cod_Complejidad_Actividad", SqlDbType.Int)).Value = entidad.ComplejidadActividad.Cod_Complejidad_Actividad;
                cmd.Parameters.Add(new SqlParameter("@Cantidad", SqlDbType.TinyInt)).Value = entidad.Cantidad;
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

        public bool Modificar(RequerimientoActividadEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            SqlTransaction trans = null;
            try
            {
                bool estado = true;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_Requerimiento_Actividad_Modificar", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Requerimiento_Actividad", SqlDbType.Int)).Value = entidad.Cod_Requerimiento_Actividad;
                cmd.Parameters.Add(new SqlParameter("@Cod_Requerimiento", SqlDbType.Int)).Value = entidad.Requerimiento.Cod_Requerimiento;
                cmd.Parameters.Add(new SqlParameter("@Cod_Actividad", SqlDbType.Int)).Value = entidad.Actividad.Cod_Actividad;
                cmd.Parameters.Add(new SqlParameter("@Cod_Complejidad_Actividad", SqlDbType.Int)).Value = entidad.ComplejidadActividad.Cod_Complejidad_Actividad;
                cmd.Parameters.Add(new SqlParameter("@Cantidad", SqlDbType.TinyInt)).Value = entidad.Cantidad;
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
                SqlCommand cmd = new SqlCommand("usp_Requerimiento_Actividad_Eliminar", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Requerimiento_Actividad", SqlDbType.Int)).Value = Int32.Parse(Codigo);
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
