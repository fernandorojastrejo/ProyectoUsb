$("#registerForm").validate({
    rules: {
        Nombres: { required: true },
        ApellidoPaterno: { required: true },
        ApellidoMaterno: { required: true },
        Email: {
            required: true,
            email: true
        },
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
        Nombres: { required: "Campo requerido" },
        ApellidoPaterno: { required: "Campo requerido" },
        ApellidoMaterno: { required: "Campo requerido" },
        Email: {
            required: "Campo requerido",
            email: "Correo electrónico no válido"
        },
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
        $("#errorTextForm").hide();
        $("#btnRegistrar").ladda();
        $("#btnRegistrar").ladda("start");

        var tokenForm = $("input[name='__RequestVerificationToken']").val();
        var urlRegister = mvcUrl + "Account/Register"
        var data = {};

        data.Nombres = $("#Nombres").val();
        data.ApellidoPaterno = $("#ApellidoPaterno").val();
        data.ApellidoMaterno = $("#ApellidoMaterno").val();
        data.Email = $("#Email").val();
        data.Password = $("#Password").val();
        data.ConfirmPassword = $("#ConfirmPassword").val();

        $.ajax({
            url: urlRegister,
            type: "POST",
            data: {
                model: data,
                __RequestVerificationToken: tokenForm

            },
        })
            .done(function (data) {
                if (data == "Ok") {
                    //console.log("success " + data);
                    $("#btnRegistrar").prop({ disabled: true });
                    crearTokenWebApi();
                    mensajeSuccessRegistro("Registro satisfactorio", "Gracias por registrarse", "Carrito");
                } else {
                    $("#errorTextForm .mensaje").html("Error al registrarse, favor de verificar los datos o consultar al administrador");
                    $("#errorTextForm").show("slow");
                }

            })
            .fail(function (data) {
                //console.log("error login " + data);
                $("#errorTextForm .mensaje").html("Un error inesperado ha ocurrido, favor de consultar al administrador");
                $("#errorTextForm").show("slow");
                $("#btnRegistrar").ladda("stop");
            })
            .always(function () {
                //alert("complete");
                $("#btnRegistrar").ladda("stop");
            });
    }
});

function crearTokenWebApi() {

    var usuario = $("#Email").val();
    var contrasenia = $("#Password").val();

    var datos = {
        grant_type: 'password',
        username: usuario,
        password: contrasenia
    };
    var urlPeticion = apiUrl + 'token';

    $.ajax({
        type: 'POST',
        url: urlPeticion,
        data: datos,
    }).done(function (data) {

        // Cache the access token in local storage.
        localStorage.setItem(webApiCliente.nombreToken, data.access_token);

        webApiCliente.nombreToken = 'tokenWebApi';
        webApiCliente.token = localStorage.getItem(webApiCliente.nombreToken);
        webApiCliente.headers = {};
        if (webApiCliente.token) {
            webApiCliente.headers.Authorization = 'Bearer ' + webApiCliente.token;
        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        console.log("error token " + errorThrown);
        $("#errorTextForm .mensaje").html("Un error inesperado ha ocurrido, favor de consultar al administrador");
        $("#errorTextForm").show("slow");
        $("#btnLogin").ladda("stop");
    });
}

function mensajeSuccessRegistro(titulo, msj, redirectUrl) {
    swal({
        title: titulo,
        text: msj,
        type: "success",
        confirmButtonColor: "#1c84c6",
        confirmButtonText: "Ok",
        closeOnCancel: false

    }, function (isConfirm) {
        if (isConfirm) {
            window.location.href = mvcUrl;
        }

    });
}