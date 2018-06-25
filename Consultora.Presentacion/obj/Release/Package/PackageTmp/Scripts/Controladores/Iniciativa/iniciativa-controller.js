$(document).ready(function () {

    $("#btn-nuevo-descargar").hide();
    $("#Resultado").hide();
    $("#Message-Error").hide();
    $("#IdViewTotalGananciaBruta").hide();
    $("#IdViewPorcentajeRentabilidad").hide();
    $("#IdViewEvaluandoRentabilidad").hide();
    $("#MensajeCarga").hide();

    $("#idEvaluarRentabilidad").on("click", function () {
        $("#MensajeCarga").show();
        $("#MensajeCarga").html("Calculando costo de equipo de consultores...");
        setTimeout(function (parametros) {
            var parametros = { "Codigo": $("#Cod_Iniciativa").val() };
            CalcularCostoEquipo(parametros);
        }, 2000);
        //CalcularCostoEquipo(parametros)
    });

    $("#idGuardar").on("click", function () {
        var parametros = {
            "Cod_Iniciativa": $("#Cod_Iniciativa").val(),
            "Rentable": ($("#IdRentable").val() == "1" ? true : false),
            "CostoTotalEquipo": parseFloat($("#IdCostoEquipo").val()),
            "CostoTotalServicio": parseFloat($("#IdCostoServicio").val()),
            "CostoTotalCliente": parseFloat($("#IdCostoCliente").val()),
            "GananciaBruta": parseFloat($("#IdGananciaBruta").val()),
            "MedidadServicio": parseFloat($("#MedidaServicio").val()),
            "TamañoServicio": $("#IdMedidaServicio").val(),
        };

        RegistrarEvaluacionRentabilidad(parametros);
    });

    $("a.Descargar").on("click", function () {
        var Url = $(this).prop('href') + "/" + $("#Cod_Iniciativa").val();
        $.fileDownload(Url);
        return false;
    });

    $("#btn-buscar-iniciativa").on("click", function () {
        var parametros = {
            "Nom_Iniciativa": $("#Nom_Iniciativa").val(),
            "Cod_Oportunidad": $("#Cod_Oportunidad").val(),
            "Negocio": { "Cod_Negocio": $("#Cod_Negocio").val() },
            "Servicio": { "Cod_Servicio": $("#Cod_Servicio").val() },
            "Cliente": { "Cod_Cliente": $("#Cod_Cliente").val() },
            "Estado_Iniciativa": $("#Estado_Iniciativa").val(),
        };
        BuscarOportunidad(parametros)
    });

    $("#btn-nuevo-siguiente").on("click", function (e) {
        $("#Message-Error strong").empty();
        $("#Message-Error").hide();
        //Validar Formulario
        var mensaje = "";
        var NombreProyecto = $("#Nom_Iniciativa").val();
        var DescripcipnProyecto = $("#Des_Iniciativa").val();

        if (NombreProyecto == null || NombreProyecto == "") {
            mensaje += Constantes.Message.FaltaNombreProyecto + Constantes.SaltoHtml;
        }
        if (DescripcipnProyecto == null || DescripcipnProyecto == "") {
            mensaje += Constantes.Message.FaltaDescripcionProyecto + Constantes.SaltoHtml;
        }

        if (mensaje != null && $.trim(mensaje) != "") {
            MostrarMensajeError(mensaje);
            return false;
        }
        $("#Indicador").val("1");
        $("#FormNuevaIniciativa").submit();
        return false;
    });

    $("#btn-ars-siguiente").on("click", function () {
        $("#Message-Error strong").empty();
        $("#Message-Error").hide();
        //Validar Formulario
        var mensaje = "";
        var NombreProyecto = $("#Nom_Iniciativa").val();
        var DescripcipnProyecto = $("#Des_Iniciativa").val();

        if (NombreProyecto == null || NombreProyecto == "") {
            mensaje += Constantes.Message.FaltaNombreProyecto + Constantes.SaltoHtml;
        }
        if (DescripcipnProyecto == null || DescripcipnProyecto == "") {
            mensaje += Constantes.Message.FaltaDescripcionProyecto + Constantes.SaltoHtml;
        }
        if (mensaje != null && $.trim(mensaje) != "") {
            MostrarMensajeError(mensaje);
            return false;
        }
        $("#Indicador").val("1");
        $("#FormARS").submit();
        return false;;
    });

    $("#btn-rfp-siguiente").on("click", function () {
        $("#Message-Error strong").empty();
        $("#Message-Error").hide();
        //Validar Formulario
        var mensaje = "";
        var NombreProyecto = $("#Nom_Iniciativa").val();
        var DescripcipnProyecto = $("#Des_Iniciativa").val();

        if (NombreProyecto == null || NombreProyecto == "") {
            mensaje += Constantes.Message.FaltaNombreProyecto + Constantes.SaltoHtml;
        }
        if (DescripcipnProyecto == null || DescripcipnProyecto == "") {
            mensaje += Constantes.Message.FaltaDescripcionProyecto + Constantes.SaltoHtml;
        }

        if (mensaje != null && $.trim(mensaje) != "") {
            MostrarMensajeError(mensaje);
            return false;
        }
        $("#Indicador").val("1");
        $("#FormRFP").submit();
        return false;
    });

    $("#btn-acl-siguiente").on("click", function () {
        $("#Message-Error strong").empty();
        $("#Message-Error").hide();
        //Validar Formulario
        var mensaje = "";
        var NombreProyecto = $("#Nom_Iniciativa").val();
        var DescripcipnProyecto = $("#Des_Iniciativa").val();

        if (NombreProyecto == null || NombreProyecto == "") {
            mensaje += Constantes.Message.FaltaNombreProyecto + Constantes.SaltoHtml;
        }
        if (DescripcipnProyecto == null || DescripcipnProyecto == "") {
            mensaje += Constantes.Message.FaltaDescripcionProyecto + Constantes.SaltoHtml;
        }

        if (mensaje != null && $.trim(mensaje) != "") {
            MostrarMensajeError(mensaje);
            return false;
        }
        $("#Indicador").val("1");
        $("#FormACL").submit();
        return false;
    });

    $("#btn-etp-siguiente").on("click", function () {
        $("#Message-Error strong").empty();
        $("#Message-Error").hide();

        //Validar Formulario
        var mensaje = "";
        var NombreProyecto = $("#Nom_Iniciativa").val();
        var DescripcipnProyecto = $("#Des_Iniciativa").val();

        if (NombreProyecto == null || NombreProyecto == "") {
            mensaje += Constantes.Message.FaltaNombreProyecto + Constantes.SaltoHtml;
        }
        if (DescripcipnProyecto == null || DescripcipnProyecto == "") {
            mensaje += Constantes.Message.FaltaDescripcionProyecto + Constantes.SaltoHtml;
        }

        if (mensaje != null && $.trim(mensaje) != "") {
            MostrarMensajeError(mensaje);
            return false;
        }

        var parametros = {
            "Requerimiento": {
                "Iniciativa": {
                    "Cod_Iniciativa": $("#Cod_Iniciativa").val(),
                }
            }
        }
        ValidarActividades(parametros, "#Indicador", "#FormETP")
        return false;
    });

    $("#btn-ecp-siguiente").on("click", function (e) {
        $("#Message-Error strong").empty();
        $("#Message-Error").hide();
        //Validar Formulario
        var mensaje = "";
        var NombreProyecto = $("#Nom_Iniciativa").val();
        var DescripcipnProyecto = $("#Des_Iniciativa").val();

        if (NombreProyecto == null || NombreProyecto == "") {
            mensaje += Constantes.Message.FaltaNombreProyecto + Constantes.SaltoHtml;
        }
        if (DescripcipnProyecto == null || DescripcipnProyecto == "") {
            mensaje += Constantes.Message.FaltaDescripcionProyecto + Constantes.SaltoHtml;
        }

        if (mensaje != null && $.trim(mensaje) != "") {
            MostrarMensajeError(mensaje);
            return false;
        }
        var parametros = {
            "Iniciativa": { "Cod_Iniciativa": $("#Cod_Iniciativa").val(), }
        };
        ValidarConsultores(parametros, "#Indicador", "#FormECP");

        //$("#Indicador").val("1");
        //$("#FormECP").submit();
        return false;
    });

    $("#btn-er-siguiente").on("click", function (e) {
        $("#Message-Error strong").empty();
        $("#Message-Error").hide();
        //Validar Formulario
        var mensaje = "";
        var NombreProyecto = $("#Nom_Iniciativa").val();
        var DescripcipnProyecto = $("#Des_Iniciativa").val();

        if (NombreProyecto == null || NombreProyecto == "") {
            mensaje += Constantes.Message.FaltaNombreProyecto + Constantes.SaltoHtml;
        }
        if (DescripcipnProyecto == null || DescripcipnProyecto == "") {
            mensaje += Constantes.Message.FaltaDescripcionProyecto + Constantes.SaltoHtml;
        }

        if (mensaje != null && $.trim(mensaje) != "") {
            MostrarMensajeError(mensaje);
            return false;
        }
        //var parametros = {
        //    "Iniciativa": { "Cod_Iniciativa": $("#Cod_Iniciativa").val(), }
        //};
        //ValidarConsultores(parametros, "#Indicador", "#FormECP");

        $("#Indicador").val("1");
        $("#FormER").submit();
        return false;
    });

    $("#btn-dpt-siguiente").on("click", function (e) {
        $("#Message-Error strong").empty();
        $("#Message-Error").hide();
        //Validar Formulario
        var mensaje = "";
        var NombreProyecto = $("#Nom_Iniciativa").val();
        var DescripcipnProyecto = $("#Des_Iniciativa").val();

        if (NombreProyecto == null || NombreProyecto == "") {
            mensaje += Constantes.Message.FaltaNombreProyecto + Constantes.SaltoHtml;
        }
        if (DescripcipnProyecto == null || DescripcipnProyecto == "") {
            mensaje += Constantes.Message.FaltaDescripcionProyecto + Constantes.SaltoHtml;
        }

        if (mensaje != null && $.trim(mensaje) != "") {
            MostrarMensajeError(mensaje);
            return false;
        }

        $("#Indicador").val("1");
        $("#FormPT").submit();
        return false;
    });

    $("#btn-rpt-conforme").on("click", function (e) {
        $("#Message-Error strong").empty();
        $("#Message-Error").hide();
        //Validar Formulario
        var mensaje = "";
        var NombreProyecto = $("#Nom_Iniciativa").val();
        var DescripcipnProyecto = $("#Des_Iniciativa").val();

        if (NombreProyecto == null || NombreProyecto == "") {
            mensaje += Constantes.Message.FaltaNombreProyecto + Constantes.SaltoHtml;
        }
        if (DescripcipnProyecto == null || DescripcipnProyecto == "") {
            mensaje += Constantes.Message.FaltaDescripcionProyecto + Constantes.SaltoHtml;
        }

        if (mensaje != null && $.trim(mensaje) != "") {
            MostrarMensajeError(mensaje);
            return false;
        }

        $("#Indicador").val("1");
        $("#IndiUrl").val("1");
        $("#FormRPT").submit();
        return false;
    });

    $("#btn-rpt-no-conforme").on("click", function (e) {
        $("#Message-Error strong").empty();
        $("#Message-Error").hide();
        //Validar Formulario
        var mensaje = "";
        var NombreProyecto = $("#Nom_Iniciativa").val();
        var DescripcipnProyecto = $("#Des_Iniciativa").val();

        if (NombreProyecto == null || NombreProyecto == "") {
            mensaje += Constantes.Message.FaltaNombreProyecto + Constantes.SaltoHtml;
        }
        if (DescripcipnProyecto == null || DescripcipnProyecto == "") {
            mensaje += Constantes.Message.FaltaDescripcionProyecto + Constantes.SaltoHtml;
        }

        if (mensaje != null && $.trim(mensaje) != "") {
            MostrarMensajeError(mensaje);
            return false;
        }

        $("#Indicador").val("1");
        $("#IndiUrl").val("2");
        $("#FormRPT").submit();
        return false;
    });

    $("#btn-ept-siguiente").on("click", function (e) {
        $("#Message-Error strong").empty();
        $("#Message-Error").hide();
        //Validar Formulario
        var mensaje = "";
        var NombreProyecto = $("#Nom_Iniciativa").val();
        var DescripcipnProyecto = $("#Des_Iniciativa").val();

        if (NombreProyecto == null || NombreProyecto == "") {
            mensaje += Constantes.Message.FaltaNombreProyecto + Constantes.SaltoHtml;
        }
        if (DescripcipnProyecto == null || DescripcipnProyecto == "") {
            mensaje += Constantes.Message.FaltaDescripcionProyecto + Constantes.SaltoHtml;
        }

        if (mensaje != null && $.trim(mensaje) != "") {
            MostrarMensajeError(mensaje);
            return false;
        }

        $("#Indicador").val("1");
        $("#FormEPT").submit();
        return false;
    });

    $("#btn-erc-aprobado").on("click", function (e) {
        $("#Message-Error strong").empty();
        $("#Message-Error").hide();
        //Validar Formulario
        var mensaje = "";
        var NombreProyecto = $("#Nom_Iniciativa").val();
        var DescripcipnProyecto = $("#Des_Iniciativa").val();

        if (NombreProyecto == null || NombreProyecto == "") {
            mensaje += Constantes.Message.FaltaNombreProyecto + Constantes.SaltoHtml;
        }
        if (DescripcipnProyecto == null || DescripcipnProyecto == "") {
            mensaje += Constantes.Message.FaltaDescripcionProyecto + Constantes.SaltoHtml;
        }

        if (mensaje != null && $.trim(mensaje) != "") {
            MostrarMensajeError(mensaje);
            return false;
        }

        $("#Indicador").val("1");
        $("#IndiUrl").val("1");
        $("#FormERC").submit();
        return false;
    });

    $("#btn-erc-observado").on("click", function (e) {
        $("#Message-Error strong").empty();
        $("#Message-Error").hide();
        //Validar Formulario
        var mensaje = "";
        var NombreProyecto = $("#Nom_Iniciativa").val();
        var DescripcipnProyecto = $("#Des_Iniciativa").val();

        if (NombreProyecto == null || NombreProyecto == "") {
            mensaje += Constantes.Message.FaltaNombreProyecto + Constantes.SaltoHtml;
        }
        if (DescripcipnProyecto == null || DescripcipnProyecto == "") {
            mensaje += Constantes.Message.FaltaDescripcionProyecto + Constantes.SaltoHtml;
        }

        if (mensaje != null && $.trim(mensaje) != "") {
            MostrarMensajeError(mensaje);
            return false;
        }

        $("#Indicador").val("1");
        $("#IndiUrl").val("2");
        $("#FormERC").submit();
        return false;
    });

    $("#btn-gis-siguiente").on("click", function (e) {
        $("#Message-Error strong").empty();
        $("#Message-Error").hide();
        //Validar Formulario
        var mensaje = "";
        var NombreProyecto = $("#Nom_Iniciativa").val();
        var DescripcipnProyecto = $("#Des_Iniciativa").val();

        if (NombreProyecto == null || NombreProyecto == "") {
            mensaje += Constantes.Message.FaltaNombreProyecto + Constantes.SaltoHtml;
        }
        if (DescripcipnProyecto == null || DescripcipnProyecto == "") {
            mensaje += Constantes.Message.FaltaDescripcionProyecto + Constantes.SaltoHtml;
        }

        if (mensaje != null && $.trim(mensaje) != "") {
            MostrarMensajeError(mensaje);
            return false;
        }

        $("#Indicador").val("1");
        $("#FormGIS").submit();
        return false;
    });


    ///Ventanas Popup
    $("#btn-etp-adicional").on("click", function () {
        //Listar Requerimientos
        var parametros = {
            "Iniciativa": { "Cod_Iniciativa": $("#Cod_Iniciativa").val(), }
        }
        ListarRequerimientoxIniciativa(parametros);
        $("#PanelReqAct").show();
        $("#PanelResultados").hide();
        $("#btn-etp-continuar-reqact").hide();
        $("#BusquedaActividades").empty();
        $("#ResultadosRequerimientoActividades").empty();
        $("#BlockActivyFilter").hide();
        $("#BlockActivyAdd").hide();
        $("#ModalEstimarTiempoProyecto").modal("show");


    });

    $("#btn-ecp-adicional").on("click", function () {
        //Listar Competencias
        var parametros = {
            "Iniciativa": { "Cod_Iniciativa": $("#Cod_Iniciativa").val(), }
        }

        $("#ResultadosTiempoxFases").empty();
        ListarCompetenciasxIniciativa(parametros);
        $("#btn-ecp-grabar").hide();
        $("#ModalConsultoresProyecto").modal("show");
    });

    $("#btn-etp-agregar-req").on("click", function () {
        var parametros = {
            "Iniciativa": { "Cod_Iniciativa": $("#Cod_Iniciativa").val(), },
            "Nom_Requerimiento": $("#Nom_Requerimiento").val(),
            "ComplejidadRequerimiento": { "Cod_Complejidad_Requerimiento": $("#ComplejidadRequerimiento_Cod_Complejidad_Requerimiento").val(), },
        }
        GrabarRequerimiento(parametros);

        //Limpiar
        $("#Nom_Requerimiento").val("");
        $("#ComplejidadRequerimiento_Cod_Complejidad_Requerimiento").eq(0).prop('selected', true);
        $("#BlockActivyFilter").hide();
        $("#BlockActivyAdd").hide();
        $("#BusquedaActividades").empty();
        $("#ResultadosRequerimientoActividades").empty();
    });

    $("#btn-etp-buscar-act").on("click", function () {
        var parametros = { "Nom_Actividad": $("#Nom_Actividad").val() };
        BuscarActividades(parametros);
        $("#Nom_Actividad").val("");
    });

    $("#btn-etp-agregar-act").on("click", function () {
        var parametros = {
            "Requerimiento": { "Cod_Requerimiento": $("#Cod_Requerimiento").val() },
            "Actividad": { "Cod_Actividad": $("#Cod_Actividad").val() },
            "ComplejidadActividad": { "Cod_Complejidad_Actividad": $("#ComplejidadActividad_Cod_Complejidad_Actividad").val() },
            "Cantidad": $("#Cantidad").val()
        };
        GrabarRequerimientoActividad(parametros);
    });

    $("#btn-etp-continuar-reqact").on("click", function () {
        var parametros = {
            "Requerimiento": {
                "Iniciativa": {
                    "Cod_Iniciativa": $("#Cod_Iniciativa").val(),
                }
            }
        }
        RequerimientoActividadExisteActividad(parametros);
    });

    $("#btn-etp-regresar-reqact").on("click", function () {
        $("#ResultadosTiempoConstruccion").empty();
        $("#ResultadosTiempoxFases").empty();
        $("#btn-etp-continuar-reqact").hide();
        $("#BusquedaActividades").empty();
        $("#ResultadosRequerimientoActividades").empty();
        $("#BlockActivyFilter").hide();
        $("#BlockActivyAdd").hide();
        $("h4.modal-title").empty();
        $("h4.modal-title").html("Calcular Tiempo de Ejecución");
        $("#PanelResultados").hide();
        $("#PanelReqAct").show();
        $(".ckbreq").prop("checked", false);

    });


    ////Estimacion de Consultores del Proyecyo

    $("#btn-competencia-agregar").on("click", function () {
        var parametros = {
            "Iniciativa": { "Cod_Iniciativa": $("#Cod_Iniciativa").val(), },
            "Competencia": { "Cod_Competencia": $("#Cod_Competencia").val(), },
            "NivelCompetencia": { "Cod_Nivel_Competencia": $("#Cod_Nivel_Competencia").val(), },
            "Negocio": { "Cod_Negocio": $("#Negocio_Cod_Negocio").val(), },
            "Horas_Participacion": $("#Horas_Participacion").val(),
        }


        GrabarCompetencia(parametros);

        //Limpiar
        //$("#Nom_Requerimiento").val("");
        //$("#ComplejidadRequerimiento_Cod_Complejidad_Requerimiento").eq(0).prop('selected', true);
        //$("#BlockActivyFilter").hide();
        //$("#BlockActivyAdd").hide();
        //$("#BusquedaActividades").empty();
        //$("#ResultadosRequerimientoActividades").empty();
    });

    $("#btn-ver-tiempos-proyecto").on("click", function () {

        var parametros = {
            "Iniciativa": { "Cod_Iniciativa": $("#Cod_Iniciativa").val(), }
        };
        ListarTiempoFases(parametros);

    });

    $("#btn-ecp-grabar").on("click", function () {
        if ($("#IndiCompetencia").val() == "1") {
            PopInformativo("Se guardaron los cambios")
        } else {
            PopInformativo("El Total de Horas de Participación ingresadas no coincide con el Total de Horas Estimadas...")
        }
    });

    ////Evaluacion de Reantabilidad
    $("#btn-er-adicional").on("click", function () {
        $("#ModalEvaluarRentabilidad").modal("show");
    });

    $("#FormNuevaIniciativa").submit(function (e) {
        $("#Message-Error strong").empty();
        $("#Message-Error").hide();
        //Validar Formulario
        var mensaje = "";
        var NombreProyecto = $("#Nom_Iniciativa").val();
        var DescripcipnProyecto = $("#Des_Iniciativa").val();
        if (NombreProyecto == null || NombreProyecto == "") {
            mensaje += Constantes.Message.FaltaNombreProyecto + Constantes.SaltoHtml;
        }
        if (DescripcipnProyecto == null || DescripcipnProyecto == "") {
            mensaje += Constantes.Message.FaltaDescripcionProyecto + Constantes.SaltoHtml;
        }
        if (mensaje != null && $.trim(mensaje) != "") {
            MostrarMensajeError(mensaje);
            return false;
        }

        //Consultar Controlador
        var info = new Object();
        info.metodo = this.method;
        info.serviceURL = this.action;
        info.parametros = new FormData(this);
        if ($("#Indicador").val() == "") {
            $("#Indicador").val("0");
        }

        GrabarOportunidad(e, info);
    });

    $("#FormARS").submit(function (e) {
        $("#Message-Error strong").empty();
        $("#Message-Error").hide();
        //Validar Formulario
        var mensaje = "";
        var NombreProyecto = $("#Nom_Iniciativa").val();
        var DescripcipnProyecto = $("#Des_Iniciativa").val();
        var RFP = $("#file").val();
        if (NombreProyecto == null || NombreProyecto == "") {
            mensaje += Constantes.Message.FaltaNombreProyecto + Constantes.SaltoHtml;
        }
        if (DescripcipnProyecto == null || DescripcipnProyecto == "") {
            mensaje += Constantes.Message.FaltaDescripcionProyecto + Constantes.SaltoHtml;
        }
        if (mensaje != null && $.trim(mensaje) != "") {
            MostrarMensajeError(mensaje);
            return false;
        }

        //Consultar Controlador
        var info = new Object();
        info.metodo = this.method;
        info.serviceURL = this.action;
        info.parametros = new FormData(this);
        if ($("#Indicador").val() == "") {
            $("#Indicador").val("0");
        }

        GrabarResponsableServicio(e, info);
    });

    $("#FormRFP").submit(function (e) {
        $("#Message-Error strong").empty();
        $("#Message-Error").hide();
        //Validar Formulario
        var mensaje = "";
        var NombreProyecto = $("#Nom_Iniciativa").val();
        var DescripcipnProyecto = $("#Des_Iniciativa").val();
        var RFP = $("#file").val();
        if (NombreProyecto == null || NombreProyecto == "") {
            mensaje += Constantes.Message.FaltaNombreProyecto + Constantes.SaltoHtml;
        }
        if (DescripcipnProyecto == null || DescripcipnProyecto == "") {
            mensaje += Constantes.Message.FaltaDescripcionProyecto + Constantes.SaltoHtml;
        }
        if (mensaje != null && $.trim(mensaje) != "") {
            MostrarMensajeError(mensaje);
            return false;
        }


        //Consultar Controlador
        var info = new Object();
        info.metodo = this.method;
        info.serviceURL = this.action;
        info.parametros = new FormData(this);
        if ($("#Indicador").val() == "") {
            $("#Indicador").val("0");
        }

        GrabarRevisionRFP(e, info);
    });

    $("#FormACL").submit(function (e) {
        $("#Message-Error strong").empty();
        $("#Message-Error").hide();
        //Validar Formulario
        var mensaje = "";
        var NombreProyecto = $("#Nom_Iniciativa").val();
        var DescripcipnProyecto = $("#Des_Iniciativa").val();
        var RFP = $("#file").val();
        if (NombreProyecto == null || NombreProyecto == "") {
            mensaje += Constantes.Message.FaltaNombreProyecto + Constantes.SaltoHtml;
        }
        if (DescripcipnProyecto == null || DescripcipnProyecto == "") {
            mensaje += Constantes.Message.FaltaDescripcionProyecto + Constantes.SaltoHtml;
        }
        if (mensaje != null && $.trim(mensaje) != "") {
            MostrarMensajeError(mensaje);
            return false;
        }


        //Consultar Controlador
        var info = new Object();
        info.metodo = this.method;
        info.serviceURL = this.action;
        info.parametros = new FormData(this);
        if ($("#Indicador").val() == "") {
            $("#Indicador").val("0");
        }

        GrabarConsultorLider(e, info);
    });

    $("#FormETP").submit(function (e) {
        $("#Message-Error strong").empty();
        $("#Message-Error").hide();
        //Validar Formulario
        var mensaje = "";
        var NombreProyecto = $("#Nom_Iniciativa").val();
        var DescripcipnProyecto = $("#Des_Iniciativa").val();
        var RFP = $("#file").val();
        if (NombreProyecto == null || NombreProyecto == "") {
            mensaje += Constantes.Message.FaltaNombreProyecto + Constantes.SaltoHtml;
        }
        if (DescripcipnProyecto == null || DescripcipnProyecto == "") {
            mensaje += Constantes.Message.FaltaDescripcionProyecto + Constantes.SaltoHtml;
        }
        if (mensaje != null && $.trim(mensaje) != "") {
            MostrarMensajeError(mensaje);
            return false;
        }


        //Consultar Controlador
        var info = new Object();
        info.metodo = this.method;
        info.serviceURL = this.action;
        info.parametros = new FormData(this);
        if ($("#Indicador").val() == "") {
            $("#Indicador").val("0");
        }

        GrabarEstimacionTiempoProyectos(e, info);
    });

    $("#FormECP").submit(function (e) {
        $("#Message-Error strong").empty();
        $("#Message-Error").hide();
        //Validar Formulario
        var mensaje = "";
        var NombreProyecto = $("#Nom_Iniciativa").val();
        var DescripcipnProyecto = $("#Des_Iniciativa").val();
        var RFP = $("#file").val();
        if (NombreProyecto == null || NombreProyecto == "") {
            mensaje += Constantes.Message.FaltaNombreProyecto + Constantes.SaltoHtml;
        }
        if (DescripcipnProyecto == null || DescripcipnProyecto == "") {
            mensaje += Constantes.Message.FaltaDescripcionProyecto + Constantes.SaltoHtml;
        }
        if (mensaje != null && $.trim(mensaje) != "") {
            MostrarMensajeError(mensaje);
            return false;
        }


        //Consultar Controlador
        var info = new Object();
        info.metodo = this.method;
        info.serviceURL = this.action;
        info.parametros = new FormData(this);
        if ($("#Indicador").val() == "") {
            $("#Indicador").val("0");
        }

        GrabarEstimacionConsultores(e, info);
    });

    $("#FormER").submit(function (e) {
        $("#Message-Error strong").empty();
        $("#Message-Error").hide();
        //Validar Formulario
        var mensaje = "";
        var NombreProyecto = $("#Nom_Iniciativa").val();
        var DescripcipnProyecto = $("#Des_Iniciativa").val();
        var RFP = $("#file").val();
        if (NombreProyecto == null || NombreProyecto == "") {
            mensaje += Constantes.Message.FaltaNombreProyecto + Constantes.SaltoHtml;
        }
        if (DescripcipnProyecto == null || DescripcipnProyecto == "") {
            mensaje += Constantes.Message.FaltaDescripcionProyecto + Constantes.SaltoHtml;
        }
        if (mensaje != null && $.trim(mensaje) != "") {
            MostrarMensajeError(mensaje);
            return false;
        }


        //Consultar Controlador
        var info = new Object();
        info.metodo = this.method;
        info.serviceURL = this.action;
        info.parametros = new FormData(this);
        if ($("#Indicador").val() == "") {
            $("#Indicador").val("0");
        }

        GrabarEvaluacionRentabilidad(e, info);
    });

    $("#FormPT").submit(function (e) {
        $("#Message-Error strong").empty();
        $("#Message-Error").hide();
        //Validar Formulario
        var mensaje = "";
        var NombreProyecto = $("#Nom_Iniciativa").val();
        var DescripcipnProyecto = $("#Des_Iniciativa").val();
        if (NombreProyecto == null || NombreProyecto == "") {
            mensaje += Constantes.Message.FaltaNombreProyecto + Constantes.SaltoHtml;
        }
        if (DescripcipnProyecto == null || DescripcipnProyecto == "") {
            mensaje += Constantes.Message.FaltaDescripcionProyecto + Constantes.SaltoHtml;
        }
        if (mensaje != null && $.trim(mensaje) != "") {
            MostrarMensajeError(mensaje);
            return false;
        }


        //Consultar Controlador
        var info = new Object();
        info.metodo = this.method;
        info.serviceURL = this.action;
        info.parametros = new FormData(this);
        if ($("#Indicador").val() == "") {
            $("#Indicador").val("0");
        }

        GrabarDesarrollarPropuestaTecnica(e, info);
    });

    $("#FormRPT").submit(function (e) {
        $("#Message-Error strong").empty();
        $("#Message-Error").hide();
        //Validar Formulario
        var mensaje = "";
        var NombreProyecto = $("#Nom_Iniciativa").val();
        var DescripcipnProyecto = $("#Des_Iniciativa").val();
        if (NombreProyecto == null || NombreProyecto == "") {
            mensaje += Constantes.Message.FaltaNombreProyecto + Constantes.SaltoHtml;
        }
        if (DescripcipnProyecto == null || DescripcipnProyecto == "") {
            mensaje += Constantes.Message.FaltaDescripcionProyecto + Constantes.SaltoHtml;
        }
        if (mensaje != null && $.trim(mensaje) != "") {
            MostrarMensajeError(mensaje);
            return false;
        }


        //Consultar Controlador
        var info = new Object();
        info.metodo = this.method;
        info.serviceURL = this.action;
        info.parametros = new FormData(this);
        if ($("#Indicador").val() == "") {
            $("#Indicador").val("0");
        }

        GrabarRevisarPropuestaTecnica(e, info);
    });

    $("#FormEPT").submit(function (e) {
        $("#Message-Error strong").empty();
        $("#Message-Error").hide();
        //Validar Formulario
        var mensaje = "";
        var NombreProyecto = $("#Nom_Iniciativa").val();
        var DescripcipnProyecto = $("#Des_Iniciativa").val();
        if (NombreProyecto == null || NombreProyecto == "") {
            mensaje += Constantes.Message.FaltaNombreProyecto + Constantes.SaltoHtml;
        }
        if (DescripcipnProyecto == null || DescripcipnProyecto == "") {
            mensaje += Constantes.Message.FaltaDescripcionProyecto + Constantes.SaltoHtml;
        }
        if (mensaje != null && $.trim(mensaje) != "") {
            MostrarMensajeError(mensaje);
            return false;
        }


        //Consultar Controlador
        var info = new Object();
        info.metodo = this.method;
        info.serviceURL = this.action;
        info.parametros = new FormData(this);
        if ($("#Indicador").val() == "") {
            $("#Indicador").val("0");
        }

        GrabarEnviarPropuestaTecnica(e, info);
    });

    $("#FormERC").submit(function (e) {
        $("#Message-Error strong").empty();
        $("#Message-Error").hide();
        //Validar Formulario
        var mensaje = "";
        var NombreProyecto = $("#Nom_Iniciativa").val();
        var DescripcipnProyecto = $("#Des_Iniciativa").val();
        if (NombreProyecto == null || NombreProyecto == "") {
            mensaje += Constantes.Message.FaltaNombreProyecto + Constantes.SaltoHtml;
        }
        if (DescripcipnProyecto == null || DescripcipnProyecto == "") {
            mensaje += Constantes.Message.FaltaDescripcionProyecto + Constantes.SaltoHtml;
        }
        if (mensaje != null && $.trim(mensaje) != "") {
            MostrarMensajeError(mensaje);
            return false;
        }


        //Consultar Controlador
        var info = new Object();
        info.metodo = this.method;
        info.serviceURL = this.action;
        info.parametros = new FormData(this);
        if ($("#Indicador").val() == "") {
            $("#Indicador").val("0");
        }

        GrabarRespuestaCliente(e, info);
    });

    $("#FormGIS").submit(function (e) {
        $("#Message-Error strong").empty();
        $("#Message-Error").hide();
        //Validar Formulario
        var mensaje = "";
        var NombreProyecto = $("#Nom_Iniciativa").val();
        var DescripcipnProyecto = $("#Des_Iniciativa").val();
        if (NombreProyecto == null || NombreProyecto == "") {
            mensaje += Constantes.Message.FaltaNombreProyecto + Constantes.SaltoHtml;
        }
        if (DescripcipnProyecto == null || DescripcipnProyecto == "") {
            mensaje += Constantes.Message.FaltaDescripcionProyecto + Constantes.SaltoHtml;
        }
        if (mensaje != null && $.trim(mensaje) != "") {
            MostrarMensajeError(mensaje);
            return false;
        }


        //Consultar Controlador
        var info = new Object();
        info.metodo = this.method;
        info.serviceURL = this.action;
        info.parametros = new FormData(this);
        if ($("#Indicador").val() == "") {
            $("#Indicador").val("0");
        }

        GrabarGestionarInicioServicio(e, info);
    });


    $("#FormCO").submit(function (e) {
        $("#Message-Error strong").empty();
        $("#Message-Error").hide();
        //Validar Formulario
        var mensaje = "";
        var NombreProyecto = $("#Nom_Iniciativa").val();
        var DescripcipnProyecto = $("#Des_Iniciativa").val();
        var RFP = $("#file").val();
        if (NombreProyecto == null || NombreProyecto == "") {
            mensaje += Constantes.Message.FaltaNombreProyecto + Constantes.SaltoHtml;
        }
        if (DescripcipnProyecto == null || DescripcipnProyecto == "") {
            mensaje += Constantes.Message.FaltaDescripcionProyecto + Constantes.SaltoHtml;
        }
        if (mensaje != null && $.trim(mensaje) != "") {
            MostrarMensajeError(mensaje);
            return false;
        }


        //Consultar Controlador
        var info = new Object();
        info.metodo = this.method;
        info.serviceURL = this.action;
        info.parametros = new FormData(this);
        if ($("#Indicador").val() == "") {
            $("#Indicador").val("0");
        }

        GrabarCierreOportunidad(e, info);
    });


});

