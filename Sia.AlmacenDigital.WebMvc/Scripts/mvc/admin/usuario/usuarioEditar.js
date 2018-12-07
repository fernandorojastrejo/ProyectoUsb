$(document).ready(function () {
    var usuarioId = $("#usuarioIdEditar").val();

    cargaDetalleUsuarioEditar(usuarioId);
    editarUsuario();
    editarUsuarioPassword();
});

function cargaCategoria() {
    var urlPeticion = apiUrl + "api/Categoria";
    $.getJSON(urlPeticion,
        function (data, textStatus, jqXHR) {
            var categoria = $("#CategoriaEditar");
            categoria.empty();
            $.each(data, function (index, item) {
                categoria.append($("<option />").val(item.CategoriaId).text(item.Descripcion));
            });
        }
    );
}
function cargaDetalleUsuarioEditar(usuarioId) {
    var urlPeticion = apiUrl + "api/usuario/ObtenerDetalleUsuario/" + usuarioId;
    $.getJSON(urlPeticion,
        function (data, textStatus, jqXHR) {
            $('#NombreEditar').val(data.Nombres);
            $('#ApEditar').val(data.ApellidoPaterno);
            $('#AmEditar').val(data.ApellidoMaterno);
            $('#CorreoEditar').val(data.Email);
            $("#TipoPrecioEditar").val(data.TipoPrecio);
            $('#Administrador').prop('checked', false);
            $('#UsuarioSistema').prop('checked', false);
            $.each(data.AspNetRoles, function (index, item) {

                
                //if (item.Name == "Administrador") {
                //    console.log("si es");
                //    console.log(item.Name);
                //    $('#Administrador').prop('checked', true);
                //}
                $("#" + item.Name).prop('checked', true);
            });
        }
    );
}
function editarUsuario() {
    //console.log("Esperando editar...");
    $("#formEditarUsuario").validate({
        rules: {
            NombreEditar: { required: true },
            ApEditar: { required: true },
            AmEditar: { required: true },
            CorreoEditar: { required: true, email: true },
            TipoPrecioEditar: { required: true }
        },
        messages: {
            NombreEditar: { required: "Campo requerido" },
            ApEditar: { required: "Campo requerido" },
            AmEditar: { required: "Campo requerido" },
            CorreoEditar: { required: "Campo requerido", email: "Correo electrónico no válido" },
            TipoPrecioEditar: { required: "Campo requerido" }

        },
        submitHandler: function (form) {
            $("#btnActualizar").ladda();
            $("#btnActualizar").ladda("start");
            var urlPeticion = apiUrl + 'api/usuario/EditarUsuario';
            var modelo = {
                Id: $("#usuarioIdEditar").val(),
                Nombres: $('#NombreEditar').val(),
                ApellidoPaterno: $('#ApEditar').val(),
                ApellidoMaterno: $('#AmEditar').val(),
                Email: $("#CorreoEditar").val(),
                UserName: $("#CorreoEditar").val(),
                TipoPrecio: $("#TipoPrecioEditar").val()
            }

            $.ajax({
                type: 'PUT',
                url: urlPeticion,
                data: modelo
            }).done(function (modelo) {
                console.log(modelo);
                modelo == "Ok" ? mensajeExito("Usuario editado correctamente") : mensajeError(modelo);
            }).fail(function (modelo) {
                mensajeError(modelo.MensajeError);
            }).always(function () {
                $("#btnActualizar").ladda("stop");
                //$(form)[0].reset();
            });
        }
    });
}
function editarUsuarioPassword() {
    $("#formEditarUsuarioPassword").validate({
        rules: {
            Password: {
                required: true,
                minlength: 6
            },
            ConfirmPassword: {
                required: true,
                equalTo: "#Password"
            }
        },
        messages: {
            Password: {
                required: "Campo requerido",
                minlength: "El password debe de contener al menos 6 carácteres"
            },
            ConfirmPassword: {
                required: "Campo requerido",
                equalTo: "El password no coincide"
            }

        },
        submitHandler: function (form) {
            $("#btnActualizarPassword").ladda();
            $("#btnActualizarPassword").ladda("start");
            var urlPeticion = apiUrl + 'api/usuario/EditarPasswordUsuario';
            var modelo = {
                Id: $("#usuarioIdEditar").val(),
                Password: $('#ConfirmPassword').val()
            }

            $.ajax({
                type: 'POST',
                url: urlPeticion,
                data: modelo
            }).done(function (modelo) {
                console.log(modelo);
                modelo == "Ok" ? mensajeExito("Contraseña de usuario editado correctamente") : mensajeError(modelo);
            }).fail(function (modelo) {
                mensajeError(modelo.MensajeError);
            }).always(function () {
                $("#btnActualizarPassword").ladda("stop");
                //$(form)[0].reset();
            });
        }
    });
}

//Evento rol
$("input[name='rol']:checkbox").on('change', function (event) {
    var usuarioId = $("#usuarioIdEditar").val();
    //1 = true
    //0 = false
    var estatusCheck = $(this).is(":checked");
    var rol = $(this).attr('id');
    var idRol = $(this).val();
    actualizarRol(usuarioId, rol, estatusCheck);
    //console.log(rol + " " + idRol + " " + estatusCheck)
});

function actualizarRol(usuarioId, idRol, estatusCheck) {
    //si es verdadero insertamos el rol
    //si es falso eliminamos el rol
    var urlPeticion = apiUrl + 'api/usuario/EditarRolUsuario';
 
        var modelo = {
            UserId: usuarioId,
            RoleId: idRol,
            estatusCheck: estatusCheck
        }

        $.ajax({
            type: 'POST',
            url: urlPeticion,
            data: modelo
        }).done(function (modelo) {
            console.log(modelo);
            modelo == "Ok" ? mensajeExito("Rol editado correctamente") : mensajeError(modelo);
        }).fail(function (modelo) {
            mensajeError(modelo.MensajeError);
        }).always(function () {
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
function mensajee(mensaje) {
    swal({
        title: "Usuario",
        text: mensaje,
        type: "warning",
        confirmButtonColor: "#ce9b02"
    });
}