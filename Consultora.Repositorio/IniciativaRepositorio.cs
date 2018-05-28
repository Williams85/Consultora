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

        public bool RegistrarEvakuaconRentabilidad(IniciativaEntidad entidad)
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



        #endregion
    }
}
