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
    public class IniciativaRepositorio
    {
        #region "Metodos No Transaccionales"
        public IniciativaEntidad ObtenerxCodigo(string Codigo)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Iniciativa_ObtenerxCodigo", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa", SqlDbType.Int)).Value = Int32.Parse(Codigo);
                cmd.CommandType = CommandType.StoredProcedure;
                IniciativaEntidad oIniciativaEntidad = new IniciativaEntidad();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        oIniciativaEntidad.Cod_Iniciativa = Reader.GetIntValue(reader, "Cod_Iniciativa");
                        oIniciativaEntidad.Nom_Iniciativa = Reader.GetStringValue(reader, "Nom_Iniciativa");
                        oIniciativaEntidad.Des_Iniciativa = Reader.GetStringValue(reader, "Des_Iniciativa");
                        oIniciativaEntidad.Cod_Oportunidad = Reader.GetStringValue(reader, "Cod_Oportunidad");

                        oIniciativaEntidad.Negocio = new NegocioEntidad
                        {
                            Cod_Negocio = Reader.GetIntValue(reader, "Cod_Negocio"),
                        };
                        oIniciativaEntidad.Servicio = new ServicioEntidad
                        {
                            Cod_Servicio = Reader.GetIntValue(reader, "Cod_Servicio"),
                        };
                        oIniciativaEntidad.Cliente = new ClienteEntidad
                        {
                            Cod_Cliente = Reader.GetIntValue(reader, "Cod_Cliente"),
                        };
                        oIniciativaEntidad.ResponsableServicio = new UsuarioEntidad
                        {
                            Cod_Usuario = Reader.GetIntValue(reader, "Responsable_Servicio"),
                        };
                        oIniciativaEntidad.ConsultorLider = new UsuarioEntidad
                        {
                            Cod_Usuario = Reader.GetIntValue(reader, "Consultor_Lider"),
                        };
                        oIniciativaEntidad.RFP = Reader.GetStringValue(reader, "RFP");
                        oIniciativaEntidad.Fecha_Registro = Reader.GetDateTimeValue(reader, "Fecha_Registro");
                        oIniciativaEntidad.Estado_Iniciativa = Reader.GetTinyIntValue(reader, "Estado_Iniciativa");
                    }
                }
                return oIniciativaEntidad;
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

        public List<IniciativaEntidad> filtrar(IniciativaEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Iniciativa_Filtrar", cn);
                cmd.Parameters.Add(new SqlParameter("@Nom_Iniciativa", SqlDbType.VarChar, 150)).Value = (entidad.Nom_Iniciativa != null ? entidad.Nom_Iniciativa : "");
                cmd.Parameters.Add(new SqlParameter("@Cod_Oportunidad", SqlDbType.VarChar, 8)).Value = (entidad.Cod_Oportunidad != null ? entidad.Cod_Oportunidad : "");
                cmd.Parameters.Add(new SqlParameter("@Cod_Negocio", SqlDbType.Int)).Value = entidad.Negocio.Cod_Negocio;
                cmd.Parameters.Add(new SqlParameter("@Cod_Servicio", SqlDbType.Int)).Value = entidad.Servicio.Cod_Servicio;
                cmd.Parameters.Add(new SqlParameter("@Cod_Cliente", SqlDbType.Int)).Value = entidad.Cliente.Cod_Cliente;
                cmd.Parameters.Add(new SqlParameter("@Estado_Iniciativa", SqlDbType.TinyInt)).Value = entidad.Estado_Iniciativa;
                cmd.CommandType = CommandType.StoredProcedure;
                List<IniciativaEntidad> ListaIniciativa = new List<IniciativaEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        IniciativaEntidad oIniciativaEntidad = new IniciativaEntidad();
                        oIniciativaEntidad.Cod_Iniciativa = Reader.GetIntValue(reader, "Cod_Iniciativa");
                        oIniciativaEntidad.Nom_Iniciativa = Reader.GetStringValue(reader, "Nom_Iniciativa");
                        oIniciativaEntidad.Des_Iniciativa = Reader.GetStringValue(reader, "Des_Iniciativa");
                        oIniciativaEntidad.Cod_Oportunidad = Reader.GetStringValue(reader, "Cod_Oportunidad");
                        oIniciativaEntidad.Negocio = new NegocioEntidad
                        {
                            Nom_Negocio = Reader.GetStringValue(reader, "Nom_Negocio"),
                        };
                        oIniciativaEntidad.Servicio = new ServicioEntidad
                        {
                            Nom_Servicio = Reader.GetStringValue(reader, "Nom_Servicio"),
                        };
                        oIniciativaEntidad.Cliente = new ClienteEntidad
                        {
                            Razon_Social = Reader.GetStringValue(reader, "Razon_Social"),
                        };
                        oIniciativaEntidad.Estado_Iniciativa = Reader.GetTinyIntValue(reader, "Estado_Iniciativa");
                        ListaIniciativa.Add(oIniciativaEntidad);
                    }
                }
                return ListaIniciativa;
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
        public decimal CalcularCostoEquipo(string Codigo)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_CalcularCostoEquipo", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa", SqlDbType.Int)).Value = Int32.Parse(Codigo);
                cmd.CommandType = CommandType.StoredProcedure;
                decimal valor = decimal.Parse(cmd.ExecuteScalar().ToString());
                return valor;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                Conexion.cerrarConexion(cn);
            }
        }
        public decimal CalcularCostoServicio(string Codigo)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_CalcularCostoServicio", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa", SqlDbType.Int)).Value = Int32.Parse(Codigo);
                cmd.CommandType = CommandType.StoredProcedure;
                decimal valor = decimal.Parse(cmd.ExecuteScalar().ToString());
                return valor;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                Conexion.cerrarConexion(cn);
            }
        }
        public decimal CalcularCostoEquipoCliente(string Codigo)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_CalcularCostoEquipoCliente", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa", SqlDbType.Int)).Value = Int32.Parse(Codigo);
                cmd.CommandType = CommandType.StoredProcedure;
                decimal valor = decimal.Parse(cmd.ExecuteScalar().ToString());
                return valor;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                Conexion.cerrarConexion(cn);
            }
        }
        public decimal CalcularTamañoServicio(string Codigo)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_CalcularTamañoServicio", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa", SqlDbType.Int)).Value = Int32.Parse(Codigo);
                cmd.CommandType = CommandType.StoredProcedure;
                decimal valor = decimal.Parse(cmd.ExecuteScalar().ToString());
                return valor;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                Conexion.cerrarConexion(cn);
            }
        }
        #endregion

        #region "Metodos Transaccionales"

        public bool RegistrarEvaluacionRentabilidad(IniciativaEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            SqlTransaction trans = null;
            try
            {
                bool estado = true;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_EvaluacionRentabilidad_Registrar", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa", SqlDbType.Int)).Value = entidad.Cod_Iniciativa;
                cmd.Parameters.Add(new SqlParameter("@Rentable", SqlDbType.Bit)).Value = entidad.Rentable;
                cmd.Parameters.Add(new SqlParameter("@Costo_Total_Equipo", SqlDbType.Real)).Value = entidad.CostoTotalEquipo;
                cmd.Parameters.Add(new SqlParameter("@Costo_Total_Servicio", SqlDbType.Real)).Value = entidad.CostoTotalServicio;
                cmd.Parameters.Add(new SqlParameter("@Costo_Total_Cliente", SqlDbType.Real)).Value = entidad.CostoTotalCliente;
                cmd.Parameters.Add(new SqlParameter("@Ganancia_Bruta", SqlDbType.Real)).Value = entidad.GananciaBruta;
                cmd.Parameters.Add(new SqlParameter("@Medidad_Servicio", SqlDbType.Real)).Value = entidad.MedidadServicio;
                cmd.Parameters.Add(new SqlParameter("@Tamaño_Servicio", SqlDbType.VarChar, 50)).Value = entidad.TamañoServicio;
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

        public bool Grabar(IniciativaEntidad entidad, ref int Cod_Iniciativa)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            SqlTransaction trans = null;
            try
            {
                bool estado = true;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_Iniciativa_Grabar", cn);
                cmd.Parameters.Add(new SqlParameter("@Nom_Iniciativa", SqlDbType.VarChar, 50)).Value = entidad.Nom_Iniciativa;
                cmd.Parameters.Add(new SqlParameter("@Des_Iniciativa", SqlDbType.VarChar, 150)).Value = entidad.Des_Iniciativa;
                cmd.Parameters.Add(new SqlParameter("@Cod_Negocio", SqlDbType.Int)).Value = entidad.Negocio.Cod_Negocio;
                cmd.Parameters.Add(new SqlParameter("@Cod_Servicio", SqlDbType.Int)).Value = entidad.Servicio.Cod_Servicio;
                cmd.Parameters.Add(new SqlParameter("@Cod_Cliente", SqlDbType.Int)).Value = entidad.Cliente.Cod_Cliente;
                cmd.Parameters.Add(new SqlParameter("@RFP", SqlDbType.VarChar, 500)).Value = entidad.RFP;
                cmd.Parameters.Add(new SqlParameter("@Estado_Iniciativa", SqlDbType.TinyInt)).Value = entidad.Estado_Iniciativa;
                cmd.Parameters.Add(new SqlParameter("@Fecha_Registro", SqlDbType.SmallDateTime)).Value = entidad.Fecha_Registro;
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa", SqlDbType.Int)).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                if (cmd.ExecuteNonQuery() < 1) { estado = false; }
                if (estado)
                {
                    trans.Commit();
                    Cod_Iniciativa = int.Parse(cmd.Parameters["@Cod_Iniciativa"].Value.ToString());
                }
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
        public bool Modificar(IniciativaEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            SqlTransaction trans = null;
            try
            {
                bool estado = true;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_Iniciativa_Modificar", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa", SqlDbType.Int)).Value = entidad.Cod_Iniciativa;
                cmd.Parameters.Add(new SqlParameter("@Nom_Iniciativa", SqlDbType.VarChar, 50)).Value = entidad.Nom_Iniciativa;
                cmd.Parameters.Add(new SqlParameter("@Des_Iniciativa", SqlDbType.VarChar, 150)).Value = entidad.Des_Iniciativa;
                cmd.Parameters.Add(new SqlParameter("@Cod_Negocio", SqlDbType.Int)).Value = entidad.Negocio.Cod_Negocio;
                cmd.Parameters.Add(new SqlParameter("@Cod_Servicio", SqlDbType.Int)).Value = entidad.Servicio.Cod_Servicio;
                cmd.Parameters.Add(new SqlParameter("@Cod_Cliente", SqlDbType.Int)).Value = entidad.Cliente.Cod_Cliente;
                cmd.Parameters.Add(new SqlParameter("@RFP", SqlDbType.VarChar, 500)).Value = entidad.RFP;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                if (cmd.ExecuteNonQuery() < 1) { estado = false; }
                if (estado)
                {
                    trans.Commit();
                }
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

        public bool ModificarEstado(IniciativaEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            SqlTransaction trans = null;
            try
            {
                bool estado = true;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_Iniciativa_ModificarEstado", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa", SqlDbType.Int)).Value = entidad.Cod_Iniciativa;
                cmd.Parameters.Add(new SqlParameter("@Estado_Iniciativa", SqlDbType.TinyInt)).Value = entidad.Estado_Iniciativa;
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

        public bool AsignarResponsableServicio(IniciativaEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            SqlTransaction trans = null;
            try
            {
                bool estado = true;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_Iniciativa_AsignarResponsable", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa", SqlDbType.Int)).Value = entidad.Cod_Iniciativa;
                cmd.Parameters.Add(new SqlParameter("@Nom_Iniciativa", SqlDbType.VarChar, 50)).Value = entidad.Nom_Iniciativa;
                cmd.Parameters.Add(new SqlParameter("@Des_Iniciativa", SqlDbType.VarChar, 150)).Value = entidad.Des_Iniciativa;
                cmd.Parameters.Add(new SqlParameter("@Cod_Negocio", SqlDbType.Int)).Value = entidad.Negocio.Cod_Negocio;
                cmd.Parameters.Add(new SqlParameter("@Cod_Servicio", SqlDbType.Int)).Value = entidad.Servicio.Cod_Servicio;
                cmd.Parameters.Add(new SqlParameter("@Cod_Cliente", SqlDbType.Int)).Value = entidad.Cliente.Cod_Cliente;
                cmd.Parameters.Add(new SqlParameter("@RFP", SqlDbType.VarChar, 500)).Value = entidad.RFP;
                cmd.Parameters.Add(new SqlParameter("@Responsable_Servicio", SqlDbType.Int)).Value = entidad.ResponsableServicio.Cod_Usuario;
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

        public bool AsignarConsultorLider(IniciativaEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            SqlTransaction trans = null;
            try
            {
                bool estado = true;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_Iniciativa_AsignarConsultorLider", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa", SqlDbType.Int)).Value = entidad.Cod_Iniciativa;
                cmd.Parameters.Add(new SqlParameter("@Nom_Iniciativa", SqlDbType.VarChar, 50)).Value = entidad.Nom_Iniciativa;
                cmd.Parameters.Add(new SqlParameter("@Des_Iniciativa", SqlDbType.VarChar, 150)).Value = entidad.Des_Iniciativa;
                cmd.Parameters.Add(new SqlParameter("@Cod_Negocio", SqlDbType.Int)).Value = entidad.Negocio.Cod_Negocio;
                cmd.Parameters.Add(new SqlParameter("@Cod_Servicio", SqlDbType.Int)).Value = entidad.Servicio.Cod_Servicio;
                cmd.Parameters.Add(new SqlParameter("@Cod_Cliente", SqlDbType.Int)).Value = entidad.Cliente.Cod_Cliente;
                cmd.Parameters.Add(new SqlParameter("@RFP", SqlDbType.VarChar, 500)).Value = entidad.RFP;
                cmd.Parameters.Add(new SqlParameter("@Responsable_Servicio", SqlDbType.Int)).Value = entidad.ResponsableServicio.Cod_Usuario;
                cmd.Parameters.Add(new SqlParameter("@Consultor_Lider", SqlDbType.Int)).Value = entidad.ConsultorLider.Cod_Usuario;
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

        public bool CerrarOportunidad(IniciativaEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            SqlTransaction trans = null;
            try
            {
                bool estado = true;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_Iniciativa_CerrarOportunidad", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa", SqlDbType.Int)).Value = entidad.Cod_Iniciativa;
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
