﻿using Consultora.Entidad;
using Consultora.Repositorio;
using Consultora.Utilitario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Dominio
{
    public class IniciativaDominio
    {
        IniciativaRepositorio oIniciativaRepositorio = new IniciativaRepositorio();

        #region "Metodos No Transaccionales"
        public IniciativaEntidad ObtenerxCodigo(string Codigo)
        {
            return oIniciativaRepositorio.ObtenerxCodigo(Codigo);
        }
        public List<IniciativaEntidad> filtrar(IniciativaEntidad entidad)
        {
            return oIniciativaRepositorio.filtrar(entidad);
        }

        public decimal CalcularCostoEquipo(string Codigo)
        {
            return oIniciativaRepositorio.CalcularCostoEquipo(Codigo);
        }
        public decimal CalcularCostoServicio(string Codigo)
        {
            return oIniciativaRepositorio.CalcularCostoServicio(Codigo);
        }
        public decimal CalcularCostoEquipoCliente(string Codigo)
        {
            return oIniciativaRepositorio.CalcularCostoEquipoCliente(Codigo);
        }
        public decimal CalcularTamañoServicio(string Codigo)
        {
            return oIniciativaRepositorio.CalcularTamañoServicio(Codigo);
        }
        public Response<bool> ValidaRentabilidad(string Codigo)
        {
            Response<bool> oResponse = new Response<bool>() { Valor = true };
            if (oIniciativaRepositorio.EvaluaRentabilidad(Codigo) == false)
            {
                oResponse.Valor = false;
                oResponse.Mensaje = "No se evaluo la rentabilidad";
            }
            return oResponse;
        }

        #endregion

        #region "Metodos Transaccionales"

        public bool RegistrarEvaluacionRentabilidad(IniciativaEntidad entidad)
        {
            return oIniciativaRepositorio.RegistrarEvaluacionRentabilidad(entidad);
        }

        public bool Grabar(IniciativaEntidad entidad, ref int Cod_Iniciativa)
        {
            return oIniciativaRepositorio.Grabar(entidad, ref Cod_Iniciativa);
        }

        public bool AsignarResponsableServicio(IniciativaEntidad entidad)
        {
            bool estado = false;
            if (oIniciativaRepositorio.AsignarResponsableServicio(entidad))
                if (oIniciativaRepositorio.ModificarEstado(entidad))
                    estado = true;
            return estado;
        }

        public bool RevisarRFP(IniciativaEntidad entidad)
        {
            bool estado = false;
            if (oIniciativaRepositorio.AsignarResponsableServicio(entidad))
                if (oIniciativaRepositorio.ModificarEstado(entidad))
                    estado = true;
            return estado;
        }

        public bool AsignarConsultorLider(IniciativaEntidad entidad)
        {
            bool estado = false;
            if (oIniciativaRepositorio.AsignarConsultorLider(entidad))
                if (oIniciativaRepositorio.ModificarEstado(entidad))
                    estado = true;
            return estado;
        }

        public bool EstimarTiempoProyecto(IniciativaEntidad entidad)
        {
            bool estado = false;
            if (oIniciativaRepositorio.AsignarConsultorLider(entidad))
                if (oIniciativaRepositorio.ModificarEstado(entidad))
                    estado = true;
            return estado;
        }

        public bool EstimarConsultores(IniciativaEntidad entidad)
        {
            bool estado = false;
            if (oIniciativaRepositorio.AsignarConsultorLider(entidad))
                if (oIniciativaRepositorio.ModificarEstado(entidad))
                    estado = true;
            return estado;
        }

        public bool EvaluarRentabilidad(IniciativaEntidad entidad)
        {
            bool estado = false;
            if (oIniciativaRepositorio.AsignarConsultorLider(entidad))
                if (oIniciativaRepositorio.ModificarEstado(entidad))
                    estado = true;
            return estado;
        }

        public bool ActualizarEstado(IniciativaEntidad entidad)
        {
            bool estado = false;
            if (oIniciativaRepositorio.Modificar(entidad))
                if (oIniciativaRepositorio.ModificarEstado(entidad))
                    estado = true;
            return estado;
        }



        public bool CerrarOportunidad(IniciativaEntidad entidad)
        {
            bool estado = false;
            if (oIniciativaRepositorio.Modificar(entidad))
                if (oIniciativaRepositorio.ModificarEstado(entidad))
                    if (oIniciativaRepositorio.CerrarOportunidad(entidad))
                        estado = true;
            return estado;
        }

        public bool CancelarOportunidad(IniciativaEntidad entidad)
        {
            bool estado = false;
            if (oIniciativaRepositorio.ModificarEstado(entidad))
                if (oIniciativaRepositorio.CancelarOportunidad(entidad))
                    estado = true;
            return estado;
        }

        #endregion
    }
}
