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
});

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
            $("#IdMensaje").find("p").html("");
            $("#IdMensaje").find("p").append("Se registro la evaluacion de rentabilidad...");
            $("#IdMensaje").modal("show");
            $("#idGuardar").prop("disabled", true);
        } else {
            $("#IdMensaje").find("p").html("");
            $("#IdMensaje").find("p").append("No se registro la evaluacion de rentabilidad...");
            $("#IdMensaje").modal("show");
        }
    });
}
