$(document).ready(function () {
    $("#btn-aceptar").on("click", function () {
        var parametros = {
            "Codigo": $("#Cod_Servicio_Empresarial").val(),
        };
        BuscarRRHH(parametros)
        //return false;
    });
    $("#btn-eliminar-consultor").on("click", function () {
        alert("Hola");
        return false;
    });

});

$("#btn-aceptar").on("click", function () {
    var parametros = {
        "Codigo": $("#Cod_Servicio_Empresarial").val(),
    };
    BuscarRRHH(parametros)
    return false;
});

$("#btn-buscar").on("click", function () {
    var parametros = {
        "Servicio": { "Cod_Servicio": $("#Cod_Servicio").val() },
        "Cliente": { "Cod_Cliente": $("#Cod_Cliente").val() },
        "Nom_Servicio_Empresarial": $("#Nom_Servicio_Empresarial").val(),
    };
    BuscarServiciosEmpresarial(parametros)
    return false;
});

$("#btn-eliminar-consultor").on("click", function () {
    alert("Hola");
    return false;
});


$("#btn-lista-requerimientos").on("click", function () {
    var parametros = {
        "Codigo": $("#Cod_Servicio_Empresarial").val(),
    };

    ListarRequerimientos(parametros)
    return false;
});

function BuscarRRHH(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.BuscarRRHH;
    info.parametros = parametros;

    ajaxPartialView(info, function (data) {
        if (data != null && data != "") {
            $("#Resultados").html(data);

        } else {
            $("#Resultados").html("");
        }

        $("#btn-eliminar-consultor").on("click", function () {
            alert("Hola");
            return false;
        });
    });
}

function BuscarServiciosEmpresarial(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.BuscarServiciosEmpresarial;
    info.parametros = parametros;

    ajaxPartialView(info, function (data) {
        if (data != null && $.trim(data) != "") {
            $("#Resultados").html(data);
        } else {
            $("#Resultados").html("");
        }
    });
}

function ListarRequerimientos(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.ListarRequerimientos;
    info.parametros = parametros;

    ajaxPartialView(info, function (data) {
        if (data != null && $.trim(data) != "") {
            $("#Modal-ListaRequerimientos").html(data);
        } else {
            $("#Modal-ListaRequerimientos").html("");
        }
        $("#ReqConsultor").modal("show");
    });
}

