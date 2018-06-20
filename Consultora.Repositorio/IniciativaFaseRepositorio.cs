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
    public class IniciativaFaseRepositorio
    {
        #region "Metodos No Transaccionales"
        public bool existe(string Codigo)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Iniciativa_Fase_Existe", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa", SqlDbType.Int)).Value = Int32.Parse(Codigo);
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

        public List<IniciativaFaseEntidad> listarxIniciativa(string Codigo)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Iniciativa_Fase_ListarxIniciativa", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa", SqlDbType.Int)).Value = Int32.Parse(Codigo);
                cmd.CommandType = CommandType.StoredProcedure;
                List<IniciativaFaseEntidad> ListaIniciativaFase = new List<IniciativaFaseEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        IniciativaFaseEntidad oIniciativaFaseEntidad = new IniciativaFaseEntidad();
                        oIniciativaFaseEntidad.Fase = new FaseProyectoEntidad
                        {
                            Nom_Fase = Reader.GetStringValue(reader, "Nom_Fase")
                        };
                        oIniciativaFaseEntidad.Horas = Reader.GetDecimalValue(reader, "Horas");
                        ListaIniciativaFase.Add(oIniciativaFaseEntidad);
                    }
                }
                return ListaIniciativaFase;
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

        public bool Grabar(string Codigo)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            SqlTransaction trans = null;
            try
            {
                bool estado = true;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_Iniciativa_Fase_Grabar", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa", SqlDbType.Int)).Value = Int32.Parse(Codigo);
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

        public bool Modificar(string Codigo)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            SqlTransaction trans = null;
            try
            {
                bool estado = true;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_Iniciativa_Fase_Modificar", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Iniciativa", SqlDbType.Int)).Value = Int32.Parse(Codigo);
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
