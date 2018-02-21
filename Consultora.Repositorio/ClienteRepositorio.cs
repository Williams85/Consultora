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
    public class ClienteRepositorio
    {
        #region "Metodos No Transaccionales"
        public List<ClienteEntidad> listarActivos()
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Cliente_listaractivos", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                List<ClienteEntidad> ListaCliente = new List<ClienteEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ClienteEntidad oClienteEntidad = new ClienteEntidad();
                        oClienteEntidad.Cod_Cliente = Reader.GetIntValue(reader, "Cod_Cliente");
                        oClienteEntidad.Razon_Social = Reader.GetStringValue(reader, "Razon_Social");
                        ListaCliente.Add(oClienteEntidad);
                    }
                }
                return ListaCliente;
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