function BuscarOportunidad(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.BuscarIniciativa;
    info.parametros = parametros;

    ajaxPartialView(info, function (data) {
        if (data != null && data != "") {
            $("#ResultadoOportunidades").html(data);
        } else {
            $("#ResultadoOportunidades").empty();
        }
    });
}

function GrabarOportunidad(e, info) {
    e.preventDefault();
    //Consultar Controlador
    ajaxForm(info, function (data) {
        if (data != null) {
            PopInformativo(data.Message);
            if (data.Estado == true) {
                $("#btn-nuevo-descargar").show();
                var datos = data.Valor.split("|");;
                $("#Cod_Iniciativa").val(datos[0]);
                $("#File").val(datos[1]);
                if ($("#Indicador").val() == "1") {
                    var Cod_Iniciativa = $("#Cod_Iniciativa").val();
                    location.href = "/Iniciativa/AsignarResposnableServicio/" + Cod_Iniciativa
                }
            }

        } else {
            console.log("error")
        }
    });
}

function GrabarResponsableServicio(e, info) {
    e.preventDefault();
    //Consultar Controlador
    ajaxForm(info, function (data) {
        if (data != null) {
            PopInformativo(data.Message);
            if (data.Estado == true && $("#Indicador").val() == "1") {
                var Cod_Iniciativa = $("#Cod_Iniciativa").val();
                location.href = "/Iniciativa/RevisarRFP/" + Cod_Iniciativa
            }
        } else {
            console.log("error")
        }
    });
}

