$(document).ready(function () {
    var oTable = $('#tblProductos').DataTable();
    tablaProductos();
    //registrarProducto();
    dropZone();
    btnEstatusProducto();
    //editarProducto();
    //uploadImagenProducto();
});
//Funcion para cargar los productos en la tabla
function tablaProductos() {
    oTable = $('#tblProductos').DataTable({
        destroy: true,
        dom: '<"html5buttons"B>lfrtip',
        //dom: 'Bfrtip',
        responsive: true,
        lengthMenu: [
            [10, 25, 50, 100, -1],
            ['10', '25', '50', '100', 'Todos']
        ],
        //buttons: [{
        //    extend: 'pageLength'
        //}, {
        //    extend: 'excelHtml5'
        //}],   
        buttons: [{
            extend: 'excelHtml5'
        }],
        ajax: {
            "url": apiUrl + "api/producto",
            "dataSrc": ""
        },
        columns: [{
            data: "ProductoId",
            visible: false
        }, {
            data: "Codigo"
        }, {
            data: "CodigoBarra"
        }, {
            data: "Nombre"
        }, {
            data: "Pieza"
        }, {
            data: "Descripcion"
        }, {
            data: "Color"
        }, {
            data: "Capacidad"
        }, {
            data: "Material"
        }, {
            data: "Categoria",
            render: function (data, type, full) {
                return '<p><span class="badge badge-success">' + data.Descripcion + '</span></p>';
            }
        },
        {
            data: "Activo",
            render: function (data, trype, full) {
                var htmlActivo = '';

                if (data == true) {
                    htmlActivo += ' <span class="label label-success" title="Producto activo"> Activo </span>';
                }
                else {
                    htmlActivo += ' <span class="label label-danger" title="Producto inactivo"> Inactivo </span>';
                }
                return htmlActivo;
            }

        }, {
            data: null,
            render: function (data, type, full) {
                return '<a href="#" data-id="' + data.ProductoId + '" class="btn-info  btn btn-circle imagenProducto" data-toggle="modal" data-target="#imagenProductoModal" title="Editar fotos producto"><i class="fa fa-picture-o"></i></a> <a href="' + mvcUrl + 'Admin/Producto/' + data.ProductoId + '" data-id="' + data.ProductoId + '" class="btn-info btn btn-circle editarProducto" title="Editar producto"><i class="fa fa-edit"></i></a> <button class="btn-info btn btn-circle btnEstatusProducto" title="Cambiar estatus de producto" data-id="' + data.ProductoId + '"><i class="fa fa-check-square-o"></i> </button>';
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
}

function cargaCategoria(target) {
    //$("#Categoria").select2({
    //    dropdownParent: $("#registrarProductoModal").parent(),
    //    escapeMarkup: function (m) {
    //        return m;
    //    },
    //    placeholder: "Seleccione categoria",
    //    allowClear: true
    //});
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
function registrarProducto() {
    //console.log("Esperando registrar...");
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
            Descripcion: { required: false }
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
            Capacidad: { required: "Campo requerido" }

        },
        submitHandler: function (form) {
            $("#btnRegistrar").ladda();
            $("#btnRegistrar").ladda("start");
            var urlPeticion = apiUrl + 'api/producto/AgregarProducto';
            //alert(urlPeticion);
            var modelo = {
                CategoriaId: $("#CategoriaRegistrar").val(),
                Codigo: $("#Codigo").val().toUpperCase(),
                CodigoBarra: $("#CodigoBarra").val().toUpperCase(),
                Nombre: $("#Nombre").val().toUpperCase(),
                Pieza: $("#Pieza").val(),
                Color: $("#Color").val(),
                Capacidad: $("#Capacidad").val(),
                Material: $("#Material").val(),
                Descripcion: $("#Descripcion").val().toUpperCase()
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
function editarProducto() {
    //console.log("Esperando editar...");
    $("#formEditarProducto").validate({
        rules: {
            ProductoIdEditar: { required: true },
            CategoriaEditar: { required: true },
            CodigoEditar: { required: true },
            CodigoBarraEditar: { required: true },
            NombreEditar: { required: true },
            PiezaEditar: { required: true, digits: true },
            DescripcionEditar: { required: true },
            CapacidadEditar: { required: true },
            ColorEditar: { required: true },
            MaterialEditar: { required: true }
        },
        messages: {
            ProductoIdEditar: { required: "Campo requerido" },
            CategoriaEditar: { required: "Campo requerido" },
            CodigoEditar: { required: "Campo requerido" },
            CodigoBarraEditar: { required: "Campo requerido" },
            NombreEditar: { required: "Campo requerido" },
            PiezaEditar: { required: "Campo requerido", digits: "Solo se aceptan números" },
            DescripcionEditar: { required: "Campo requerido" },
            MaterialEditar: { required: "Campo requerido" },
            ColorEditar: { required: "Campo requerido" },
            CapacidadEditar: { required: "Campo requerido" }

        },
        submitHandler: function (form) {
            $("#btnActualizar").ladda();
            $("#btnActualizar").ladda("start");
            var urlPeticion = apiUrl + 'api/producto/EditarProducto';
            var modelo = {
                ProductoId: $("#ProductoIdEditar").val(),
                CategoriaId: $("#CategoriaEditar").val(),
                Codigo: $("#CodigoEditar").val().toUpperCase(),
                CodigoBarra: $("#CodigoBarraEditar").val().toUpperCase(),
                Nombre: $("#NombreEditar").val().toUpperCase(),
                Pieza: $("#PiezaEditar").val(),
                Capacidad: $("#CapacidadEditar").val(),
                Color: $("#ColorEditar").val(),
                Material: $("#MaterialEditar").val(),
                Descripcion: $("#DescripcionEditar").val().toUpperCase()
            }

            $.ajax({
                type: 'PUT',
                url: urlPeticion,
                data: modelo
            }).done(function (modelo) {
                modelo.Exito == true ? mensajeExito(modelo.MensajeExito) : mensajeError(modelo.MensajeError);
                oTable.ajax.reload(null, false);
            }).fail(function (modelo) {
                mensajeError(modelo.MensajeError);
            }).always(function () {
                $("#btnActualizar").ladda("stop");
                //$(form)[0].reset();
            });
        }
    });
}
function btnEstatusProducto() {
    $("table").on("click", ".btnEstatusProducto", function () {
        var ProductoId = $(this).data("id");
        var urlPeticion = apiUrl + "api/producto/EditarEstatusProducto";
        var modelo = {
            ProductoId: ProductoId
        }

        $.ajax({
            type: 'PUT',
            url: urlPeticion,
            data: modelo
        }).done(function (modelo) {
            modelo.Exito == true ? mensajeExito('Se cambio el estatus del producto') : mensajeError(modelo.MensajeError);
            var oTable = $("#tblProductos").DataTable();
            oTable.ajax.reload(null, false);
        }).fail(function (modelo) {
            mensajeError(modelo.MensajeError);
        }).always(function () {
        });

        //console.log(usuarioId)
    });
}
function registrarProductoFotografia() {
    //var file = $("#subirImagenProducto").val();
    //var file_ext = file.substr(file.lastIndexOf('.') + 1, file.length);
    var file = $("#subirImagenProducto")[0].files[0]
    //console.log(file.name);
    var modelo = {
        ProductoId: $("#ProductoIdEditarImagen").val(),
        UrlImagen: $("#UrlImagen").val() + file.name,
    }
    var urlPeticion = apiUrl + "api/ProductoFotografia/AgregarProductoFotografia";
    $.ajax({
        type: 'POST',
        url: urlPeticion,
        data: modelo
    }).done(function (modelo) {
        modelo.Exito == true ? mensajeExito(modelo.MensajeExito) : mensajeError(modelo.MensajeError);
    }).fail(function (modelo) {
        mensajeError(modelo.MensajeError);
    }).always(function () {
        $("#btnSubirFoto").ladda("stop");
    });
}
//function subirImagenProducto() {
//    $("#formImagenProducto").submit(function (e) {
//        e.preventDefault();
//        $("#btnSubirFoto").ladda();
//        $("#btnSubirFoto").ladda("start");

//        var data = new FormData();
//        var imagen = $("#subirImagenProducto").get(0).files;
//        data.append("subirImagenProducto", imagen[0]);
//        var urlPeticion = apiUrl + "api/ProductoFotografia/SubirImagenProducto";

//        $.ajax({
//            type: 'POST',
//            processData: false,
//            contentType: false,
//            url: urlPeticion,
//            data: data,
//        }).done(function (modelo) {
//            if (modelo.Exito == true) {
//                mensajeExito(modelo.MensajeExito)
//                registrarProductoFotografia();
//            } else { mensajeError(modelo.MensajeError); }
//        }).fail(function (modelo) {
//            mensajeError(modelo.MensajeError);
//        }).always(function () {
//        });

//    });
//}
function dropZone() {
    $("#formImagenProducto").dropzone({
        url: mvcUrl + "Producto/UploadedFile",
        maxFiles: 1,
        acceptedFiles: '.jpeg, .jpg',
        dictDefaultMessage: "Arrastra los archivos aqui para subirlos en formato JPEG en plataforma <strong>Technology!</strong>",
        init: function () {
            this.on("complete", function (data) {
                //console.log(JSON.stringify(data));
                if (data.xhr == undefined) {
                    return;
                }
                var res = JSON.parse(data.xhr.responseText);
                if (res.Message === 'ok') {
                    var _this = this;
                    _this.removeAllFiles(true);

                }
                else {
                    if (res.Message === 'NoArchivo') {
                        mensajeError('El archivo seleccionado no corresponde a un formato con extensión JPEG, favor de verificar.');
                    }
                    else if (res.Message === 'ErrorArchivo') { mensajeError('Error al procesar el archivo'); }
                    else {
                        mensajeError(res.Message);

                    }
                }
            });
            this.on('sending', function (file, xhr, formData) {
                var productoId = $("#ProductoIdEditarImagen").val();
                var categoriaId = $("#CategoriaProductoIdEditarImagen").val();
                //agregarTablaProducto(categoriaId, productoId, file.name);
                formData.append('productID', productoId);
                formData.append('categoID', categoriaId);
                formData.append('webapi', apiUrl);
                dictDefaultMessage = "Arrastra los archivos aqui para subirlos en formato JPEG en plataforma <strong>Technology!</strong >";
            });
            this.on("processing", function (file) {
                //mensajee('Subiendo imagen por favor espere...');
            });
            this.on("maxfilesexceeded", function (data) {
                var res = eval('(' + data.xhr.responseText + ')');
            });
            this.on("addedfile", function (file) {
                // Create the remove button
                var removeButton = Dropzone.createElement("<center><button class='btn btn-danger btn-circle'> <i class='fa fa-times'></i></button></center>");

                // Capture the Dropzone instance as closure.
                var _this = this;

                // Listen to the click event
                removeButton.addEventListener("click", function (e) {
                    // Make sure the button click doesn't submit the form:
                    e.preventDefault();
                    e.stopPropagation();
                    // Remove the file preview.
                    _this.removeFile(file);
                    // If you want to the delete the file on the server as well,
                    // you can do the AJAX request here.
                });

                // Add the button to the file preview element.
                file.previewElement.appendChild(removeButton);
            });
            this.on("success", function (file, response) {
                //console.log(response);
                //console.log(file);
                var productoId = $("#ProductoIdEditarImagen").val();
                var categoriaId = $("#CategoriaProductoIdEditarImagen").val();
                agregarTablaProducto(categoriaId, productoId, file.name);
            });
        }
    });
}
function agregarTablaProducto(categoriaId, productoId, fname) {

    var folderCategoria;
    //console.log("categoriaId: " + categoriaId);

    switch (categoriaId) {
        case "1":
            folderCategoria = "memoriasUsb";
            break;
        case "2":
            folderCategoria = "powerBank";
            break;
        case "3":
            folderCategoria = "audio";
            break;
        case "4":
            folderCategoria = "tecnologia";
            break;
        case "5":
            folderCategoria = "varios";
            break;
        case "6":
            folderCategoria = "principal";
            break;
        case "7":
            folderCategoria = "promocion";
            break;
        default:
            break;
    }

    var urlImagen = "Images/categoria/" + folderCategoria + "/" + fname
    //console.log(urlImagen);
    var modelo = {
        ProductoId: productoId,
        UrlImagen: urlImagen
    }
    var urlPeticion = apiUrl + "api/ProductoFotografia/AgregarProductoFotografia";

    $.ajax({
        type: 'POST',
        url: urlPeticion,
        data: modelo
    }).done(function (modelo) {
        modelo.Exito == true ? mensajeExito('Se agregó correctamente la fotografía al producto seleccionado') : mensajeError(modelo.MensajeError);
        var oTable = $("#tblProductoFotografia").DataTable();
        oTable.ajax.reload(null, false);
    }).fail(function (modelo) {
        mensajeError(modelo.MensajeError);
    }).always(function () {
    });
}
//Evento modal Open
//Registrar
$('#registrarProductoModal').on('show.bs.modal', function (event) {
    cargaCategoria("#CategoriaRegistrar");
    var validator = $("#formRegistrarProducto").validate();
    validator.resetForm();
});
$('#registrarProductoModal').on('hidden.bs.modal', function (e) {
    //console.log("Limpiado registro");
    $("#formRegistrarProducto")[0].reset();
    oTable.ajax.reload(null, false);
})
//Editar
$('#editarProductoModal').on('show.bs.modal', function (event) {
    cargaCategoria("#CategoriaEditar");
    var validator = $("#formEditarProducto").validate();
    validator.resetForm();
    var button = $(event.relatedTarget); // Button that triggered the modal
    var idProducto = button.data('id'); // Extract info from data-* attributes
    var modal = $(this);
    var urlPeticion = apiUrl + "api/producto/ObtenerDetalleProducto/" + idProducto;
    $.getJSON(urlPeticion,
        function (data, textStatus, jqXHR) {
            modal.find('#ProductoIdEditar').val(data.ProductoId)
            modal.find('#CategoriaEditar').val(data.Categoria.CategoriaId)
            modal.find('#CodigoEditar').val(data.Codigo)
            modal.find('#CodigoBarraEditar').val(data.CodigoBarra)
            modal.find('#NombreEditar').val(data.Nombre)
            modal.find('#PiezaEditar').val(data.Pieza)
            modal.find('#CapacidadEditar').val(data.Capacidad)
            modal.find('#ColorEditar').val(data.Color)
            modal.find('#MaterialEditar').val(data.Material)

            modal.find('#DescripcionEditar').text(data.Descripcion)
        }
    );
    //$.ajax({
    //    url: urlPeticion,
    //}).done(function (data) {
    //    modal.find('#ProductoIdEditar').val(data.ProductoId)
    //    modal.find('#CategoriaEditar').val(data.Categoria.CategoriaId)
    //    modal.find('#CodigoEditar').val(data.Codigo)
    //    modal.find('#CodigoBarraEditar').val(data.CodigoBarra)
    //    modal.find('#NombreEditar').val(data.Nombre)
    //    modal.find('#PiezaEditar').val(data.Pieza)
    //    modal.find('#DescripcionEditar').text(data.Descripcion)
    //})
    //    .fail(function (data) {
    //        console.log("error " + data);
    //    })
    //    .always(function () {
    //    });;
});
//Editar imagen producto
$('#imagenProductoModal').on('show.bs.modal', function (event) {
    //$("#UrlImagen").val(apiUrl + "api/Images/Productos/");
    //subirImagenProducto();

    var button = $(event.relatedTarget); // Button that triggered the modal
    var idProducto = button.data('id'); // Extract info from data-* attributes
    var modal = $(this);

    var urlPeticion = apiUrl + "api/producto/ObtenerDetalleProducto/" + idProducto;
    $.getJSON(urlPeticion,
        function (data, textStatus, jqXHR) {
            //console.log(data)
            modal.find('#ProductoIdEditarImagen').val(data.ProductoId)
            modal.find('#CategoriaProductoIdEditarImagen').val(data.Categoria.CategoriaId)

            $.each(data.ProductoFotografia, function (index, item) {
                //console.log(item.UrlImagen)
            });
            tablaProductosFotografia(data.ProductoId);
        }
    );


});

function tablaProductosFotografia(idProducto) {
    var urlPeticion = apiUrl + "api/ProductoFotografia/ObtenerProductoFotos/" + idProducto;
    var table = $("#tblProductoFotografia").DataTable({
        destroy: true,
        searching: false,
        paging: false,
        responsive: true,
        ajax: {
            "url": urlPeticion,
            "dataSrc": ""
        },
        columns: [{
            data: "ProductoFotoId",
            visible: false
        }, {
            data: "ProductoId",
            visible: false
        }, {
            data: "UrlImagen"
        }, {
            data: null,
            render: function (data, type, full) {
                return '<div class="text-center"><div class="btn-group"><button class="btn btn-danger btn-circle eliminarProductoFoto" type="button" data-id="' + data.ProductoFotoId + '" title="Eliminar foto"><i class="fa fa-trash"></i></button ></div></div>';
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
    table.columns.adjust().draw();
    eliminarFoto();
}

function eliminarFoto() {
    $("table").on("click", ".eliminarProductoFoto", function (e) {
        var ProductoFotoId = $(this).data("id");
        mensajePreguntaEliminar(ProductoFotoId);
    });
}

//function uploadImagenProducto() {
//    Dropzone.options.formImagenProducto = {
//        maxFiles: 1,
//        acceptedFiles: '.jpeg, .jpg',
//        dictDefaultMessage: "Arrastra los archivos aqui para subirlos en formato JPEG en plataforma <strong>Technology!</strong>",
//        init: function () {
//            this.on("complete", function (data) {
//                console.log(JSON.stringify(data.status));
//                //$('#ibox1').children('.ibox-content').toggleClass('sk-loading');
//                if (data.xhr == undefined) {
//                    return;
//                }
//                var res = JSON.parse(data.xhr.responseText);
//                if (res.Message === 'ok') {
//                    mensajeExito('Se agrego correctamente la imagen al producto');

//                    //$('#ibox1').children('.ibox-content').removeClass('sk-loading');

//                }
//                else {
//                    if (res.Message === 'NoArchivo') {
//                        mensajeError('El archivo seleccionado no corresponde a un formato con extensión JPEG, favor de verificar.');
//                    }
//                    else if (res.Message === 'ErrorArchivo') { mensajeError('Error al procesar el archivo'); }
//                    else {
//                        mensajeError(res.Message);

//                    }
//                    //$('#ibox1').children('.ibox-content').removeClass('sk-loading');
//                }
//            });
//            this.on('sending', function (file, xhr, formData) {
//                var productoId = $("#ProductoIdEditarImagen").val();
//                var categoriaId = $("#CategoriaProductoIdEditarImagen").val();


//                console.log(productoId);
//                console.log(categoriaId);
//                console.log(apiUrl);

//                formData.append('productID', productoId);
//                formData.append('categoID', categoriaId);
//                formData.append('webapi', apiUrl);
//                dictDefaultMessage = "Arrastra los archivos aqui para subirlos en formato JPEG en plataforma <strong>Technology!</strong >";
//            });
//            this.on("processing", function (file) {
//                mensajee('Subiendo imagen por favor espere...');

//                //$('#ibox1').children('.ibox-content').toggleClass('sk-loading');
//            });
//            this.on("maxfilesexceeded", function (data) {
//                var res = eval('(' + data.xhr.responseText + ')');
//            });
//            this.on("addedfile", function (file) {

//                // Create the remove button
//                var removeButton = Dropzone.createElement("<button>Quitar archivo</button>");

//                // Capture the Dropzone instance as closure.
//                var _this = this;

//                // Listen to the click event
//                removeButton.addEventListener("click", function (e) {
//                    // Make sure the button click doesn't submit the form:
//                    e.preventDefault();
//                    e.stopPropagation();
//                    // Remove the file preview.
//                    _this.removeFile(file);
//                    // If you want to the delete the file on the server as well,
//                    // you can do the AJAX request here.
//                });

//                // Add the button to the file preview element.
//                file.previewElement.appendChild(removeButton);
//            });
//        },
//    };
//}

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
function mensajePreguntaEliminar(ProductoFotoId) {
    swal({
        title: "Esta seguro de eliminar la foto del producto?",
        text: "¡No podrás recuperar este archivo!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si, eliminar!",
        closeOnConfirm: false
    }, function () {
        $.ajax({
            type: 'POST',
            url: apiUrl + "api/ProductoFotografia/EliminarProductoFoto/" + ProductoFotoId
        })
            .done(function (modelo) {
                if (modelo.Exito == true) {
                    mensajeExito(modelo.MensajeExito)
                    var oTable = $("#tblProductoFotografia").DataTable();
                    oTable.ajax.reload(null, false);
                } else { mensajeError(modelo.MensajeError); }
            })
            .fail(function (modelo) {
                mensajeError(modelo.MensajeError);
            })
            .always(function () {
            });
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