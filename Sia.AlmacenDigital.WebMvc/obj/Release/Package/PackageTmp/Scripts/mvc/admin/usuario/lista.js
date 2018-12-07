var table;
$(document).ready(function () {
    $('#ibox1').children('.ibox-content').toggleClass('sk-loading');
    var urlPeticion = apiUrl + 'api/usuario/obtener';

    table = $('#tblUsuarios').DataTable({
        responsive: true,
        dom: '<"html5buttons"B>lfrtip',
        buttons: [{
            extend: 'excelHtml5'
        }],
        "ajax": {
            "type": 'GET',
            'beforeSend': function (request) {
                request.setRequestHeader("Authorization", webApiCliente.headers.Authorization);
            },
            "url": urlPeticion,
            "dataSrc": ''
        },
        columns: [
            {
                data: "Id",
                visible: false
            },
            {
                data: "NombreCompleto"
            },
            {
                data: "Email"
            },
            {
                data: "TipoPrecio"
            },
            {
                data: "Activo",
                render: function (data, type, full) {
                    var htmlActivo = '';
                    if (data == true) {
                        htmlActivo += ' <span class="label label-success" title="Usuario activo"> Activo</span>';
                    } else {
                        htmlActivo += ' <span class="label label-danger" title="Usuario inactivo"> Inactivo</span>';
                    }
                    return htmlActivo;
                }
            },
            {
                data: "AspNetRoles[0].Name",
                render: function (data, trype, full) {
                    var htmlActivo = '';
                    full.AspNetRoles.forEach(function (el) {
                        if (el.Name == 'Administrador') {
                            htmlActivo += ' <span class="label label-success">' + el.Name + '</span>';
                        }
                        else {
                            htmlActivo += ' <span class="label label-default">' + el.Name + '</span>';
                        }
                    });
                    return htmlActivo;
                }
            },
            {
                data: null,
                render: function (data, type, full) {
                    var htmlActivo = '';
                    htmlActivo = '<a href="' + mvcUrl + 'Admin/UsuarioSistema/' + data.Id + '" class="btn-info btn btn-circle" title="Editar usuario"><i class="fa fa-edit"></i></a> <button class="btn-info btn btn-circle btnEstatusUsuario" title="Cambiar estatus de usuario" data-id="' + data.Id + '"><i class="fa fa-check-square-o"></i> </button>';
                    return htmlActivo;
                }
            }],
        language: {
            url: mvcUrl + "locales/es_ES.txt",
            buttons: {
                pageLength: {
                    _: "Mostrando %d registros",
                    '-1': "Todos"
                }
            }
        }
    });
    $('#ibox1').children('.ibox-content').removeClass('sk-loading');
    btnEstatusUsuario();
});

function btnEstatusUsuario() {
    $("table").on("click", ".btnEstatusUsuario", function () {        
        var usuarioId = $(this).data("id");
        var urlPeticion = apiUrl + "api/usuario/EditarEstatusUsuario";
        var modelo = {
            Id: usuarioId
        }

        $.ajax({
            type: 'POST',
            url: urlPeticion,
            data: modelo
        }).done(function (modelo) {
            modelo == "Ok" ? mensajeExito('Se cambio el estatus del usuario') : mensajeError(modelo.MensajeError);
            var oTable = $("#tblUsuarios").DataTable();
            oTable.ajax.reload(null, false);
        }).fail(function (modelo) {
            mensajeError(modelo.MensajeError);
        }).always(function () {
        });
        
        //console.log(usuarioId)
    });
}
function eliminarMensaje(id) {
    swal({
        title: "¡Vas a editar al usuario!",
        text: "¿Estas seguro?",
        type: "info",
        showCancelButton: true,
        confirmButtonColor: "#00c08a",
        confirmButtonText: "¡Sí!",
        cancelButtonText: "Cancelar",
        closeOnConfirm: false
    },
        function (isConfirm) {
            if (isConfirm) {

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