function GrabarRevisionRFP(e, info) {
    e.preventDefault();
    //Consultar Controlador
    ajaxForm(info, function (data) {
        if (data != null) {
            PopInformativo(data.Message);
            if (data.Estado == true && $("#Indicador").val() == "1") {
                var Cod_Iniciativa = $("#Cod_Iniciativa").val();
                location.href = "/Iniciativa/AsignarConsultorLider/" + Cod_Iniciativa
            }
        } else {
            console.log("error")
        }
    });
}

function GrabarConsultorLider(e, info) {
    e.preventDefault();
    //Consultar Controlador
    ajaxForm(info, function (data) {
        if (data != null) {
            PopInformativo(data.Message);
            if (data.Estado == true && $("#Indicador").val() == "1") {
                var Cod_Iniciativa = $("#Cod_Iniciativa").val();
                location.href = "/Iniciativa/EstimarTiemposProyecto/" + Cod_Iniciativa
            }
        } else {
            console.log("error")
        }
    });
}

function GrabarEstimacionTiempoProyectos(e, info) {
    e.preventDefault();
    //Consultar Controlador
    ajaxForm(info, function (data) {
        if (data != null) {
            PopInformativo(data.Message);
            if (data.Estado == true && $("#Indicador").val() == "1") {
                var Cod_Iniciativa = $("#Cod_Iniciativa").val();
                location.href = "/Iniciativa/EstimarConsultoresProyecto/" + Cod_Iniciativa
            }
        } else {
            console.log("error")
        }
    });
}

