$(document).ready(function () {

    //Asignacion Automatica
    $("#btn-aceptar").on("click", function () {

        if ($.trim($("#Resultados").html()) != "") {
            $("#IdAsignacionAutomatica").modal("show");
        } else {
            var parametros = {
                "Codigo": $("#Cod_Servicio_Empresarial").val(),
            };
            BuscarRRHH(parametros)
        }

        return false;
    });

    $("#AsignacionAutomatica").on("click", function () {
        var parametros = {
            "Codigo": $("#Cod_Servicio_Empresarial").val(),
        };
        SearchRRHH(parametros)
    });


    $("#btn-buscar-consultor").on("click", function () {
        var parametros = {
            "Empleado": {
                "Nom_Empleado": $("#Nom_Empleado").val()
            },
            "Competencia": {
                "Cod_Competencia": $("#Cod_Competencia").val()
            },
            "NivelCompetencia": {
                "Cod_Nivel_Competencia": $("#Cod_Nivel_Competencia").val()
            },
        };
        BuscarConsultor(parametros);
        return false;
    });

    if ($("#IdExisteConsultoresAsignados").val() != 0 && $("#IdExisteConsultoresAsignados").val() != null && $("#IdExisteConsultoresAsignados").val() != "") {
        var parametros = {
            "Codigo": $("#Cod_Servicio_Empresarial").val(),
        };
        BuscarRRHH(parametros);
        $("#btn-aceptar").prop("disabled", true);
        if ($("#IdExisteConsultoresAsignados").val() == "1") {
            $("#IdMensaje").find("p").html("");
            $("#IdMensaje").find("p").append("Algunos consultores no se encuentran disponibles para completar el requerimiento...");
            $("#IdMensaje").modal("show");
        }
    }
});


$("#btn-buscar").on("click", function () {
    var parametros = {
        "Servicio": { "Cod_Servicio": $("#Cod_Servicio").val() },
        "Cod_Servicio_Generado": $("#Cod_Servicio_Generado").val(),
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

            //Detalle del Consultor
            $(".view-consultor").on("click", function () {
                var parametros = { "Codigo": $(this).attr("data-cod-consultor") };
                BuscarConsultorxCodigo(parametros);
            });

            //Guardar Cambios
            $("#guardar-datos").on("click", function () {
                if ($("#IdMaximoConsultores").val() != $("#Resultados table tbody tr").length) {
                    $("#IdMensaje").find("p").html("");
                    $("#IdMensaje").find("p").append("No se ha completado la cantidad requerida de consultores requeridos para el servicio...");
                    $("#IdMensaje").modal("show");
                } else {
                    $("#IdGuardarCambios").modal("show");
                }
                return false;
            });

            $("#GuardarCambios").on("click", function () {
                $("#IdGuardarCambios").modal("hide");
                var parametros = {
                    "Cod_Servicio_Empresarial": $("#Cod_Servicio_Empresarial").val(),
                };
                AsignarRRHH(parametros);
                return false;
            });

            //Eliminar Consultor
            $(".btn-eliminar-consultor").on("click", function () {
                $("#Cod_Servicio_Empresarial_Competencias").val($(this).attr("data-id"));
                $("#IdEliminarConsultor").modal("show");
            });

            $("#EliminarConsultor").on("click", function () {
                var parametros = {
                    "Codigo": $("#Cod_Servicio_Empresarial_Competencias").val(),
                };
                EliminarConsultor(parametros);
            });


            //Buscar Consultores
            $("#buscar-consultor").on("click", function () {
                $("#IdMensaje").find("p").html("");
                if ($("#IdMaximoConsultores").val() == $("#Resultados table tbody tr").length) {
                    $("#IdMensaje").find("p").append("Se superó el límite de consultores que este servicio requiere...");
                    $("#IdMensaje").modal("show");
                } else {
                    $("#FiltrarConsultor").modal("show");
                    $("#Modal-ListaConsultores").empty();
                }
                return false;
            })
            if ($("#IdExisteConsultoresAsignados").val() == "2") {
                $("#guardar-datos").prop("disabled", true);
                $("#buscar-consultor").prop("disabled", true);
                $(".btn-eliminar-consultor").prop("disabled", true);
            }

            if ($("#IdMaximoConsultores").val() != $("#Resultados table tbody tr").length) {
                $("#IdMensaje").find("p").html("");
                $("#IdMensaje").find("p").append("Algunos consultores no se encuentran disponibles para completar el requerimiento...");
                $("#IdMensaje").modal("show");
            }

        } else {
            $("#Resultados").html("");
            $("#IdMensaje").find("p").html("");
            $("#IdMensaje").find("p").append("No se encontraron consultores disponibles que cumplan con los criterios de búsqueda...");
            $("#IdMensaje").modal("show");
        }

    });
}


