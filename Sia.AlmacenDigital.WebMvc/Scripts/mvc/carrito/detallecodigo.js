$(document).ready(function () {
    $('.iboxDetalleProducto').children('.ibox-content').toggleClass('sk-loading');
    cargaDetalleProductoPadre();
    tablaProductos();
    //setTimeout(function () { $('.iboxDetalleProducto').children('.ibox-content').toggleClass('sk-loading'); }, 1000);

});

var codigoProducto = $("#CodigoProducto").val();
var urlPeticion = apiUrl + "api/producto/ObtenerProductosPorCodigo/" + codigoProducto;
var IsAuthenticated = $("#IsAuthenticated").val();
var tipoPrecio = $("#tipoPrecio").val();
//Funcion para cargar los productos en la tabla
function tablaProductos() {    
    var precio;

    var oTable = $('#tblProductos').DataTable({
        destroy: true,
        dom: '<"html5buttons"B>lfrtip',
        responsive: true,
        lengthMenu: [
            [10, 25, 50, 100, -1],
            ['10', '25', '50', '100', 'Todos']
        ],
        buttons: [{
            extend: 'excelHtml5'
        }],
        ajax: {
            "url": urlPeticion,
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
            data: null,
                render: function (data, type, full) {
                if (IsAuthenticated != "false") {

                    if (tipoPrecio == 1) {
                        return "<span class='label label-success'>" + accounting.formatMoney(data.Precio[0].Precio1, "$", 2, ",", ".") + "</span>";
                    }
                    else if (tipoPrecio == 2) {
                        return "<span class='label label-success'>" + accounting.formatMoney(data.Precio[0].Precio2, "$", 2, ",", ".") + "</span>";
                    }
                    else {
                        return "<span class='label label-success'>" + accounting.formatMoney(data.Precio[0].Precio3, "$", 2, ",", ".") + "</span>";
                    }

                } else {
                    return " <i class='fa fa-ban' title='Favor de iniciar sesión para ver el precio'></i>";
                }
            }
        },
        {
            data: null,
            render: function (data, type, full) {
                return '<a href="' + mvcUrl + 'DetalleProducto/' + data.ProductoId + '" data-id="' + data.ProductoId + '" class="btn-info  btn btn-circle" title="Ver detalle producto"><i class="fa fa-th-list"></i></a>';
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

//Fucnion para obtener detalle del producto padre
function cargaDetalleProductoPadre() {
    var urlPeticion = apiUrl + "api/producto/ObtenerProductoDetallePorCodigo/" + codigoProducto;
    $.ajax({
        url: urlPeticion,
        dataType: "json",
        async: false
    }).done(function (data) {
        //console.log(data);
        $('.modal-nombre-producto').text(data.Nombre ? data.Nombre : "N/A");
        $('.modal-codigo-producto').text(data.Codigo ? data.Codigo : "N/A");
        $('.modal-descripcion-producto').text(data.Descripcion ? data.Descripcion : "N/A");


        if (data.ProductoFotografia.length > 0) {
            $.each(data.ProductoFotografia, function (index, item) {
                if (index == 0) {
                    var img = ''
                        + '<img class="xzoom img-responsive" src="' + mvcUrl + item.UrlImagen + '" xoriginal="' + mvcUrl + item.UrlImagen + '" width="400"/>';
                    $("#firstPreview").html(img);

                    var img1 = ''
                        + '<a href="' + mvcUrl + item.UrlImagen + '"><img class="xzoom-gallery" width="80"  src ="' + mvcUrl + item.UrlImagen + '"  xpreview="' + mvcUrl + item.UrlImagen + '"></a>';
                    $('.xzoom-thumbs').append(img1);
                } else {
                    var img2 = ''
                        + '<a href="' + mvcUrl + item.UrlImagen + '"><img class="xzoom-gallery" width="80"  src ="' + mvcUrl + item.UrlImagen + '"></a>';
                    $('.xzoom-thumbs').append(img2);
                }

            });
        }
        else {
            var img = ''
                + '<img class="xzoom img-responsive" src ="' + mvcUrl + 'Images/categoria/no_producto.png" xoriginal="' + mvcUrl + 'Images/categoria/no_producto.png" width="400"/>';
            $("#firstPreview").html(img);
        }
        $('.xzoom, .xzoom-gallery').xzoom({ zoomWidth: 400, title: true, tint: '#333', Xoffset: 15 });
    })
        .fail(function (data) {
            console.log("error " + data);
        })
        .always(function () {
            setTimeout(function () { $('.iboxDetalleProducto').children('.ibox-content').toggleClass('sk-loading'); }, 1000);

        });
}