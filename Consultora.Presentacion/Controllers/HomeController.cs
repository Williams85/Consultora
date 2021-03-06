﻿using Consultora.Dominio;
using Consultora.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Consultora.Presentacion.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            CompetenciaDominio oCompetenciaDominio = new CompetenciaDominio();
            NivelCompetenciaDominio oNivelCompetenciaDominio = new NivelCompetenciaDominio();
            SessionManager.ListaCompetencia = oCompetenciaDominio.listarActivos();
            SessionManager.ListaNivelCompetencia = oNivelCompetenciaDominio.listarActivos();

            SessionManager.Usuario = new UsuarioEntidad
            {
                Cod_Usuario = 1,
                Empleado = new EmpleadoEntidad
                {
                    Cod_Empleado = 1,
                    Nom_Empleado = "Williams",
                    AP_Empleado = "Morales",
                    AM_Empleado = "Caballero"
                },
                Nom_Usuario = "Williams Morales Caballero",
            };
            return RedirectToAction("Index", "ServicioEmpresarial");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}