$("#loginForm").validate({
    rules: {
        Email: {
            required: true,
            email: true
        },
        Password: {
            required: true,
            minlength: 5
        }
    },
    messages: {
        Email: {
            required: "Campo requerido",
            email: "Correo eléctronico no válido"
        },
        Password: {
            required: "Campo requerido",
            minlength: "El password debe de contener al menos 5 carácteres"
        },
    },
    submitHandler: function (form) {
        //form.submit();
        $("#errorTextForm").hide();
        $("#btnLogin").ladda();
        $("#btnLogin").ladda("start");

        var tokenForm = $("input[name='__RequestVerificationToken']").val();
        var urlLogin = mvcUrl + "Account/Login"
        var data = {};

        data.Email = $("#Email").val();
        data.Password = $("#Password").val();
        data.RememberMe = false;

        $.ajax({
            url: urlLogin,
            type: "POST",
            data: {
                model: data,
                __RequestVerificationToken: tokenForm

            },
        })
            .done(function (data) {
                if (data == "Ok") {
                    console.log("access " + data);
                    crearTokenWebApi();
                } else {
                    $("#errorTextForm .mensaje").html(data);
                    $("#errorTextForm").show("slow");
                    //setTimeout(function () {alert("Hello"); }, 3000);
                    $("#btnLogin").ladda("stop");
                }

            })
            .fail(function (data) {
                console.log("error login " + data);
                $("#errorTextForm .mensaje").html("Un error inesperado ha ocurrido, favor de consultar al administrador");
                $("#errorTextForm").show("slow");
                $("#btnLogin").ladda("stop");
            })
            .always(function () {
                //alert("complete");
                //$("#btnLogin").ladda("stop");
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
        //
        var returnUrlController = $("#returnUrlController").val();
        if (returnUrlController == "" || returnUrlController == null) {
            window.location.href = mvcUrl;
        } else {
            var res = returnUrlController.substring(1);
            window.location.href = mvcUrl + res;
        }
        //console.log(returnUrlController);
    }).fail(function (jqXHR, textStatus, errorThrown) {
        console.log("error token " + errorThrown);
        $("#errorTextForm .mensaje").html("Un error WEB.API ha ocurrido, favor de consultar al administrador, actualizando formulario...");
        $("#errorTextForm").show("slow");
        $("#btnLogin").ladda("stop");
        setTimeout(function () { location.reload(); }, 3000);
    });
}

//function obtenerUsuario(usuario) {

//    var urlPeticion = apiUrl + 'api/Identidad';

//    $.ajax({
//        url: urlPeticion,
//        data: usuario,
//        headers: webApiCliente.headers
//    }).done(function (data) {
//        console.log(data);
//        //localStorage.setItem('usuarioId', data.Id);
//    }).fail(function (jqXHR, textStatus, errorThrown) {
//        $("#errorTextForm .mensaje").html("Un error inesperado ha ocurrido, favor de consultar al administrador");
//        $("#errorTextForm").show("slow");
//        $("#btnLogin").ladda("stop");
//    });
//}