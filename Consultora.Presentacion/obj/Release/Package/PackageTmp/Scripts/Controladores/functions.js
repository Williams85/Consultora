//Rutas
var rutas = {};
rutas.BuscarRRHH = "/ServicioEmpresarial/BuscarRRHH";
rutas.SearchRRHH = "/ServicioEmpresarial/SearchRRHH";
rutas.BuscarServiciosEmpresarial = "/ServicioEmpresarial/BuscarServiciosEmpresarial";
rutas.ListarRequerimientos = "/ServicioEmpresarial/ListarRequerimientos";
rutas.AsignarRRHH = "/ServicioEmpresarial/AsignarRRHH";
rutas.BuscarConsultor = "/ServicioEmpresarial/BuscarConsultor";
rutas.AgregarConsultor = "/ServicioEmpresarial/AgregarConsultor";
rutas.EliminarConsultor = "/ServicioEmpresarial/EliminarConsultor";
//$.datepicker.regional['es'] = {
//    closeText: 'Cerrar',
//    prevText: '<Ant',
//    nextText: 'Sig>',
//    currentText: 'Hoy',
//    monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
//    monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
//    dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
//    dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
//    dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
//    weekHeader: 'Sm',
//    dateFormat: 'dd/mm/yy',
//    firstDay: 1,
//    isRTL: false,
//    showMonthAfterYear: false,
//    yearSuffix: ''
//};
//$.datepicker.setDefaults($.datepicker.regional['es']);

function DisposeEvent(objeto) {
    $(objeto).off();
}

function DisposeEvent(objeto, evento) {
    $(document).off(evento, objeto);
}

function Visible(objeto) {
    $(objeto).show();
}

function Hidden(objeto) {
    $(objeto).hide();
}

function MostrarMensajeError(mensaje) {
    $("#Message-Error strong").empty();
    $("#Message-Error strong").append(mensaje);
    $("#Message-Error").show();

}
function MostrarMensajeOK(mensaje) {
    Hidden("#Message-Error");
    $("#Message-OK").empty();
    $("#Message-OK").append(mensaje);
    $("#Message-OK").show();
}

function PopInformativo(texto) {
    $("#MessagePopup").find("#modalmsg-texto").empty();
    $("#MessagePopup").find("#modalmsg-texto").append(texto);
    $("#MessagePopup").modal();

}

function isValidEmail(mail) {
    return true;
    //return /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,4})+$/.test(mail);
}