function GrabarEstimacionConsultores(e, info) {
    e.preventDefault();
    //Consultar Controlador
    ajaxForm(info, function (data) {
        if (data != null) {
            PopInformativo(data.Message);
            if (data.Estado == true && $("#Indicador").val() == "1") {
                var Cod_Iniciativa = $("#Cod_Iniciativa").val();
                location.href = "/Iniciativa/EvaluarRentabilidad/" + Cod_Iniciativa
            }
        } else {
            console.log("error")
        }
    });
}

function GrabarEvaluacionRentabilidad(e, info) {
    e.preventDefault();
    //Consultar Controlador
    ajaxForm(info, function (data) {
        if (data != null) {
            PopInformativo(data.Message);
            if (data.Estado == true && $("#Indicador").val() == "1") {
                var Cod_Iniciativa = $("#Cod_Iniciativa").val();
                location.href = "/Iniciativa/DesarrollarPropuestaTecnica/" + Cod_Iniciativa
            }
        } else {
            console.log("error")
        }
    });
}

function GrabarDesarrollarPropuestaTecnica(e, info) {
    e.preventDefault();
    //Consultar Controlador
    ajaxForm(info, function (data) {
        if (data != null) {
            PopInformativo(data.Message);
            if (data.Estado == true && $("#Indicador").val() == "1") {
                var Cod_Iniciativa = $("#Cod_Iniciativa").val();
                location.href = "/Iniciativa/RevisarPropuestaTecnica/" + Cod_Iniciativa
            }
        } else {
            console.log("error")
        }
    });
}

