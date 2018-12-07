
$(document).ready(function () {
    tablaBanner('#tblBannerPrincipal', 6);
    tablaBanner('#tblBannerPromocion', 7);
    tablaBanner('#tblBannerMemorias', 1);
    tablaBanner('#tblBannerPower', 2);
    tablaBanner('#tblBannerAudio', 3);
    tablaBanner('#tblBannerTecnologia', 4);
    tablaBanner('#tblBannerVarios', 5);
    tablaBanner('#tblBannerCatalogo', 100);
    eliminarBanner();
    dropZone();
});

//function cargaProductos() {
//    //$(".chosen-select").chosen({ no_results_text: "Oops, sin resultados!" });
//    var urlPeticion = apiUrl + "api/Producto";
//    $.getJSON(urlPeticion,
//        function (data, textStatus, jqXHR) {
//            var selectProducto = $("#ProductoRegistrarBanner");
//            selectProducto.empty();
//            $.each(data, function (index, item) {
//                selectProducto.append($("<option />").val(item.ProductoId).text(item.Nombre));
//            });
//        }
//    );

//}

//Evento modal Open
$('#registrarBannerTiempoModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget);
    var idCategoria = button.data('categoriaid');
    var modal = $(this);

    cargaDetalleCategoria(idCategoria, "CategoriaIdRegistrarBannerTiempo", "CategoriaRegistrarBannerTiempo");
    agregarBannerTiempo();
});

$('#registrarBannerModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget);
    var idCategoria = button.data('categoriaid');
    var modal = $(this);
    //console.log(idCategoria)
    cargaDetalleCategoria(idCategoria, "CategoriaIdRegistrarBanner", "CategoriaRegistrarBanner");
    //dropZone();
    //$(".chosen-select").chosen({ no_results_text: "Oops, sin resultados!" });
});

function cargaDetalleCategoria(categoriaId, targetId, target) {
    if (categoriaId == 0) {
        //Banner Principal
        $("#" + target).html("Principal");
        $("#" + targetId).val(0);
        $("#CategoriaIdRegistrarBannerTiempo").val(0);

    } else {
        var urlPeticion = apiUrl + "api/Categoria/ObtenerDetalleCategoria/" + categoriaId;
        $.getJSON(urlPeticion,
            function (data, textStatus, jqXHR) {
                $("#" + target).html(data.Descripcion);
                $("#" + targetId).val(data.CategoriaId);
                data.TiempoBanner !== null ? $("#bannerTiempo").val(data.TiempoBanner) : $("#bannerTiempo").val(0);

            }
        );
    }

}
function dropZone() {
    $("#formRegistrarBanner").dropzone({
        url: mvcUrl + "Banner/UploadedFileAsync",
        maxFiles: 1,
        acceptedFiles: '.jpeg, .jpg',
        dictDefaultMessage: "Arrastra los archivos aqui para subirlos en formato JPEG en plataforma <strong>Technology!</strong>",
        init: function () {
            this.on("complete", function (data) {                
                //console.log(data);
                if (data.xhr == undefined) {
                    return;
                }
                var res = JSON.parse(data.xhr.responseText);
                if (res.Message === 'ok') {                   
                    //setTimeout
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
                var categoriaId = $("#CategoriaIdRegistrarBanner").val();
                
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
                var categoriaId = $("#CategoriaIdRegistrarBanner").val();
                agregarTablaBanner(categoriaId, file.name);                
            });
        }
    });
}
function agregarTablaBanner(categoriaId, fname) {

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

    var urlImagen = "Images/banner/" + folderCategoria + "/" + fname
    //console.log(urlImagen);
    var modelo = {
        CategoriaId: categoriaId,
        UrlImagen: urlImagen
    }
    var urlPeticion = apiUrl + "api/Banner/AgregarBanner"

    $.ajax({
        type: 'POST',
        url: urlPeticion,
        data: modelo
    }).done(function (modelo) {
        modelo.Exito == true ? mensajeExito('Se agregó correctamente el banner a la categoría seleccionada') : mensajeError(modelo.MensajeError);
        var table = $(".tab-pane.active table").attr("id");
        var oTable = $("#" + table).DataTable();
        oTable.ajax.reload(null, false);
    }).fail(function (modelo) {
        mensajeError(modelo.MensajeError);
    }).always(function () {
    });
}
function tablaBanner(target, categoriaId) {
    var table = $(target).DataTable({
        destroy: true,
        searching: false,
        paging: false,
        responsive: true,
        ajax: {
            "url": apiUrl + "api/Banner/ObtenerPorCategoria/" + categoriaId,
            "dataSrc": ""
        },
        columns: [{
            data: "BannerId",
            visible: false
        }, {
            data: "Categoria",
            render: function (data, type, full) {
                return '<p><span class="badge badge-success">' + data.Descripcion + '</span></p>';
            }
        }, {
            data: "UrlImagen"
        }, {
            data: null,
            render: function (data, type, full) {
                return '<div class="text-center"><div class="btn-group"><button class="btn btn-danger btn-circle eliminarBanner" type="button" data-id="' + data.BannerId + '" title="Eliminar banner"><i class="fa fa-trash"></i></button></div></div>';
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
}

function agregarBannerTiempo() {
    $("#formRegistrarBannerTiempo").submit(function (event) {
        var modelo = {
            CategoriaId: $("#CategoriaIdRegistrarBannerTiempo").val(),
            TiempoBanner: $("#bannerTiempo").val()
        }
        //console.log(modelo);
        var urlPeticion = apiUrl + "api/Categoria/AgregarBannerTiempo"
        $("#btnRegistrarBannerTiempo").ladda();
        $("#btnRegistrarBannerTiempo").ladda("start");
        $.ajax({
            type: 'PUT',
            url: urlPeticion,
            data: modelo
        }).done(function (modelo) {
            modelo.Exito == true ? mensajeExito(modelo.MensajeExito) : mensajeError(modelo.MensajeError);
        }).fail(function (modelo) {
            mensajeError(modelo.MensajeError);
        }).always(function () {
            $("#btnRegistrarBannerTiempo").ladda("stop");
        });

        event.preventDefault();
    });

}
function eliminarBanner() {
    $("table").on("click", ".eliminarBanner", function (e) {
        var idBanner = $(this).data("id");
        var idTabla = e.delegateTarget.id;
        //console.log(idTabla);
        mensajePreguntaEliminar(idTabla, idBanner);
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
function mensajePreguntaEliminar(idTabla, idBanner) {
    swal({
        title: "Esta seguro de eliminar el banner?",
        text: "¡No podrás recuperar este archivo!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si, eliminar!",
        closeOnConfirm: false
    }, function () {
        $.ajax({
            type: 'POST',
            url: apiUrl + "api/Banner/EliminarBanner/" + idBanner
        })
            .done(function (modelo) {
                if (modelo.Exito == true) {
                    mensajeExito(modelo.MensajeExito)
                    var oTable = $("#" + idTabla).DataTable();
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