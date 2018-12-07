$(document).ready(function () {
    $('.iboxDetalleProducto').children('.ibox-content').toggleClass('sk-loading');
    var idProducto = $("#productoId").val();
    var urlPeticion = apiUrl + "api/producto/ObtenerDetalleProducto/" + idProducto;
    $.ajax({
        url: urlPeticion,
        dataType: "json",
        async: false
    }).done(function (data) {
        //console.log(data);
        $('.modal-nombre-producto').text(data.Nombre ? data.Nombre : "N/A");
        $('.modal-codigo-producto').text(data.Codigo ? data.Codigo : "N/A");

        var IsAuthenticated = $("#IsAuthenticated").val();
        var precio;
        if (IsAuthenticated != "false") {
            if (tipoPrecio == 1) {
                precio = data.Precio.length > 0 ? accounting.formatMoney(data.Precio[0].Precio1, "$", 2, ",", ".") : accounting.formatMoney(0, "$", 2, ",", ".");
            }
            else if (tipoPrecio == 2) {
                precio = data.Precio.length > 0 ? accounting.formatMoney(data.Precio[0].Precio2, "$", 2, ",", ".") : accounting.formatMoney(0, "$", 2, ",", ".");
            }
            else {
                precio = data.Precio.length > 0 ? accounting.formatMoney(data.Precio[0].Precio3, "$", 2, ",", ".") : accounting.formatMoney(0, "$", 2, ",", ".");
            }
            //precio = 2;
        } else {
            precio = " <i class='fa fa-ban'></i>";
            $('.product-main-price').attr("title", "Iniciar sesión para ver el precio");
        }
        $('.product-main-price').html(precio);
        //if (data.Precio.length > 0) {
        //    $('.product-main-price').text("$" + data.Precio[0].Precio1);
        //} else {
        //    $('.product-main-price').text("$0");
        //}
        $('.modal-descripcion-producto').text(data.Descripcion ? data.Descripcion : "N/A");

        if (data.ProductoExistencia.length > 0) {
            $('.modal-existencia-producto').text(data.ProductoExistencia[0].Existente ? data.ProductoExistencia[0].Existente : "N/A");
            $('.modal-reservado-producto').text(data.ProductoExistencia[0].Reservado ? data.ProductoExistencia[0].Reservado : "N/A");
            var subFechaArribo = data.ProductoExistencia[0].FechaArribo;
            var fechaArribo = subFechaArribo.split('T')[0];
            $('.modal-arribo-producto').text(data.ProductoExistencia[0].FechaArribo ? fechaArribo : "N/A");
        }
        

        $('.modal-color-producto').text(data.Color ? data.Color : "N/A");
        $('.modal-material-producto').text(data.Material ? data.Material : "N/A");
        $('.modal-capacidad-producto').text(data.Capacidad ? data.Capacidad : "N/A");
        

        if (data.ProductoFotografia.length > 0) {
            $.each(data.ProductoFotografia, function (index, item) {
                if (index == 0) {
                    var img = ''
                        + '<img class="xzoom img-responsive" src="' + mvcUrl + item.UrlImagen + '" xoriginal="' + mvcUrl + item.UrlImagen + '" width="400"/>';
                        //+ '<div class="item active">'
                        //+ '<img alt="image" class="img-responsive" src ="' + mvcUrl + item.UrlImagen + '" >'
                        //+ '</div>';
                    $("#firstPreview").html(img);

                    var img1 = ''
                        + '<a href="' + mvcUrl + item.UrlImagen + '"><img class="xzoom-gallery" width="80"  src ="' + mvcUrl + item.UrlImagen + '"  xpreview="' + mvcUrl + item.UrlImagen + '"></a>';

                    //+ '<div class="item">'
                    //+ '<img alt="image" class="img-responsive" src ="' + mvcUrl + item.UrlImagen + '" >'
                    //+ '</div>';
                    $('.xzoom-thumbs').append(img1);
                } else {
                    var img2 = ''
                        + '<a href="' + mvcUrl + item.UrlImagen + '"><img class="xzoom-gallery" width="80"  src ="' + mvcUrl + item.UrlImagen + '"></a>';
                    
                        //+ '<div class="item">'
                        //+ '<img alt="image" class="img-responsive" src ="' + mvcUrl + item.UrlImagen + '" >'
                        //+ '</div>';
                    $('.xzoom-thumbs').append(img2);
                }
                //$('.cargar-imagen').append(img);
                
            });
        }
        else {
            var img = ''
                + '<img class="xzoom img-responsive" src ="' + mvcUrl + 'Images/categoria/no_producto.png" xoriginal="' + mvcUrl + 'Images/categoria/no_producto.png" width="400"/>';
                //+ '<div class="item active">'
                //+ '<img alt="image" class="img-responsive" src ="' + mvcUrl + 'Images/categoria/no_producto.png" >'
                //+ '</div>';
            //$('.cargar-imagen').append(img);
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

});