$(document).ready(function () {

    $("#Resultado").hide();
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

    $("#btn-nuevo-siguiente").on("click", function () {
        var Cod_Iniciativa = $("#Cod_Iniciativa").val();
        location.href = "/Iniciativa/AsignarResposnableServicio/" + Cod_Iniciativa
        return false;
    });

    $("#btn-ars-siguiente").on("click", function () {
        var Cod_Iniciativa = $("#Cod_Iniciativa").val();
        location.href = "/Iniciativa/RevisarRFP/" + Cod_Iniciativa
        return false;
    });
    $("#btn-rfp-siguiente").on("click", function () {
        var Cod_Iniciativa = $("#Cod_Iniciativa").val();
        location.href = "/Iniciativa/AsignarConsultorLider/" + Cod_Iniciativa
        return false;
    });
    $("#btn-acl-siguiente").on("click", function () {
        var Cod_Iniciativa = $("#Cod_Iniciativa").val();
        location.href = "/Iniciativa/EstimarTiemposProyecto/" + Cod_Iniciativa
        return false;
    });
    $("#btn-etp-siguiente").on("click", function () {
        var Cod_Iniciativa = $("#Cod_Iniciativa").val();
        location.href = "/Iniciativa/EstimarConsultoresProyecto/" + Cod_Iniciativa
        return false;
    });

    $("#btn-ecp-siguiente").on("click", function () {
        var Cod_Iniciativa = $("#Cod_Iniciativa").val();
        location.href = "/Iniciativa/EvaluarRentabilidad/" + Cod_Iniciativa
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


    ////Evaluacion de Reantabilidad
    $("#btn-er-adicional").on("click", function () {
        $("#ModalEvaluarRentabilidad").modal("show");
    });


    $("#FormNuevaIniciativa").submit(function (e) {
        e.preventDefault();
        //Consultar Controlador
        var info = new Object();
        info.metodo = this.method;
        info.serviceURL = this.action;
        info.parametros = new FormData(this);
        ajaxForm(info, function (data) {
            if (data != null) {
                PopInformativo(data.Message);
                var array = data.Valor.split("|");
                $("#Cod_Oportunidad").val(array[0]);
                $("#Cod_Iniciativa").val(array[1]);
                $("#btn-nuevo-siguiente").prop("disabled", false);
            } else {
                console.log("error")
            }
        });
    });

    $("#FormARS").submit(function (e) {
        e.preventDefault();
        //Consultar Controlador
        var info = new Object();
        info.metodo = this.method;
        info.serviceURL = this.action;
        info.parametros = new FormData(this);
        ajaxForm(info, function (data) {
            if (data != null) {
                PopInformativo(data.Message);
                $("#btn-ars-siguiente").prop("disabled", false);
            } else {
                console.log("error")
            }
        });
    });

    $("#FormRFP").submit(function (e) {
        e.preventDefault();
        //Consultar Controlador
        var info = new Object();
        info.metodo = this.method;
        info.serviceURL = this.action;
        info.parametros = new FormData(this);
        ajaxForm(info, function (data) {
            if (data != null) {
                PopInformativo(data.Message);
                $("#btn-rfp-siguiente").prop("disabled", false);
            } else {
                console.log("error")
            }
        });
    });

    $("#FormACL").submit(function (e) {
        e.preventDefault();
        //Consultar Controlador
        var info = new Object();
        info.metodo = this.method;
        info.serviceURL = this.action;
        info.parametros = new FormData(this);
        ajaxForm(info, function (data) {
            if (data != null) {
                PopInformativo(data.Message);
                $("#btn-acl-siguiente").prop("disabled", false);
            } else {
                console.log("error")
            }
        });
    });

    $("#FormETP").submit(function (e) {
        e.preventDefault();
        //Consultar Controlador
        var info = new Object();
        info.metodo = this.method;
        info.serviceURL = this.action;
        info.parametros = new FormData(this);
        ajaxForm(info, function (data) {
            if (data != null) {
                PopInformativo(data.Message);
                $("#btn-etp-adicional").prop("disabled", false);
            } else {
                console.log("error")
            }
        });
    });

    $("#FormECP").submit(function (e) {
        e.preventDefault();
        //Consultar Controlador
        var info = new Object();
        info.metodo = this.method;
        info.serviceURL = this.action;
        info.parametros = new FormData(this);
        ajaxForm(info, function (data) {
            if (data != null) {
                PopInformativo(data.Message);
                $("#btn-ecp-siguiente").prop("disabled", false);
            } else {
                console.log("error")
            }
        });
    });

    $("#FormER").submit(function (e) {
        e.preventDefault();
        //Consultar Controlador
        var info = new Object();
        info.metodo = this.method;
        info.serviceURL = this.action;
        info.parametros = new FormData(this);
        ajaxForm(info, function (data) {
            if (data != null) {
                PopInformativo(data.Message);
                $("#btn-er-adicional").prop("disabled", false);
            } else {
                console.log("error")
            }
        });
    });


});

function ListarRequerimientoxIniciativa(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.ListarRequerimientoxIniciativa;
    info.parametros = parametros;

    ajaxPartialView(info, function (data) {
        if (data != null && data != "") {
            $("#ResultadosRequerimientos").html(data);
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
            $(".ckbact").on("click", function () {
                $("#Cod_Actividad").val($(this).attr("data-id"));
                $("#ComplejidadActividad_Cod_Complejidad_Actividad").eq(0).prop('selected', true);
                $("#Cantidad").val(0);
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
                var parametros = {"Requerimiento":{
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
            $("#btn-er-siguiente").prop("disabled", true);
        } else {
            PopInformativo("No se registro la evaluacion de rentabilidad...")
        }
    });
}