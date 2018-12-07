$(document).ready(function () {
    cargaCategoria("#CategoriaRegistrar");
    registrarProducto();   
    $('#Codigo').keyup(function () {
        $("#CodigoPrincipal").val($("#Codigo").val());

    });
});

function cargaCategoria(target) {
    var urlPeticion = apiUrl + "api/Categoria";
    $.getJSON(urlPeticion,
        function (data, textStatus, jqXHR) {
            var categoria = $(target);
            categoria.empty();
            $.each(data, function (index, item) {
                categoria.append($("<option />").val(item.CategoriaId).text(item.Descripcion));
            });
        }
    );
}
$("#EsPrincipal").change(function () {
    if ($("#EsPrincipal").val() == 1) {
        $("#CodigoPrincipal").val($("#Codigo").val());
    } else {
        $("#CodigoPrincipal").val('');
    }
});
function registrarProducto() {
    $("#formRegistrarProducto").validate({
        rules: {
            CategoriaRegistrar: { required: true },
            Codigo: { required: true },
            CodigoBarra: { required: true },
            Nombre: { required: true },
            Pieza: { required: true, digits: true },
            Color: { required: true },
            Capacidad: { required: true },
            Material: { required: true },
            Descripcion: { required: false },
            Medida: { required: true },
            CodigoPrincipal: { required: true },
            EsPrincipal: { required: true }
        },
        messages: {
            CategoriaRegistrar: { required: "Campo requerido" },
            Codigo: { required: "Campo requerido" },
            CodigoBarra: { required: "Campo requerido" },
            Nombre: { required: "Campo requerido" },
            Pieza: { required: "Campo requerido", digits: "Solo se aceptan números" },
            Descripcion: { required: "Campo requerido" },
            Material: { required: "Campo requerido" },
            Color: { required: "Campo requerido" },
            Capacidad: { required: "Campo requerido" },
            Medida: { required: "Campo requerido" },
            CodigoPrincipal: { required: "Campo requerido" },
            EsPrincipal: { required: "Campo requerido" }

        },
        submitHandler: function (form) {
            $("#btnRegistrar").ladda();
            $("#btnRegistrar").ladda("start");
            var urlPeticion = apiUrl + 'api/producto/AgregarProducto';
            var esPrincipal = false;
            if ($("#EsPrincipal").val() == 1)
                esPrincipal = true;

            var modelo = {
                CategoriaId: $("#CategoriaRegistrar").val(),
                Codigo: $("#Codigo").val().toUpperCase(),
                CodigoBarra: $("#CodigoBarra").val().toUpperCase(),
                Nombre: $("#Nombre").val().toUpperCase(),
                Pieza: $("#Pieza").val().toUpperCase(),
                Color: $("#Color").val().toUpperCase(),
                Capacidad: $("#Capacidad").val().toUpperCase(),
                Material: $("#Material").val().toUpperCase(),
                Descripcion: $("#Descripcion").val().toUpperCase(),
                Medida: $("#Medida").val().toUpperCase(),
                EsPrincipal: esPrincipal,
                CodigoPrincipal: $("#CodigoPrincipal").val()
            }

            $.ajax({
                type: 'POST',
                url: urlPeticion,
                data: modelo
            }).done(function (modelo) {
                modelo.Exito == true ? mensajeExito(modelo.MensajeExito) : mensajeError(modelo.MensajeError);
            }).fail(function (modelo) {
                mensajeError(modelo.MensajeError);
            }).always(function () {
                $("#btnRegistrar").ladda("stop");
                $(form)[0].reset();
            });
        }
    });
}
function mensajeError(mensaje) {
    swal({
        title: "Error",
        text: mensaje,
        type: "warning",
        confirmButtonColor: "#DD6B55"
    });
}
function mensajeExito(mensaje) {
    swal({
        title: "Éxito",
        text: mensaje,
        type: "success",
        confirmButtonColor: "#18A689"
    });
}