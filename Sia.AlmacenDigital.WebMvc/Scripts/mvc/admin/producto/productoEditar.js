$(document).ready(function () {
    var productoId = $("#ProductoIdEditar").val();
    cargaCategoria();
    cargaDetalleProductoEditar(productoId);
    editarProducto();
    editarProductoExistencia();
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
$('#CodigoEditar').keyup(function () {
    $("#CodigoPrincipalEditar").val($("#CodigoEditar").val());

});
$("#EsPrincipalEditar").change(function () {
    if ($("#EsPrincipalEditar").val() == 1) {
        $("#CodigoPrincipalEditar").val($("#CodigoEditar").val());
    } else {
        $("#CodigoPrincipalEditar").val('');
    }
});
function cargaDetalleProductoEditar(idProducto) {
    var urlPeticion = apiUrl + "api/producto/ObtenerDetalleProducto/" + idProducto;
    $.getJSON(urlPeticion,
        function (data, textStatus, jqXHR) {
            //$('#ProductoIdEditar').val(data.ProductoId)
            $('#CategoriaEditar').val(data.Categoria.CategoriaId)
            $('#CodigoEditar').val(data.Codigo)
            $('#CodigoBarraEditar').val(data.CodigoBarra)
            $('#NombreEditar').val(data.Nombre)
            $('#PiezaEditar').val(data.Pieza)
            $('#CapacidadEditar').val(data.Capacidad)
            $('#ColorEditar').val(data.Color)
            $('#MaterialEditar').val(data.Material)
            $('#DescripcionEditar').text(data.Descripcion)
            $('#MedidaEditar').val(data.Medida)
            $('#CodigoPrincipalEditar').val(data.CodigoPrincipal)
            if (data.EsPrincipal == 0 || data.EsPrincipal == null)
                $('#EsPrincipalEditar').val(0)
            else $('#EsPrincipalEditar').val(1)
            
            //Preguntar si tiene producto existencia
            if (data.ProductoExistencia.length > 0) {
                //console.log(data.ProductoExistencia)
                $("#ProductoExistenciaIdEditar").val(data.ProductoExistencia[0].ProductoExistenciaId);
                $("#MinimoEditar").val(data.ProductoExistencia[0].Minimo);
                $("#MaximoEditar").val(data.ProductoExistencia[0].Maximo);
                $('#ReservadoEditar').val(data.ProductoExistencia[0].Reservado);
                $('#ExistenteEditar').val(data.ProductoExistencia[0].Existente);
                var subFechaArribo = data.ProductoExistencia[0].FechaArribo;
                var fechaArribo = subFechaArribo.split('T')[0];
                $("#FechaArriboEditar").val(fechaArribo);                
            }
        }
    );
}
function editarProducto() {
    //console.log("Esperando editar...");
    $("#formEditarProducto").validate({
        rules: {
            CategoriaEditar: { required: true },
            CodigoEditar: { required: true },
            CodigoBarraEditar: { required: true },
            NombreEditar: { required: true },
            PiezaEditar: { required: true, digits: true },
            DescripcionEditar: { required: true },
            CapacidadEditar: { required: true },
            ColorEditar: { required: true },
            MaterialEditar: { required: true },
            MedidaEditar: { required: true },
            CodigoPrincipalEditar: { required: true },
            EsPrincipalEditar: { required: true }
        },
        messages: {
            CategoriaEditar: { required: "Campo requerido" },
            CodigoEditar: { required: "Campo requerido" },
            CodigoBarraEditar: { required: "Campo requerido" },
            NombreEditar: { required: "Campo requerido" },
            PiezaEditar: { required: "Campo requerido", digits: "Solo se aceptan números" },
            DescripcionEditar: { required: "Campo requerido" },
            MaterialEditar: { required: "Campo requerido" },
            ColorEditar: { required: "Campo requerido" },
            CapacidadEditar: { required: "Campo requerido" },
            MedidaEditar: { required: "Campo requerido" },
            CodigoPrincipalEditar: { required: "Campo requerido" },
            EsPrincipalEditar: { required: "Campo requerido" }

        },
        submitHandler: function (form) {
            $("#btnActualizar").ladda();
            $("#btnActualizar").ladda("start");
            var urlPeticion = apiUrl + 'api/producto/EditarProducto';
            var esPrincipal = false;
            if ($("#EsPrincipalEditar").val() == 1)
                esPrincipal = true;
            var modelo = {
                ProductoId: $("#ProductoIdEditar").val(),
                CategoriaId: $("#CategoriaEditar").val(),
                Codigo: $("#CodigoEditar").val().toUpperCase(),
                CodigoBarra: $("#CodigoBarraEditar").val().toUpperCase(),
                Nombre: $("#NombreEditar").val().toUpperCase(),
                Pieza: $("#PiezaEditar").val(),
                Capacidad: $("#CapacidadEditar").val().toUpperCase(),
                Color: $("#ColorEditar").val().toUpperCase(),
                Material: $("#MaterialEditar").val().toUpperCase(),
                Descripcion: $("#DescripcionEditar").val().toUpperCase(),
                Medida: $("#MedidaEditar").val().toUpperCase(),
                EsPrincipal: esPrincipal,
                CodigoPrincipal: $("#CodigoPrincipalEditar").val()
            }

            $.ajax({
                type: 'PUT',
                url: urlPeticion,
                data: modelo
            }).done(function (modelo) {
                modelo.Exito == true ? mensajeExito(modelo.MensajeExito) : mensajeError(modelo.MensajeError);
            }).fail(function (modelo) {
                mensajeError(modelo.MensajeError);
            }).always(function () {
                $("#btnActualizar").ladda("stop");
                //$(form)[0].reset();
            });
        }
    });
}

function editarProductoExistencia() {
    //console.log("Esperando editar...");
    $("#formEditarProductoExistencia").validate({
        rules: {
            ProductoExistenciaIdEditar: { required: true },
            MinimoEditar: { required: true, digits: true },
            MaximoEditar: { required: true, digits: true },
            ReservadoEditar: { required: true, digits: true },
            ExistenteEditar: { required: true, digits: true },            
            FechaArriboEditar: { required: true }
        },
        messages: {
            ProductoExistenciaIdEditar: { required: "Campo requerido" },
            MinimoEditar: { required: "Campo requerido" },
            MaximoEditar: { required: "Campo requerido" },
            ReservadoEditar: { required: "Campo requerido" },
            ExistenteEditar: { required: "Campo requerido" },
            FechaArriboEditar: { required: "Campo requerido" }

        },
        submitHandler: function (form) {
            $("#btnActualizarExistencia").ladda();
            $("#btnActualizarExistencia").ladda("start");
            var urlPeticion = apiUrl + 'api/producto/EditarProductoExistencia';
            var modelo = {
                ProductoExistenciaId: $("#ProductoExistenciaIdEditar").val(),
                ProductoId: $("#ProductoIdEditar").val(),
                Minimo: $("#MinimoEditar").val(),
                Maximo: $("#MaximoEditar").val(),                
                Reservado: $("#ReservadoEditar").val(),
                Existente: $("#ExistenteEditar").val(),
                FechaArribo: $("#FechaArriboEditar").val()
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
                $("#btnActualizarExistencia").ladda("stop");
                //$(form)[0].reset();
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
function mensajee(mensaje) {
    swal({
        title: "Usuario",
        text: mensaje,
        type: "warning",
        confirmButtonColor: "#ce9b02"
    });
}