function GrabarRevisarPropuestaTecnica(e, info) {
    e.preventDefault();
    //Consultar Controlador
    ajaxForm(info, function (data) {
        if (data != null) {
            PopInformativo(data.Message);
            if (data.Estado == true && $("#Indicador").val() == "1") {
                var Cod_Iniciativa = $("#Cod_Iniciativa").val();
                var indiUrl = $("#IndiUrl").val();
                if (indiUrl == "2")
                    location.href = "/Iniciativa/DesarrollarPropuestaTecnica/" + Cod_Iniciativa;
                else if (indiUrl == "1")
                    location.href = "/Iniciativa/EnviarPropuestaTecnica/" + Cod_Iniciativa;
            }
        } else {
            console.log("error")
        }
    });
}

function GrabarEnviarPropuestaTecnica(e, info) {
    e.preventDefault();
    //Consultar Controlador
    ajaxForm(info, function (data) {
        if (data != null) {
            PopInformativo(data.Message);
            if (data.Estado == true && $("#Indicador").val() == "1") {
                var Cod_Iniciativa = $("#Cod_Iniciativa").val();
                location.href = "/Iniciativa/RespuestaCliente/" + Cod_Iniciativa
            }
        } else {
            console.log("error")
        }
    });
}

function GrabarRespuestaCliente(e, info) {
    e.preventDefault();
    //Consultar Controlador
    ajaxForm(info, function (data) {
        if (data != null) {
            PopInformativo(data.Message);
            if (data.Estado == true && $("#Indicador").val() == "1") {
                var Cod_Iniciativa = $("#Cod_Iniciativa").val();
                var indiUrl = $("#IndiUrl").val();
                if (indiUrl == "2")
                    location.href = "/Iniciativa/DesarrollarPropuestaTecnica/" + Cod_Iniciativa;
                else if (indiUrl == "1")
                    location.href = "/Iniciativa/GestionarInicioServicio/" + Cod_Iniciativa;
            }
        } else {
            console.log("error")
        }
    });
}

