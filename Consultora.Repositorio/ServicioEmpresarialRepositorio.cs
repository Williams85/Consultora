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
    public class ServicioEmpresarialRepositorio
    {
        #region "Metodos No Transaccionales"
        public List<ServicioEmpresarialEntidad> listarActivos()
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Servicio_Empresarial_listaractivos", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                List<ServicioEmpresarialEntidad> ListaServicioEmpresarial = new List<ServicioEmpresarialEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ServicioEmpresarialEntidad oServicioEmpresarialEntidad = new ServicioEmpresarialEntidad();
                        oServicioEmpresarialEntidad.Cod_Servicio_Empresarial = Reader.GetIntValue(reader, "Cod_Servicio_Empresarial");
                        oServicioEmpresarialEntidad.Nom_Servicio_Empresarial = Reader.GetStringValue(reader, "Nom_Servicio_Empresarial");
                        ListaServicioEmpresarial.Add(oServicioEmpresarialEntidad);
                    }
                }
                return ListaServicioEmpresarial;
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

        public ServicioEmpresarialEntidad FiltrarxCodigo(string Codigo)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Servicio_Empresarial_FiltrarxCodigo", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Servicio_Empresarial", SqlDbType.Int)).Value = Int32.Parse(Codigo);
                cmd.CommandType = CommandType.StoredProcedure;
                ServicioEmpresarialEntidad oServicioEmpresarialEntidad = new ServicioEmpresarialEntidad();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        oServicioEmpresarialEntidad.Cod_Servicio_Empresarial = Reader.GetIntValue(reader, "Cod_Servicio_Empresarial");
                        oServicioEmpresarialEntidad.Nom_Servicio_Empresarial = Reader.GetStringValue(reader, "Nom_Servicio_Empresarial");
                        oServicioEmpresarialEntidad.Cliente = new ClienteEntidad
                        {
                            Razon_Social = Reader.GetStringValue(reader, "Razon_Social"),
                        };
                        oServicioEmpresarialEntidad.Empleado = new EmpleadoEntidad
                        {
                            Nom_Empleado = Reader.GetStringValue(reader, "Nom_Empleado"),
                            AP_Empleado = Reader.GetStringValue(reader, "AP_Empleado"),
                            AM_Empleado = Reader.GetStringValue(reader, "AM_Empleado"),
                        };
                        oServicioEmpresarialEntidad.Servicio = new ServicioEntidad
                        {
                            Nom_Servicio = Reader.GetStringValue(reader, "Nom_Servicio"),
                        };
                        oServicioEmpresarialEntidad.Fecha_Inicio = Reader.GetDateTimeValue(reader, "Fecha_Inicio");
                        oServicioEmpresarialEntidad.Fecha_Fin = Reader.GetDateTimeValue(reader, "Fecha_Fin");

                    }
                }
                return oServicioEmpresarialEntidad;
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
        public List<ServicioEmpresarialEntidad> Filtrar(ServicioEmpresarialEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Servicio_Empresarial_Filtrar", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Servicio", SqlDbType.Int)).Value = entidad.Servicio.Cod_Servicio;
                cmd.Parameters.Add(new SqlParameter("@Cod_Servicio_Generado", SqlDbType.VarChar, 10)).Value = (entidad.Cod_Servicio_Generado != null ? entidad.Cod_Servicio_Generado : "");
                cmd.Parameters.Add(new SqlParameter("@Cod_Cliente", SqlDbType.Int)).Value = entidad.Cliente.Cod_Cliente;
                cmd.Parameters.Add(new SqlParameter("@Nom_Servicio_Empresarial", SqlDbType.VarChar, 100)).Value = (entidad.Nom_Servicio_Empresarial != null ? entidad.Nom_Servicio_Empresarial : "");
                cmd.CommandType = CommandType.StoredProcedure;
                List<ServicioEmpresarialEntidad> ListaServicioEmpresarial = new List<ServicioEmpresarialEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ServicioEmpresarialEntidad oServicioEmpresarialEntidad = new ServicioEmpresarialEntidad();
                        oServicioEmpresarialEntidad.Cod_Servicio_Empresarial = Reader.GetIntValue(reader, "Cod_Servicio_Empresarial");
                        oServicioEmpresarialEntidad.Nom_Servicio_Empresarial = Reader.GetStringValue(reader, "Nom_Servicio_Empresarial");
                        oServicioEmpresarialEntidad.Cliente = new ClienteEntidad
                        {
                            Razon_Social = Reader.GetStringValue(reader, "Razon_Social"),
                        };
                        oServicioEmpresarialEntidad.Servicio = new ServicioEntidad
                        {
                            Nom_Servicio = Reader.GetStringValue(reader, "Nom_Servicio"),
                        };
                        oServicioEmpresarialEntidad.Empleado = new EmpleadoEntidad
                        {
                            Nom_Empleado = Reader.GetStringValue(reader, "Nom_Empleado"),
                            AP_Empleado = Reader.GetStringValue(reader, "AP_Empleado"),
                            AM_Empleado = Reader.GetStringValue(reader, "AM_Empleado"),
                        };
                        oServicioEmpresarialEntidad.Fecha_Inicio = Reader.GetDateTimeValue(reader, "Fecha_Inicio");
                        oServicioEmpresarialEntidad.Fecha_Fin = Reader.GetDateTimeValue(reader, "Fecha_Fin");
                        ListaServicioEmpresarial.Add(oServicioEmpresarialEntidad);
                    }
                }
                return ListaServicioEmpresarial;
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

        public List<ServicioEmpresarialEntidad> BuscarAsignaciones()
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Servicio_Empresarial_BuscarAsignaciones", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                List<ServicioEmpresarialEntidad> ListaServicioEmpresarial = new List<ServicioEmpresarialEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ServicioEmpresarialEntidad oServicioEmpresarialEntidad = new ServicioEmpresarialEntidad();
                        oServicioEmpresarialEntidad.Cod_Servicio_Empresarial = Reader.GetIntValue(reader, "Cod_Servicio_Empresarial");
                        oServicioEmpresarialEntidad.Nom_Servicio_Empresarial = Reader.GetStringValue(reader, "Nom_Servicio_Empresarial");
                        oServicioEmpresarialEntidad.Cliente = new ClienteEntidad
                        {
                            Razon_Social = Reader.GetStringValue(reader, "Razon_Social"),
                        };
                        oServicioEmpresarialEntidad.Servicio = new ServicioEntidad
                        {
                            Nom_Servicio = Reader.GetStringValue(reader, "Nom_Servicio"),
                        };
                        oServicioEmpresarialEntidad.Empleado = new EmpleadoEntidad
                        {
                            Nom_Empleado = Reader.GetStringValue(reader, "Nom_Empleado"),
                            AP_Empleado = Reader.GetStringValue(reader, "AP_Empleado"),
                            AM_Empleado = Reader.GetStringValue(reader, "AM_Empleado"),
                        };
                        oServicioEmpresarialEntidad.Fecha_Asignacion = Reader.GetDateTimeValue(reader, "Fecha_Asignacion");
                        ListaServicioEmpresarial.Add(oServicioEmpresarialEntidad);
                    }
                }
                return ListaServicioEmpresarial;
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

        public bool AsignacionAutomatica(ServicioEmpresarialEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            SqlTransaction trans = null;
            try
            {
                bool estado = true;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_Servicio_Empresarial_AsignacionAutomatica", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Servicio_Empresarial", SqlDbType.Int)).Value = entidad.Cod_Servicio_Empresarial;
                cmd.Parameters.Add(new SqlParameter("@Cod_Empleado", SqlDbType.Int)).Value = entidad.Empleado.Cod_Empleado;
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
        public bool GrabarAprobacionAsignacionConsultores(string Codigo)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            SqlTransaction trans = null;
            try
            {
                bool estado = true;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_Servicio_Empresarial_AprobarAsignacion", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Servicio_Empresarial", SqlDbType.Int)).Value = int.Parse(Codigo);
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

        public bool GrabarRechazoAsignacionConsultores(string Codigo)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            SqlTransaction trans = null;
            try
            {
                bool estado = true;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_Servicio_Empresarial_RechazarAsignacion", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Servicio_Empresarial", SqlDbType.Int)).Value = int.Parse(Codigo);
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
