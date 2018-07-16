$(document).ready(function () {
    if ($("#Indicador").val()=="1") {
        var parametros = {};
        BuscarAsignacionesAutomatica(parametros);
    } else {
        var parametros = {
            "Codigo": $("#Cod_Servicio_Empresarial").val(),
        };
        BuscarConsultoresAsignados(parametros);
    }


    $("#buscar-asignaciones-automaticas").on("click", function () {
        var parametros = {};
        BuscarAsignacionesAutomatica(parametros);
    });

    $("#btn-aprobar-asignacion").on("click", function () {
        $("#IdAprobarAsignacion").modal("show");
    });

    $("#btn-rechazar-asignacion").on("click", function () {
        $("#IdRechazarAsignacion").modal("show");
    });

    $("#IdAprobarAsignacion").on("click", function () {
        var parametros = {
            "Codigo": $("#Cod_Servicio_Empresarial").val()
        };
        GrabarAprobacionAsignacionConsultores(parametros);
    });

    $("#RechazarAsignacion").on("click", function () {
        $("#IdRechazarAsignacion").modal("hide");
        $("#ModalRechazarAsignacion").modal("show");
    });

    $("#OKRechazarAsignacion").on("click", function () {
        var parametros = {
            "Cod_Servicio_Empresarial": $("#Cod_Servicio_Empresarial").val(),
            "Motivo_Rechazo": $("#Motivo_Rechazo :selected").text(),
            "Comentario_Rechazo": $("#Comentario_Rechazo").val()
        };
        GrabarRechazoAsignacionConsultores(parametros);
    });

});

//Buscar Asignaciones Automaticas

function BuscarAsignacionesAutomatica(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.BuscarAsignacionesAutomatica;
    info.parametros = parametros;

    ajaxPartialView(info, function (data) {
        $("#Resultados").empty();
        if (data != null && $.trim(data) != "") {
            $("#Resultados").html(data);
        } else {
            $("#Resultados").html("");
            PopInformativo("No existen servicios pendientes de aprobación en estos momentos...");
        }

    });
}

function BuscarConsultoresAsignados(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.BuscarConsultoresAsignados;
    info.parametros = parametros;

    ajaxPartialView(info, function (data) {
        $("#Resultados").empty();
        if (data != null && $.trim(data) != "") {
            $("#Resultados").html(data);
        } else {
            $("#Resultados").html("");
            PopInformativo("No existen servicios pendientes de aprobación en estos momentos...");
        }

    });
}

function GrabarAprobacionAsignacionConsultores(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.GrabarAprobacionAsignacionConsultores;
    info.parametros = parametros;

    ajax(info, function (data) {
        if (data != null && data == true) {
            $("#IdAprobarAsignacion").modal("hide");
            PopInformativo("Se notificó al equipo del servicio...");
            $("#btn-aprobar-asignacion").prop("disabled", true);
            $("#btn-rechazar-asignacion").prop("disabled", true);
        } else {
            PopInformativo("No pudo aprobar la asignación de consultores...");
        }

    });
}

function GrabarRechazoAsignacionConsultores(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.GrabarRechazoAsignacionConsultores;
    info.parametros = parametros;

    ajax(info, function (data) {
        if (data != null && data == true) {
            $("#ModalRechazarAsignacion").modal("hide");
            PopInformativo("Se rechazo la asignación de consultores...");
            $("#btn-aprobar-asignacion").prop("disabled", true);
            $("#btn-rechazar-asignacion").prop("disabled", true);
        } else {
            PopInformativo("No pudo rechazar la asignación de consultores...");
        }

    });
}