function GrabarGestionarInicioServicio(e, info) {
    e.preventDefault();
    //Consultar Controlador
    ajaxForm(info, function (data) {
        if (data != null) {
            PopInformativo(data.Message);
            if (data.Estado == true && $("#Indicador").val() == "1") {
                var Cod_Iniciativa = $("#Cod_Iniciativa").val();
                location.href = "/Iniciativa/CerrarOportunidad/" + Cod_Iniciativa;
            }
        } else {
            console.log("error")
        }
    });
}

//Estimacion de Tiempo del Proyecto

function ListarRequerimientoxIniciativa(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.ListarRequerimientoxIniciativa;
    info.parametros = parametros;

    ajaxPartialView(info, function (data) {
        if (data != null && data != "") {
            $("#ResultadosRequerimientos").html(data);
            //$("#TablaResultadosRequerimientos  tbody").paginathing({
            //    perPage: 5,
            //    insertAfter: '.table',
            //});
            $(".btn-eliminar-requerimiento").on("click", function () {
                var parametros = {
                    "Cod_Requerimiento": $(this).attr("data-id"),
                    "Iniciativa": { "Cod_Iniciativa": $("#Cod_Iniciativa").val(), }
                }
                EliminarRequerimiento(parametros);
                $("#BlockActivyFilter").hide();
                $("#BlockActivyAdd").hide();
            });
            $(".ckbreq").on("click", function () {
                $("#Cod_Requerimiento").val($(this).attr("data-id"));
                $("#Nom_Actividad").val("");
                $("#BusquedaActividades").empty();
                $("#ResultadosRequerimientoActividades").empty();
                $("#BlockActivyAdd").hide();
                $("#btn-etp-continuar-reqact").hide();
                var parametros = {
                    "Requerimiento": { "Cod_Requerimiento": $("#Cod_Requerimiento").val() },
                };
                ListarRequerimientoActividadxRequerimiento(parametros);
                $("#BlockActivyFilter").show();
            });
        } else {
            $("#ResultadosRequerimientos").empty();
        }
    });
}

function GrabarRequerimiento(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.GrabarRequerimiento;
    info.parametros = parametros;
    ajax(info, function (data) {
        if (data != null) {
            if (data.Estado == true)
                ListarRequerimientoxIniciativa(parametros);
            PopInformativo(data.Message);
            $("#btn-etp-continuar-reqact").hide();
        } else {
            PopInformativo("Ocurrio un error al guardar el requerimiento...");
        }
    });
}

function EliminarRequerimiento(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.EliminarRequerimiento;
    info.parametros = parametros;
    ajax(info, function (data) {
        if (data != null) {
            if (data.Estado == true)
                ListarRequerimientoxIniciativa(parametros);
            PopInformativo(data.Message);
        } else {
            PopInformativo("Ocurrio un error al eliminar el requerimiento...");
        }
    });
}

function BuscarActividades(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.BuscarActividades;
    info.parametros = parametros;

    ajaxPartialView(info, function (data) {
        if (data != null && data != "") {
            $("#BusquedaActividades").html(data);
            //$("#TablaResultadosActividades  tbody").paginathing({
            //    perPage: 5,
            //    insertAfter: '.table',
            //});

            $(".ckbact").on("click", function () {
                $("#Cod_Actividad").val($(this).attr("data-id"));
                $("#ComplejidadActividad_Cod_Complejidad_Actividad").eq(0).prop('selected', true);
                $("#Cantidad").val(1);
                $("#btn-etp-regresar-reqact").hide();
                $("#BlockActivyAdd").show();
            });
        } else {
            $("#BusquedaActividades").empty();
        }
    });
}

function GrabarRequerimientoActividad(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.GrabarRequerimientoActividad;
    info.parametros = parametros;
    ajax(info, function (data) {
        if (data != null) {
            if (data.Estado == true)
                ListarRequerimientoActividadxRequerimiento(parametros);
            PopInformativo(data.Message);
        } else {
            PopInformativo("Ocurrio un error al guardar el requerimiento...");
        }
    });
}

function EliminarRequerimientoActividad(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.EliminarRequerimientoActividad;
    info.parametros = parametros;
    ajax(info, function (data) {
        if (data != null) {
            if (data.Estado == true)
                ListarRequerimientoActividadxRequerimiento(parametros);
            PopInformativo(data.Message);
        } else {
            PopInformativo("Ocurrio un error al eliminar el requerimiento...");
        }
    });
}