function SearchRRHH(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.SearchRRHH;
    info.parametros = parametros;

    ajaxPartialView(info, function (data) {
        if (data != null && data != "") {
            $("#Resultados").html(data);

            //Detalle del Consultor
            $(".view-consultor").on("click", function () {
                var parametros = { "Codigo": $(this).attr("data-cod-consultor") };
                BuscarConsultorxCodigo(parametros);
            });

            //Guardar Cambios
            $("#guardar-datos").on("click", function () {
                if ($("#IdMaximoConsultores").val() != $("#Resultados table tbody tr").length) {
                    $("#IdMensaje").find("p").html("");
                    $("#IdMensaje").find("p").append("No se ha completado la cantidad requerida de consultores requeridos para el servicio...");
                    $("#IdMensaje").modal("show");
                } else {
                    $("#IdGuardarCambios").modal("show");
                }
                return false;
            });

            $("#GuardarCambios").on("click", function () {
                $("#IdGuardarCambios").modal("hide");
                var parametros = {};
                AsignarRRHH(parametros);
                return false;
            });

            //Eliminar Consultor
            $(".btn-eliminar-consultor").on("click", function () {
                $("#Cod_Servicio_Empresarial_Competencias").val($(this).attr("data-id"));
                $("#IdEliminarConsultor").modal("show");
            });

            $("#EliminarConsultor").on("click", function () {
                var parametros = {
                    "Codigo": $("#Cod_Servicio_Empresarial_Competencias").val(),
                };
                EliminarConsultor(parametros);
            });


            //Buscar Consultores
            $("#buscar-consultor").on("click", function () {
                $("#IdMensaje").find("p").html("");
                if ($("#IdMaximoConsultores").val() == $("#Resultados table tbody tr").length) {
                    $("#IdMensaje").find("p").append("Se superó el límite de consultores que este servicio requiere...");
                    $("#IdMensaje").modal("show");
                } else {
                    $("#FiltrarConsultor").modal("show");
                }
                return false;
            })
            if ($("#IdExisteConsultoresAsignados").val() == "2") {
                $("#guardar-datos").prop("disabled", true);
                $("#buscar-consultor").prop("disabled", true);
                $(".btn-eliminar-consultor").prop("disabled", true);
            }

            if ($("#IdMaximoConsultores").val() != $("#Resultados table tbody tr").length) {
                $("#IdMensaje").find("p").html("");
                $("#IdMensaje").find("p").append("Algunos consultores no se encuentran disponibles para completar el requerimiento...");
                $("#IdMensaje").modal("show");
            }

        } else {
            $("#Resultados").html("");
            $("#IdMensaje").find("p").html("");
            $("#IdMensaje").find("p").append("No se encontraron consultores disponibles que cumplan con los criterios de búsqueda...");
            $("#IdMensaje").modal("show");
        }
        $("#IdAsignacionAutomatica").modal("hide");
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

function AsignarRRHH(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.AsignarRRHH;
    info.parametros = parametros;

    ajax(info, function (data) {
        $("#IdMensaje").find("p").html("");
        if (data == true) {
            $("#IdMensaje").find("p").append("Se guardaron los cambios satisfatoriamente...");
            $("#guardar-datos").prop("disabled", true);
        } else {
            $("#IdMensaje").find("p").append("No se guardaron los cambios...");
        }
        $("#IdMensaje").modal("show");
    });
}

function BuscarConsultor(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.BuscarConsultor;
    info.parametros = parametros;

    ajaxPartialView(info, function (data) {
        $("#Modal-ListaConsultores").empty();
        if (data != null && $.trim(data) != "") {
            $("#Modal-ListaConsultores").html(data);
            $(".btn-selector-consultor").on("click", function () {
                var parametros = {
                    "Cod_Consultor": $(this).attr("data-consultor"),
                };
                AgregarConsultor(parametros);
                var parametros = {
                    "Codigo": $("#Cod_Servicio_Empresarial").val(),
                };
                BuscarRRHH(parametros);
                return false;
            })
        } else {
            $("#Modal-ListaConsultores").html("");
        }
    });
}

function BuscarConsultorxCodigo(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.BuscarConsultorxCodigo;
    info.parametros = parametros;

    ajaxPartialView(info, function (data) {
        if (data != null && $.trim(data) != "") {
            $("#Modal-ViewConsultor").html(data);
            $("#ViewConsultor").modal("show");
        } else {
            $("#Modal-ViewConsultor").html("");
        }
    });
}



function AgregarConsultor(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.AgregarConsultor;
    info.parametros = parametros;

    ajax(info, function (data) {
        if (data == true) {
            var parametros = {
                "Codigo": $("#Cod_Servicio_Empresarial").val(),
            };
        }
        $("#FiltrarConsultor").modal("hide");
    });
}

function EliminarConsultor(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.EliminarConsultor;
    info.parametros = parametros;

    ajax(info, function (data) {
        if (data == true) {
            var parametros = {
                "Codigo": $("#Cod_Servicio_Empresarial").val(),
            };
            BuscarRRHH(parametros)
        }
        $("#IdEliminarConsultor").modal("hide");
    });
}

