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
    public class ServicioEmpresarialCompetenciaRepositorio
    {
        #region "Metodos No Transaccionales"

        public List<ServicioEmpresarialCompetenciaEntidad> BuscarRRHH(string Codigo)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Tb_Servicio_Empresarial_Competencias_BuscarRRHH", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Servicio_Empresarial", SqlDbType.Int)).Value = Int32.Parse(Codigo);
                cmd.CommandType = CommandType.StoredProcedure;
                List<ServicioEmpresarialCompetenciaEntidad> ListaServicioEmpresarialCompetencia = new List<ServicioEmpresarialCompetenciaEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ServicioEmpresarialCompetenciaEntidad oServicioEmpresarialCompetenciaEntidad = new ServicioEmpresarialCompetenciaEntidad();
                        oServicioEmpresarialCompetenciaEntidad.Cod_Servicio_Empresarial_Competencias = Reader.GetIntValue(reader, "Cod_Servicio_Empresarial_Competencias");
                        oServicioEmpresarialCompetenciaEntidad.Consultor = new ConsultorEntidad
                        {
                            Cod_Consultor = Reader.GetIntValue(reader, "Cod_Consultor"),
                            Empleado = new EmpleadoEntidad
                            {
                                Nom_Empleado = Reader.GetStringValue(reader, "Nom_Empleado"),
                                AP_Empleado = Reader.GetStringValue(reader, "AP_Empleado"),
                                AM_Empleado = Reader.GetStringValue(reader, "AM_Empleado"),
                            },
                            Competencia = new CompetenciaEntidad
                            {
                                Cod_Competencia = Reader.GetIntValue(reader, "Cod_Competencia"),
                                Nom_Competencia = Reader.GetStringValue(reader, "Nom_Competencia"),
                            },
                            NivelCompetencia = new NivelCompetenciaEntidad
                            {
                                Cod_Nivel_Competencia = Reader.GetIntValue(reader, "Cod_Nivel_Competencia"),
                                Nom_Nivel_Competencia = Reader.GetStringValue(reader, "Nom_Nivel_Competencia"),
                            },
                            Disponible = Reader.GetBooleanValue(reader, "Disponible"),
                            Asignado = Reader.GetBooleanValue(reader, "Asignado"),
                        };
                        ListaServicioEmpresarialCompetencia.Add(oServicioEmpresarialCompetenciaEntidad);
                    }
                }
                return ListaServicioEmpresarialCompetencia;
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

        public List<ServicioEmpresarialCompetenciaEntidad> ListarRequerimientos(string Codigo)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Servicio_Empresarial_Competencias_ListarRequerimientos", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Servicio_Empresarial", SqlDbType.Int)).Value = Int32.Parse(Codigo);
                cmd.CommandType = CommandType.StoredProcedure;
                List<ServicioEmpresarialCompetenciaEntidad> ListaServicioEmpresarialCompetencia = new List<ServicioEmpresarialCompetenciaEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ServicioEmpresarialCompetenciaEntidad oServicioEmpresarialCompetenciaEntidad = new ServicioEmpresarialCompetenciaEntidad();
                        oServicioEmpresarialCompetenciaEntidad.Competencia = new CompetenciaEntidad
                        {
                            Nom_Competencia = Reader.GetStringValue(reader, "Nom_Competencia"),
                        };
                        oServicioEmpresarialCompetenciaEntidad.NivelCompetencia = new NivelCompetenciaEntidad
                        {
                            Nom_Nivel_Competencia = Reader.GetStringValue(reader, "Nom_Nivel_Competencia"),
                        };
                        oServicioEmpresarialCompetenciaEntidad.Cantidad = Reader.GetIntValue(reader, "Cantidad");
                        ListaServicioEmpresarialCompetencia.Add(oServicioEmpresarialCompetenciaEntidad);
                    }
                }
                return ListaServicioEmpresarialCompetencia;
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

        public List<ServicioEmpresarialCompetenciaEntidad> BuscarRRHHAsignados(string Codigo)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Tb_Servicio_Empresarial_Competencias_ListarRRHHAsignados", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Servicio_Empresarial", SqlDbType.Int)).Value = Int32.Parse(Codigo);
                cmd.CommandType = CommandType.StoredProcedure;
                List<ServicioEmpresarialCompetenciaEntidad> ListaServicioEmpresarialCompetencia = new List<ServicioEmpresarialCompetenciaEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ServicioEmpresarialCompetenciaEntidad oServicioEmpresarialCompetenciaEntidad = new ServicioEmpresarialCompetenciaEntidad();
                        oServicioEmpresarialCompetenciaEntidad.Cod_Servicio_Empresarial_Competencias = Reader.GetIntValue(reader, "Cod_Servicio_Empresarial_Competencias");
                        oServicioEmpresarialCompetenciaEntidad.Consultor = new ConsultorEntidad
                        {
                            Cod_Consultor = Reader.GetIntValue(reader, "Cod_Consultor"),
                            Empleado = new EmpleadoEntidad
                            {
                                Nom_Empleado = Reader.GetStringValue(reader, "Nom_Empleado"),
                                AP_Empleado = Reader.GetStringValue(reader, "AP_Empleado"),
                                AM_Empleado = Reader.GetStringValue(reader, "AM_Empleado"),
                            },
                            Competencia = new CompetenciaEntidad
                            {
                                Cod_Competencia = Reader.GetIntValue(reader, "Cod_Competencia"),
                                Nom_Competencia = Reader.GetStringValue(reader, "Nom_Competencia"),
                            },
                            NivelCompetencia=new NivelCompetenciaEntidad{
                                Cod_Nivel_Competencia = Reader.GetIntValue(reader, "Cod_Nivel_Competencia"),
                                Nom_Nivel_Competencia = Reader.GetStringValue(reader, "Nom_Nivel_Competencia"),                           
                            },
                            Disponible = Reader.GetBooleanValue(reader, "Disponible"),
                        };
                        //oServicioEmpresarialCompetenciaEntidad.Competencia = new CompetenciaEntidad
                        //{
                        //    Cod_Competencia = Reader.GetIntValue(reader, "Cod_Competencia"),
                        //    Nom_Competencia = Reader.GetStringValue(reader, "Nom_Competencia"),
                        //};
                        //oServicioEmpresarialCompetenciaEntidad.NivelCompetencia = new NivelCompetenciaEntidad
                        //{
                        //    Cod_Nivel_Competencia = Reader.GetIntValue(reader, "Cod_Nivel_Competencia"),
                        //    Nom_Nivel_Competencia = Reader.GetStringValue(reader, "Nom_Nivel_Competencia"),
                        //};

                        ListaServicioEmpresarialCompetencia.Add(oServicioEmpresarialCompetenciaEntidad);
                    }
                }
                return ListaServicioEmpresarialCompetencia;
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

        public List<ServicioEmpresarialCompetenciaEntidad> BuscarConsultoresAsignados(string Codigo)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Servicio_Empresarial_Competencias_BuscarConsultoresAsignados", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Servicio_Empresarial", SqlDbType.Int)).Value = Int32.Parse(Codigo);
                cmd.CommandType = CommandType.StoredProcedure;
                List<ServicioEmpresarialCompetenciaEntidad> ListaServicioEmpresarialCompetencia = new List<ServicioEmpresarialCompetenciaEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ServicioEmpresarialCompetenciaEntidad oServicioEmpresarialCompetenciaEntidad = new ServicioEmpresarialCompetenciaEntidad();
                        //oServicioEmpresarialCompetenciaEntidad.Cod_Servicio_Empresarial_Competencias = Reader.GetIntValue(reader, "Cod_Servicio_Empresarial_Competencias");
                        oServicioEmpresarialCompetenciaEntidad.Consultor = new ConsultorEntidad
                        {
                            //Cod_Consultor = Reader.GetIntValue(reader, "Cod_Consultor"),
                            Empleado = new EmpleadoEntidad
                            {
                                Nom_Empleado = Reader.GetStringValue(reader, "Nom_Empleado"),
                                AP_Empleado = Reader.GetStringValue(reader, "AP_Empleado"),
                                AM_Empleado = Reader.GetStringValue(reader, "AM_Empleado"),
                            },
                            Competencia = new CompetenciaEntidad
                            {
                                //Cod_Competencia = Reader.GetIntValue(reader, "Cod_Competencia"),
                                Nom_Competencia = Reader.GetStringValue(reader, "Nom_Competencia"),
                            },
                            NivelCompetencia = new NivelCompetenciaEntidad
                            {
                                //Cod_Nivel_Competencia = Reader.GetIntValue(reader, "Cod_Nivel_Competencia"),
                                Nom_Nivel_Competencia = Reader.GetStringValue(reader, "Nom_Nivel_Competencia"),
                            },
                            Disponible = Reader.GetBooleanValue(reader, "Disponible"),
                        };
                        ListaServicioEmpresarialCompetencia.Add(oServicioEmpresarialCompetenciaEntidad);
                    }
                }
                return ListaServicioEmpresarialCompetencia;
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

        public bool AsignarRRHH(List<ServicioEmpresarialCompetenciaEntidad> lista)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnConsultora);
            SqlTransaction trans = null;
            try
            {
                bool estado = true;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                foreach (var entidad in lista)
                {
                    SqlCommand cmd = new SqlCommand("usp_Tb_Servicio_Empresarial_Competencias_AsignarRRHH", cn);
                    cmd.Parameters.Add(new SqlParameter("@Cod_Servicio_Empresarial_Competencias", SqlDbType.Int)).Value = entidad.Cod_Servicio_Empresarial_Competencias;
                    cmd.Parameters.Add(new SqlParameter("@Cod_Consultor", SqlDbType.Int)).Value = entidad.Consultor.Cod_Consultor;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Transaction = trans;
                    if (cmd.ExecuteNonQuery() < 1) { estado = false; break; }
                }


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