var Constantes = {
    SaltoHtml: "<br/>",
    ExpresionRegular: {
        DNI: "^[0-9]+$",
        Pasaporte: "^[ a-zA-ZñÑ0-9áéíóúÁÉÍÓÚ]+$",
        NombresApellidos: "^[A-Za-zÑñaáéíóúÁÉÍÓÚ' ]+$",
        Email: "^[0-9A-ZÑa-zñaáéíóúÁÉÍÓÚ@&.,#/' ]+$",
        Web: "^[0-9A-Za-zñÑaáéíóúÁÉÍÓÚ&._:,#/' ]+$",
        Telefono: "^[-0123456789 ]+$",
        Direccion: "^[0-9A-Za-zñÑaáéíóúÁÉÍÓÚ&.#/°' ]+$",
        Empresa: "^[0-9A-Za-zñÑaáéíóúÁÉÍÓÚ&.' ]+$",
        Ruc: "^[0-9]{11,11}$",
        SoloNumeros: "^[0-9]+$",
        SoloLetras: "^[A-Za-zñÑaáéíóúÁÉÍÓÚ]+$",
        NumerosLetras: "^[0-9A-Za-zñÑaáéíóúÁÉÍÓÚ_ ]+$",
        Clima: "^[0-9CF° ]+$",
        Poblacion: "^[0-9,. ]+$",
        Altitud: "^[0-9A-Z. ]+$",
        NumerosLetrasEspacio: "^[-0123456789A-Za-zñÑaáéíóúÁÉÍÓÚ ]+$",
        Precio: "^[0-9.,]+$",
        Parrafo: "^[0-9A-ZÑa-zñÁÉÍÓÚáéíóú&.,#/' ]+$",
    },
    Message: {
        //Mensaje Mantenimiento Canales
        FaltaNombreCanal: "Ingresar el nombre del canal.",
        FaltaRucCanal: "Ingresar el R.U.C. del canal.",
        FaltaRzCanal: "Ingresar la Razón Social del canal.",
        FaltaDireccionCanal: "Ingresar la dirección del canal.",
        FaltaTelefono1Canal: "Ingresar el teléfono 1 del canal.",
        FaltaTelefono2Canal: "Ingresar el teléfono 2 del canal.",
        FaltaCelularCanal: "Ingresar el celular del canal.",
        FaltaRLCanal: "Ingresar el represenatnte legal del canal.",
        FaltaEmailCanal: "Ingresar el email del representante legal del canal.",
        FaltaFICanal: "Ingresar la fecha inicio de actividades del canal.",
        FaltaFCCanal: "Ingresar la fecha cese de actividades del canal.",
        ErrorEmailCanal: "Error de formato de email del representante legal del canal.",

        ErrorNombreCanal: "Nombre del canal incorrecto.",
        ErrorRucCanal: "R.U.C. del canal incorrecto.",
        ErrorRzCanal: "Razón Social del canal incorrecto.",
        ErrorDireccionCanal: "Dirección del canal incorrecto.",
        ErrorTelefono1Canal: "Teléfono 1 del canal incorrecto.",
        ErrorTelefono2Canal: "Teléfono 2 del canal incorrecto.",
        ErrorCelularCanal: "Celular del canal incorrecto.",
        ErrorRLCanal: "Representante legal del canal incorrecto.",
        ErrorEmailCanal: "Email del representante legal del canal incorrecto.",

        //Mensaje Mantenimiento SubCanales
        FaltaCodigoSubCanal: "Ingresar el codigo del canal.",
        FaltaNombreSubCanal: "Ingresar el nombre del subcanal.",
        FaltaDireccionSubCanal: "Ingresar la dirección del subcanal.",
        FaltaFISubCanal: "Ingresar la fecha inicio de actividades del subcanal.",
        FaltaFCSubCanal: "Ingresar la fecha cese de actividades del subcanal.",

        ErrorNombreSubCanal: "Nombre del subcanal incorrecto.",
        ErrorDireccionSubCanal: "Dirección del subcanal incorrecto.",

        //Mensaje Mantenimiento Vendedores
        FaltaCodigoVendedor: "Ingresar el código del vendedor.",
        FaltaNombreVendedor: "Ingresar los nombres del vendedor.",
        FaltaAPVendedor: "Ingresar el apellido paterno del vendedor.",
        FaltaAMVendedor: "Ingresar el apellido materno del vendedor.",
        FaltaNumDocVendedor: "Ingresar el número de documento del vendedor.",
        FaltaDireccionVendedor: "Ingresar la dirección del vendedor.",
        FaltaTelefono1Vendedor: "Ingresar el teléfono 1 del vendedor.",
        FaltaTelefono2Vendedor: "Ingresar el teléfono 2 del vendedor.",
        FaltaCelularVendedor: "Ingresar el célular del vendedor.",
        FaltaFIVendedor: "Ingresar la fecha inicio de actividades del vendedor.",
        FaltaFCVendedor: "Ingresar la fecha cese de actividades del vendedor.",

        ErrorNombreVendedor: "Nombres del vendedor incorrecto.",
        ErrorAPVendedor: "Apellido paterno del vendedor incorrecto.",
        ErrorAMVendedor: "Apellido materno del vendedor incorrecto.",
        ErrorNumDocVendedor: "Número de documento del vendedor incorrecto.",
        ErrorDireccionVendedor: "Dirección del vendedor incorrecto.",
        ErrorTelefono1Vendedor: "Teléfono 1 del vendedor incorrecto.",
        ErrorTelefono2Vendedor: "Teléfono 2 del vendedor incorrecto.",
        ErrorCelularVendedor: "Célular del vendedor incorrecto.",

        //Mensaje Mantenimiento Reclamo
        FaltaReclamoCodigoSolicitud: "Ingresar la solicitud.",
        FaltaReclamoTipoReclamo: "Ingresar el tipo de reclamo.",
        FaltaReclamoMotivoRechazo: "Ingresar el motivo de rechazo.",
        FaltaReclamoObservacion: "Ingresar las observaciones.",
        FaltaReclamoRespuesta: "Ingresar las respuesta.",
        FaltaReclamoFechaInicio: "Ingresar la fecha de inicio.",
        FaltaReclamoFechaFin: "Ingresar la fecha de fin.",
        FaltaReclamoEstado: "Ingresar el estado.",
    },
};
