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
    public class UsuarioRepositorio
    {
        #region "Metodos No Transaccionales"
        public List<UsuarioEntidad> ListarxPerfil(UsuarioEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Usuario_ListarxPerfil", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Perfil", SqlDbType.Int)).Value = entidad.Perfil.Cod_Perfil;
                cmd.CommandType = CommandType.StoredProcedure;
                List<UsuarioEntidad> ListaUsuario = new List<UsuarioEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        UsuarioEntidad oUsuarioEntidad = new UsuarioEntidad();
                        oUsuarioEntidad.Cod_Usuario = Reader.GetIntValue(reader, "Cod_Usuario");
                        oUsuarioEntidad.Empleado = new EmpleadoEntidad
                        {
                            Nom_Empleado = Reader.GetStringValue(reader, "Nom_Empleado"),
                            AP_Empleado = Reader.GetStringValue(reader, "AP_Empleado"),
                            AM_Empleado = Reader.GetStringValue(reader, "AM_Empleado"),
                        };
                        ListaUsuario.Add(oUsuarioEntidad);
                    }
                }
                return ListaUsuario;
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
