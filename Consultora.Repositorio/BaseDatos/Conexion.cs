using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Repositorio.BaseDatos
{
    public class Conexion
    {
        public static string CnConsultora = ConfigurationManager.ConnectionStrings["CnConsultora"].ToString();
        public static void tranRollBack(DbTransaction tran)
        {
            if (tran != null && tran.Connection != null)
                tran.Rollback();
        }
        public static void tranCommit(DbTransaction tran)
        {
            if (tran != null && tran.Connection != null)
                tran.Commit();
        }
        public static void cerrarConexion(DbConnection con)
        {
            if (con != null && con.State != ConnectionState.Closed)
            {
                con.Close();
                con.Dispose();
            }
        }
        public static void abrirConexion(DbConnection con)
        {
            if (con != null && con.State != ConnectionState.Open)
            {
                con.Open();
            }
        }
        public static void cerrarReader(IDataReader dr)
        {
            if (dr != null && !dr.IsClosed)
            {
                dr.Close();
                dr.Dispose();
            }
        }
    }
}
