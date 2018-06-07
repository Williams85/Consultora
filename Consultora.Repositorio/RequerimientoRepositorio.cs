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
    public class RequerimientoRepositorio
    {
        #region "Metodos No Transaccionales"

        public List<RequerimientoEntidad> listarxIniciativa(RequerimientoEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Requerimiento_ListarxIniciativa", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa", SqlDbType.Int)).Value = entidad.Iniciativa.Cod_Iniciativa;
                cmd.CommandType = CommandType.StoredProcedure;
                List<RequerimientoEntidad> ListaRequerimiento = new List<RequerimientoEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RequerimientoEntidad oRequerimientoEntidad = new RequerimientoEntidad();
                        oRequerimientoEntidad.Cod_Requerimiento = Reader.GetIntValue(reader, "Cod_Requerimiento");
                        oRequerimientoEntidad.Nom_Requerimiento = Reader.GetStringValue(reader, "Nom_Requerimiento");
                        oRequerimientoEntidad.ComplejidadRequerimiento = new ComplejidadRequerimientoEntidad
                        {
                            Cod_Complejidad_Requerimiento = Reader.GetIntValue(reader, "Cod_Complejidad_Requerimiento"),
                            Nom_Complejidad_Requerimiento = Reader.GetStringValue(reader, "Nom_Complejidad_Requerimiento"),
                        };
                        ListaRequerimiento.Add(oRequerimientoEntidad);
                    }
                }
                return ListaRequerimiento;
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

        public bool validarGrabacion(RequerimientoEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Requerimiento_ValidarGrabacion", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa", SqlDbType.Int)).Value = entidad.Iniciativa.Cod_Iniciativa;
                cmd.Parameters.Add(new SqlParameter("@Cod_Complejidad_Requerimiento", SqlDbType.Int)).Value = entidad.ComplejidadRequerimiento.Cod_Complejidad_Requerimiento;
                cmd.Parameters.Add(new SqlParameter("@Nom_Requerimiento", SqlDbType.VarChar, 150)).Value = entidad.Nom_Requerimiento;
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

        public bool validarModificacion(RequerimientoEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Requerimiento_ValidarModificacion", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Requerimiento", SqlDbType.Int)).Value = entidad.Cod_Requerimiento;
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa", SqlDbType.Int)).Value = entidad.Iniciativa.Cod_Iniciativa;
                cmd.Parameters.Add(new SqlParameter("@Cod_Complejidad_Requerimiento", SqlDbType.Int)).Value = entidad.ComplejidadRequerimiento.Cod_Complejidad_Requerimiento;
                cmd.Parameters.Add(new SqlParameter("@Nom_Requerimiento", SqlDbType.VarChar, 150)).Value = entidad.Nom_Requerimiento;
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

        public bool Grabar(RequerimientoEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            SqlTransaction trans = null;
            try
            {
                bool estado = true;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_Requerimiento_Grabar", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa", SqlDbType.Int)).Value = entidad.Iniciativa.Cod_Iniciativa;
                cmd.Parameters.Add(new SqlParameter("@Cod_Complejidad_Requerimiento", SqlDbType.Int)).Value = entidad.ComplejidadRequerimiento.Cod_Complejidad_Requerimiento;
                cmd.Parameters.Add(new SqlParameter("@Nom_Requerimiento", SqlDbType.VarChar, 150)).Value = entidad.Nom_Requerimiento;
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

        public bool Modificar(RequerimientoEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            SqlTransaction trans = null;
            try
            {
                bool estado = true;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_Requerimiento_Modificar", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Requerimiento", SqlDbType.Int)).Value = entidad.Cod_Requerimiento;
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa", SqlDbType.Int)).Value = entidad.Iniciativa.Cod_Iniciativa;
                cmd.Parameters.Add(new SqlParameter("@Cod_Complejidad_Requerimiento", SqlDbType.Int)).Value = entidad.ComplejidadRequerimiento.Cod_Complejidad_Requerimiento;
                cmd.Parameters.Add(new SqlParameter("@Nom_Requerimiento", SqlDbType.VarChar, 150)).Value = entidad.Nom_Requerimiento;
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
                SqlCommand cmd = new SqlCommand("usp_Requerimiento_Eliminar", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Requerimiento", SqlDbType.Int)).Value = Int32.Parse(Codigo);
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