function ListarRequerimientoActividadxRequerimiento(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.ListarRequerimientoActividadxRequerimiento;
    info.parametros = parametros;

    ajaxPartialView(info, function (data) {
        if (data != null && $.trim(data) != "") {
            $("#ResultadosRequerimientoActividades").html(data);
            $("#btn-etp-continuar-reqact").show();
            $(".btn-eliminar-reqact").on("click", function () {
                var parametros = {
                    "Cod_Requerimiento_Actividad": $(this).attr("data-id"),
                    "Requerimiento": { "Cod_Requerimiento": $("#Cod_Requerimiento").val(), }
                }
                EliminarRequerimientoActividad(parametros);
            });
        } else {
            $("#ResultadosRequerimientoActividades").empty();
        }
    });
}

function RequerimientoActividadExisteActividad(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.RequerimientoActividadExisteActividad;
    info.parametros = parametros;
    ajax(info, function (data) {
        if (data != null) {
            if (data.Estado == true) {
                $("h4.modal-title").empty();
                $("h4.modal-title").html("Resultados de Estimación");
                var parametros = {
                    "Requerimiento": {
                        "Iniciativa": {
                            "Cod_Iniciativa": $("#Cod_Iniciativa").val(),
                        }
                    }
                }
                BuscarRequerimientoxIniciativa(parametros);
                var parametros = {
                    "Iniciativa": {
                        "Cod_Iniciativa": $("#Cod_Iniciativa").val(),
                    }
                }
                GenerarTiempoFases(parametros);
                $("#PanelReqAct").hide();
                $("#PanelResultados").show();
                $("#btn-etp-continuar-reqact").hide();
                $("#btn-etp-regresar-reqact").show();
                $("#BusquedaActividades").empty();
                $("#ResultadosRequerimientoActividades").empty();
                $("#BlockActivyFilter").hide();
                $("#BlockActivyAdd").hide();
            } else {
                PopInformativo(data.Message);
            }
        } else {
            PopInformativo("Ocurrio un error al guardar el requerimiento...");
        }
    });
}

function ValidarActividades(parametros, Indicador, FormETP) {
    var estado = false;
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.RequerimientoActividadExisteActividad;
    info.parametros = parametros;
    ajax(info, function (data) {
        if (data != null) {
            if (data.Estado == false) {
                PopInformativo(data.Message);
            } else {
                $(Indicador).val("1");
                $(FormETP).submit();
            }
        }
    });
}

function BuscarRequerimientoxIniciativa(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.BuscarRequerimientoxIniciativa;
    info.parametros = parametros;
    ajaxPartialView(info, function (data) {
        if (data != null && $.trim(data)) {
            $("#ResultadosTiempoConstruccion").html(data);
        } else {
            $("#ResultadosTiempoConstruccion").empty();
        }
    });
}

function GenerarTiempoFases(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.GenerarTiempoFases;
    info.parametros = parametros;
    ajax(info, function (data) {
        if (data != null) {
            if (data.Estado) {
                ListarTiempoFases(parametros);
            } else
                PopInformativo(data.Message);
        } else {
            $("#ResultadosTiempoxFases").empty();
        }
    });
}

function ListarTiempoFases(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.ListarTiempoFases;
    info.parametros = parametros;
    ajaxPartialView(info, function (data) {
        if (data != null && $.trim(data)) {
            $("#ResultadosTiempoxFases").html(data);
            $("#btn-etp-siguiente").prop("disabled", false);
        } else {
            $("#ResultadosTiempoxFases").empty();
        }
    });
}

//Estimacion Consultores del Proyecto


function ListarCompetenciasxIniciativa(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.ListarCompetenciasxIniciativa;
    info.parametros = parametros;

    ajaxPartialView(info, function (data) {
        if (data != null && $.trim(data) != "") {
            $("#ResultadoCompetencias").html(data);
            $(".btn-cancelar-competencia").hide();
            $(".btn-guardar-competencia").hide();
            $("#btn-ecp-grabar").show();
            $(".btn-eliminar-competencia").on("click", function () {
                var parametros = {
                    "Cod_Iniciativa_Competencia": $(this).attr("data-id"),
                    "Iniciativa": { "Cod_Iniciativa": $("#Cod_Iniciativa").val(), }
                }
                EliminarCompetencia(parametros);
            });

            $(".btn-editar-competencia").on("click", function () {
                $(this).parents("td").prev().find("input").prop("readonly", false).css({ "border": "1px solid #000" });
                var contenido = $(this).parents("td").prev().find("span").html();
                console.log(contenido);
                $(".btn-eliminar-competencia").hide();
                $(".btn-editar-competencia").hide();
                $(".btn-cancelar-competencia").show();
                $(".btn-guardar-competencia").show();
            });

            $(".btn-cancelar-competencia").on("click", function () {
                $(this).parents("td").prev().find("input").prop("readonly", true).css({ "border": "none" });
                var span = $(this).parents("td").prev().find("span").html();
                $(this).parents("td").prev().find("input").val(span);
                $(".btn-eliminar-competencia").show();
                $(".btn-editar-competencia").show();
                $(".btn-cancelar-competencia").hide();
                $(".btn-guardar-competencia").hide();
            });

            $(".btn-guardar-competencia").on("click", function () {
                var input = $(this).parents("td").prev().find("input");
                var span = $(this).parents("td").prev().find("span").html();
                var parametros = {
                    "Iniciativa": { "Cod_Iniciativa": $("#Cod_Iniciativa").val() },
                    "Cod_Iniciativa_Competencia": $(this).attr("data-id"),
                    "Horas_Participacion": input.val(),
                    "Horas_Participacion_Antes": span,
                }
                ModificarCompetencia(parametros);
                $(this).parents("td").prev().find("input").prop("readonly", true).css({ "border": "none" });
                $(this).parents("td").prev().find("span").empty();
                $(this).parents("td").prev().find("span").html(input.val());
                $(".btn-eliminar-competencia").show();
                $(".btn-editar-competencia").show();
                $(".btn-cancelar-competencia").hide();
                $(".btn-guardar-competencia").hide();
            });

            $(".btn-cerrar").on("click", function () {
                if ($("#IndiCompetencia").val() == 0) {
                    $(".btn-eliminar-competencia").trigger("click");
                }
            });

        } else {
            $("#ResultadoCompetencias").empty();
        }
    });
}

function ValidarConsultores(parametros, Indicador, FormECP) {
    var estado = false;
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.ListarCompetenciasxIniciativa;
    info.parametros = parametros;
    ajaxPartialView(info, function (data) {
        if (data != null && $.trim(data) != "") {
            $(Indicador).val("1");
            $(FormECP).submit();
        } else {
            PopInformativo("No puede continuar, sin consultores agregados...");
        }
    });
}

function GrabarCompetencia(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.GrabarCompetencia;
    info.parametros = parametros;
    ajax(info, function (data) {
        if (data != null) {
            if (data.Estado == true)
                ListarCompetenciasxIniciativa(parametros);
            PopInformativo(data.Message);
        } else {
            PopInformativo("Ocurrio un error al guardar la competencia...");
        }
    });
}

function EliminarCompetencia(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.EliminarCompetencia;
    info.parametros = parametros;
    ajax(info, function (data) {
        if (data != null) {
            if (data.Estado == true)
                ListarCompetenciasxIniciativa(parametros);
            //$("#btn-ecp-continuar").hide();
            //PopInformativo(data.Message);
        } else {
            PopInformativo("Ocurrio un error al eliminar la competencia...");
        }
    });
}

function ModificarCompetencia(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.ModificarCompetencia;
    info.parametros = parametros;
    ajax(info, function (data) {
        if (data != null) {
            PopInformativo(data.Message);
        } else {
            PopInformativo("Ocurrio un error al modificar la competencia...");
        }
    });
}


//Evaluar  Rentabilidad

function CalcularCostoEquipo(parametros) {


    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.CalcularCostoEquipo;
    info.parametros = parametros;

    ajaxPartialView(info, function (data) {
        if (data != null && $.trim(data) != "") {
            $("#Resultado").show();
            $("#IdViewCostoTotalEquipo").html(data);
            $("#MensajeCarga").html("")
            $("#MensajeCarga").html("Calculando el costo total del servicio...")
            setTimeout(function () {
                var parametros = { "Codigo": $("#Cod_Iniciativa").val() };
                CalcularCostoServicio(parametros);
            }, 2000);

        } else {
            $("#IdViewCostoTotalEquipo").html("");
        }
    });

}

function CalcularCostoServicio(parametros) {


    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.CalcularCostoServicio;
    info.parametros = parametros;

    ajaxPartialView(info, function (data) {
        if (data != null && $.trim(data) != "") {
            $("#IdViewCostoTotalServicio").html(data);
            $("#MensajeCarga").html("")
            $("#MensajeCarga").html("Calculando precio estimado para el cliente...")
            setTimeout(function () {
                var parametros = { "Codigo": $("#Cod_Iniciativa").val() };
                CalcularCostoEquipoCliente(parametros);
            }, 2000);
        } else {
            $("#IdViewCostoTotalServicio").html("");
        }
    });
}

function CalcularCostoEquipoCliente(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.CalcularCostoEquipoCliente;
    info.parametros = parametros;

    ajaxPartialView(info, function (data) {
        if (data != null && $.trim(data) != "") {
            $("#IdViewCostoTotalCliente").html(data);

            //Calcular Ganancia Bruta
            $("#MensajeCarga").html("")
            $("#MensajeCarga").html("Calculando rentabilidad neta del servicio...")
            setTimeout('CalcularRentabilidadServicio();', 2000);

            //CalcularMedidaServicio(parametros);
        } else {
            $("#IdViewCostoTotalCliente").html("");
        }
    });
}

function CalcularRentabilidadServicio() {
    var costototalequipo = parseFloat($("#IdCostoEquipo").val());
    var costototalservicio = parseFloat($("#IdCostoServicio").val());
    var costototalcliente = parseFloat($("#IdCostoCliente").val());

    var gananciabruta = (costototalcliente - (costototalequipo + costototalservicio));
    $("#IdGananciaBruta").val(gananciabruta.toFixed(2));
    $("#IdViewTotalGananciaBruta").show();
    $("#MensajeCarga").html("")
    $("#MensajeCarga").html("Calculando tamaño del servicio...")
    setTimeout(function () {
        var parametros = { "Codigo": $("#Cod_Iniciativa").val() };
        CalcularMedidaServicio(parametros);
    }, 2000);
}

function CalcularMedidaServicio(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.CalcularTamañoServicio;
    info.parametros = parametros;

    ajaxPartialView(info, function (data) {
        if (data != null && $.trim(data) != "") {
            $("#IdViewMedidaServicio").html(data);

            //Calculando % Rentabilidad Esperado
            $("#MensajeCarga").html("")
            $("#MensajeCarga").html("Calculando % rentabilidad esperada...")
            setTimeout('CalcularRentabilidadEsperada();', 2000);

        } else {
            $("#IdViewMedidaServicio").html("");
        }
    });

}

function CalcularRentabilidadEsperada() {
    var medidaservicio = $("#IdMedidaServicio").val();
    var pocentajerentabilidad = "0%"
    if (medidaservicio == "Pequeño")
        pocentajerentabilidad = "25% (Servicio Pequeño)"
    else if (medidaservicio == "Mediano")
        pocentajerentabilidad = "30% (Servicio Mediano)"
    else if (medidaservicio == "Grande")
        pocentajerentabilidad = "35% (Servicio Grande)"
    $("#IdPorcentajeRentabilidad").val(pocentajerentabilidad);
    $("#IdViewPorcentajeRentabilidad").show();

    //Evaluando Rentabilidad
    $("#MensajeCarga").html("")
    $("#MensajeCarga").html("Calculando evaluacion de cumplimiento de rentabilidad...")
    setTimeout('CalcularEvaluacionRentabilidad();', 2000);
}

function CalcularEvaluacionRentabilidad() {
    var medidaservicio = $("#IdMedidaServicio").val();
    var rentabilidad = parseFloat($("#IdGananciaBruta").val());
    var preciocliente = parseFloat($("#IdCostoCliente").val());
    var porcentaje = 0;
    var porcentajerenta = 0;
    porcentaje = (rentabilidad / preciocliente)
    if (medidaservicio == "Pequeño") {
        porcentajerenta = 0.25
        if (porcentaje >= porcentajerenta) {
            $("#IdEvalRentabilidad").val("Cumple con el 25%");
            $("#IdRentable").val("1");
            $("#MensajeResultado").html("El servicio cumple con la rentabilidad esperada. Coordinar con el Gernte de Operaciones la continuidad de esta oportunidad.");
        }
        else {
            $("#IdEvalRentabilidad").val("No Cumple con el 25%");
            $("#IdRentable").val("0");
            $("#MensajeResultado").html(" El servicio no cumple con la rentabilidad esperada. Coordinar con el Gernte de Operaciones la continuidad de esta oportunidad.");
        }
    }
    else if (medidaservicio == "Mediano") {
        porcentajerenta = 0.3
        if (porcentaje >= porcentajerenta) {
            $("#IdEvalRentabilidad").val("Cumple con el 30%");
            $("#IdRentable").val("1");
            $("#MensajeResultado").html("El servicio cumple con la rentabilidad esperada. Coordinar con el Gernte de Operaciones la continuidad de esta oportunidad.");
        }
        else {
            $("#IdEvalRentabilidad").val("No Cumple con el 30%");
            $("#IdRentable").val("0");
            $("#MensajeResultado").html(" El servicio no cumple con la rentabilidad esperada. Coordinar con el Gernte de Operaciones la continuidad de esta oportunidad.");
        }
    }
    else if (medidaservicio == "Grande") {
        porcentajerenta = 0.35
        if (porcentaje >= porcentajerenta) {
            $("#IdEvalRentabilidad").val("Cumple con el 35%");
            $("#IdRentable").val("1");
            $("#MensajeResultado").html("El servicio cumple con la rentabilidad esperada. Coordinar con el Gernte de Operaciones la continuidad de esta oportunidad.");
        }
        else {
            $("#IdEvalRentabilidad").val("No Cumple con el 35%");
            $("#IdRentable").val("0");
            $("#MensajeResultado").html(" El servicio no cumple con la rentabilidad esperada. Coordinar con el Gernte de Operaciones la continuidad de esta oportunidad.");
        }
    }
    $("#IdViewEvaluandoRentabilidad").show();
    $("#MensajeCarga").html("")
    $("#MensajeCarga").hide();
}

function RegistrarEvaluacionRentabilidad(parametros) {

    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.RegistrarEvaluacionRentabilidad;
    info.parametros = parametros;

    ajax(info, function (data) {
        if (data == true) {
            PopInformativo("Se registro la evaluacion de rentabilidad...")
            $("#idGuardar").prop("disabled", true);
            //$("#btn-er-siguiente").prop("disabled", true);
        } else {
            PopInformativo("No se registro la evaluacion de rentabilidad...")
        }
    });
}

//Desarrollar Propuesta Tecnica

//Cerrar Oportunidad 
function GrabarCierreOportunidad(e, info) {
    e.preventDefault();
    //Consultar Controlador
    ajaxForm(info, function (data) {
        if (data != null) {
            PopInformativo(data.Message);
        } else {
            console.log("error")
        }
    });
}