//Mostrar mensaje si no inicia sesion
var IsAuthenticated = $("#IsAuthenticated").val();
if (IsAuthenticated != "false") {
    $("#mensajePrecios").hide();
} else {
    $("#mensajePrecios").show();
}



$('a.page-scroll').bind('click', function (event) {
    var link = $(this);
    $('html, body').stop().animate({
        scrollTop: $(link.attr('href')).offset().top - 50
    }, 500);
    event.preventDefault();
    $('#navbar').collapse('hide');
});

$(window).scroll(function () {
    if ($(this).scrollTop() >= 50) {        // If page is scrolled more than 50px
        $('#return-to-top').fadeIn(200);    // Fade in the arrow
    } else {
        $('#return-to-top').fadeOut(200);   // Else fade out the arrow
    }
});
$('#return-to-top').click(function () {      // When arrow is clicked
    goTop();
});

function goTop() {
    $('body,html').animate({
        scrollTop: 0                       // Scroll to top of body
    }, 500);
}

function goTopProducto() {
    $('html,body').animate(
        {
            scrollTop: $(".top").offset().top
        }, 'slow');
}
//Funciones seccion Carrito

function cargaBanner(target, idCategoria) {
    var bannerTiempo;
    $('.bxslider-' + target).empty();
    
    var urlPeticion = apiUrl + "api/Banner/ObtenerPorCategoria/" + idCategoria;
    $.ajax({
        url: urlPeticion,
        dataType: "json",
        async: false,
        bannerTiempo: 2 //modifique porque en la ultima imagen llega y se saltaba mauricio pr
    }).done(function (data) {
        //console.log(data);
        bannerTiempo = data[0].Categoria.TiempoBanner;
        if (data == null || data == "") {
            var img = ''
                + '<li>'
                + '<img class="img-responsive" src="' + mvcUrl + '/Images/banner/banner_demo.jpg" >'
                + '</li>';
            //$('.bxslider-' + target).append(img);
            $('.bxslider-' + target).last().append(img);
        }

        else {
            $.each(data, function (index, item) {

                if (item.UrlImagen.length > 0) {
                    //if (index == 0) {
                    //    var img = ''
                    //        + '<li>'
                    //        + '<img class="img-responsive" src ="' + mvcUrl + item.UrlImagen + '" >'
                    //        + '</li>';
                    //} else {
                    //    var img = ''
                    //        + '<li>'
                    //        + '<img class="img-responsive" src ="' + mvcUrl + item.UrlImagen + '" >'
                    //        + '</li>';
                    //}
                    var img = ''
                        + '<li>'
                        + '<img class="img-responsive" src ="' + mvcUrl + item.UrlImagen + '" >'
                        + '</li>';
                    //$('.bxslider-' + target).append(img);
                    $('.bxslider-' + target).last().append(img);
                }
                else {
                    var img = ''
                        + '<li>'
                        + '<img class="img-responsive" src="' + mvcUrl + '/Images/banner/banner_demo.jpg" >'
                        + '</li>';
                    //$('.bxslider-' + target).append(img);
                    $('.bxslider-' + target).last().append(img);
                }
            });
        }

        var sliderInstance = $('.bxslider-' + target).bxSlider({
            autoControls: true,
            mode: 'horizontal',
            //moveSlides: 1,
            slideMargin: 0,
            infiniteLoop: true,
            slideWidth: 2200,
            //minSlides: 1,
            //maxSlides: 1,
            auto: true,
            speed: parseInt(bannerTiempo) * 1000,
            //pause: 2000,
            //autoHover: true,
            pager: true,
            adaptiveHeight: true
        });
        // get the current slide
        var currentSlide = sliderInstance.getCurrentSlide();


        // reload the instance
        sliderInstance.reloadSlider({
            startSlide: currentSlide,
        });
    }).fail(function (data) {
        console.log("error " + data);
    })
        .always(function () {
            //$('.bxslider-' + target).bxSlider({
            //    autoControls: true,
            //    mode: 'horizontal',
            //    moveSlides: 1,
            //    slideMargin: 0,
            //    infiniteLoop: true,
            //    slideWidth: 2200,
            //    minSlides: 1,
            //    maxSlides: 1,
            //    auto: true,
            //    speed: parseInt(bannerTiempo) * 1000,
            //    pause: 2000,
            //    autoHover: true,
            //    pager: true,
            //    adaptiveHeight: true
            //});
        });

    //var urlPeticion = apiUrl + "api/Banner/ObtenerPorCategoria/" + idCategoria;
    //$.ajax({
    //    url: urlPeticion,
    //    dataType: "json",
    //    async: false
    //}).done(function (data) {
    //    //console.log(data);
    //    $.each(data, function (index, item) {
    //        if (item.UrlImagen.length > 0) {
    //                if (index == 0) {
    //                    var img = ''
    //                        + '<div class="item active">'
    //                        + '<img alt="image" class="img-responsive" src ="' + mvcUrl + item.UrlImagen + '" >'
    //                        + '</div>';
    //                } else {
    //                    var img = ''
    //                        + '<div class="item">'
    //                        + '<img alt="image" class="img-responsive" src ="' + mvcUrl + item.UrlImagen + '" >'
    //                        + '</div>';
    //                }
    //            $('.cargar-banner-' + target).append(img);
    //        }
    //        else {
    //            var img = ''
    //                + '<div class="item active">'
    //                + '<img alt="image" class="img-responsive" src ="' + mvcUrl + '/Images/productos/usb.png" >'
    //                + '</div>';
    //            $('.cargar-banner-' + target).append(img);
    //        }
    //    });        
    //})
    //    .fail(function (data) {
    //        console.log("error " + data);
    //    })
    //    .always(function () {
    //    });
}
//Obtiene los ultimos productos registrados
function obtenerNuevosProductos() {
    $('.iboxProducto').children('.ibox-content').toggleClass('sk-loading');
    var urlPeticion = apiUrl + "api/producto/ObtenerNuevosProductos";
    ajaxProducto(urlPeticion, false);
}

//CategoriaId	Descripcion
//1	Memorias USB
//2	Power Bank
//3	Audio
//4	Tecnología
//5	Varios
function obtenerProductosCategoria(categoriaId, pagina) {
    $('.iboxProducto').children('.ibox-content').toggleClass('sk-loading');
    //var urlPeticion = apiUrl + "api/producto/ObtenerProductosCategoria/" + categoriaId;
    var urlPeticion = apiUrl + "api/producto/ObtenerProductosCategoriaPaginado/" + categoriaId + "/" + pagina;
    ajaxProducto(urlPeticion, true);
}

//Variables para buscar
var descripcion;
var color;
var capacidad;
var material;
var buscar = false;

function clickBtnBuscar(categoriaId, pagina) {
    $("#btnBuscarProducto").on("click", function () {
        buscar = true;
        $("#descripcion").val() != "" ? descripcion = $("#descripcion").val() : descripcion = "null";
        $("#color").val() != "" ? color = $("#color").val() : color = "null";
        $("#capacidad").val() != "" ? capacidad = $("#capacidad").val() : capacidad = "null";
        $("#material").val() != "" ? material = $("#material").val() : material = "null";
        //console.log(categoriaId + descripcion + color + capacidad + material + pagina);
        buscarProductosCategoria(categoriaId, pagina);
        dibujarPaginador(categoriaId);
    });
}
///funcion buscador
function buscarProductosCategoria(categoriaId, pagina) {
    $('.iboxProducto').children('.ibox-content').toggleClass('sk-loading');
    //var urlPeticion = apiUrl + "api/producto/ObtenerProductosCategoria/" + categoriaId;
    var urlPeticion = apiUrl + "api/producto/BuscarProductosCategoria/" + categoriaId + "/" + descripcion + "/" + color + "/" + capacidad + "/" + material + "/" + pagina;
    ajaxProducto(urlPeticion, true);
}

$("#btnRecargarPagina").on("click", function () {
    location.reload();
});

//Variable GLOBAL para paginador
var jsonPaginador;
var totalCount;

//Funcion para dibujar Grid Productos
function ajaxProducto(urlPeticion, paginado) {
    totalCount = 0;
    $.ajax({
        url: urlPeticion,
        async: false,
    }).done(function (data, textStatus, xhr) {
        var html = '';
        $("#divGridProducto").empty();
        if (data.length > 0) {
            $.each(data, function (index, item) {

                var IsAuthenticated = $("#IsAuthenticated").val();
                var precio;
                if (IsAuthenticated != "false") {

                    if (tipoPrecio == 1) {
                        precio = item.Precio.length > 0 ? accounting.formatMoney(item.Precio[0].Precio1, "$", 2, ",", ".") : accounting.formatMoney(0, "$", 2, ",", ".");
                    }
                    else if (tipoPrecio == 2) {
                        precio = item.Precio.length > 0 ? accounting.formatMoney(item.Precio[0].Precio2, "$", 2, ",", ".") : accounting.formatMoney(0, "$", 2, ",", ".");
                    }
                    else {
                        precio = item.Precio.length > 0 ? accounting.formatMoney(item.Precio[0].Precio3, "$", 2, ",", ".") : accounting.formatMoney(0, "$", 2, ",", ".");
                    }

                } else {
                    precio = " <i class='fa fa-ban'></i>";
                }

                if (item.ProductoFotografia.length > 0) {
                    html = ''
                        + '<div class="col-md-6 col-lg-3">'
                        + '<div class="ibox">'
                        + '<div class="ibox-content product-box">'
                        + '<div class="product">'
                        + '<img src="' + mvcUrl + item.ProductoFotografia[0].UrlImagen + '" class="img-responsive">'
                        + '</div>'
                        + '<div class="product-desc">'
                        + '<span class="product-price">'
                        + precio
                        + '</span>'
                        + '<small class="text-muted">' + item.Categoria.Descripcion + '</small>'
                        + '<span class="product-name"> Producto</span>'

                        + '<div class="small m-t-xs">'
                        + item.Descripcion
                        + '</div>'

                        + '<div class="m-t text-righ">'
                        + '<a href="DetalleProducto/' + item.ProductoId + '" class="btn btn-xs btn-outline btn-primary" data-id="' + item.ProductoId + '">Detalle <i class="fa fa-long-arrow-right"></i> </a>'
                        + '</div>'
                        + '</div>'
                        + '</div>'
                        + '</div >'
                        + '</div >';
                }
                else {
                    html = ''
                        + '<div class="col-md-6 col-lg-3">'
                        + '<div class="ibox">'
                        + '<div class="ibox-content product-box">'
                        + '<div class="product">'
                        + '<img src="' + mvcUrl + 'Images/categoria/no_producto.png" class="img-responsive">'
                        + '</div>'
                        + '<div class="product-desc">'
                        + '<span class="product-price">'
                        + precio
                        + '</span>'
                        + '<small class="text-muted">' + item.Categoria.Descripcion + '</small>'
                        + '<span class="product-name"> Producto</span>'

                        + '<div class="small m-t-xs">'
                        + item.Descripcion
                        + '</div>'

                        + '<div class="m-t text-righ">'
                        + '<a href="DetalleProducto/' + item.ProductoId + '" class="btn btn-xs btn-outline btn-primary" data-id="' + item.ProductoId + '">Detalle <i class="fa fa-long-arrow-right"></i> </a>'
                        + '</div>'
                        + '</div>'
                        + '</div>'
                        + '</div >'
                        + '</div >';
                }
                $("#divGridProducto").append(html);
            });

            if (paginado) {
                jsonPaginador = JSON.parse(xhr.getResponseHeader("Paging-Headers"));
                //console.log(jsonPaginador);
                totalCount = jsonPaginador.totalCount;
                //console.log("c" + totalCount);
            }
        } else {
            if (buscar) {
                $("#divGridProducto").html("<h1 class='text-warning'>No se encotraron productos...!</h1>");
            } else {
                $("#divGridProducto").html("<h1 class='text-warning'>Aún no hay productos en esta sección...!</h1>");
            }
        }



    })
        .fail(function (data) {
            console.log("error " + data);
        })
        .always(function () {
            $('.iboxProducto').children('.ibox-content').toggleClass('sk-loading');
        });
}


function dibujarPaginador(categoriaId) {
    $(".pagination").empty();
    for (var i = 0; i < jsonPaginador.totalPages; i++) {
        if (i == 0) {
            $(".pagination").append("<li class='activate page-item'><a class='page-link' href='#'>" + (i + 1) + "</a></li>");
        }
        $(".pagination").append("<li class='page-item'><a class='page-link' href='#'>" + (i + 1) + "</a></li>");
    }

    pagination(categoriaId, totalCount);
}

function pagination(categoriaId, totalCount) {
    var urlPeticion;
    $('.pagination').pagination({
        items: totalCount,
        itemsOnPage: 12,
        currentPage: 1,
        hrefTextPrefix: "#",
        prevText: "Anterior",
        nextText: "Siguiente",
        onPageClick: function (pageNumber) {
            buscar == false ? urlPeticion = apiUrl + "api/producto/ObtenerProductosCategoriaPaginado/" + categoriaId + "/" + pageNumber : urlPeticion = apiUrl + "api/producto/BuscarProductosCategoria/" + categoriaId + "/" + descripcion + "/" + color + "/" + capacidad + "/" + material + "/" + pagina;
            ajaxProducto(urlPeticion, true);
            goTopProducto();
        }
    });
}

//Evento modal Open Producto detalle

//$('#detalleProductoModal').on('show.bs.modal', function () {
//    $('.cargar-imagen').empty();
//});
//$('#detalleProductoModal').on('shown.bs.modal', function (event) {
//    $('.iboxDetalleProducto').children('.ibox-content').toggleClass('sk-loading');
//    //setTimeout(function () {  }, 1000);
//    var button = $(event.relatedTarget) // Button that triggered the modal
//    var idProducto = button.data('id') // Extract info from data-* attributes
//    var modal = $(this)
//    var urlPeticion = apiUrl + "api/producto/ObtenerDetalleProducto/" + idProducto;
//    $.ajax({
//        url: urlPeticion,
//        dataType: "json",
//        async: false
//    }).done(function (data) {
//        //console.log(data);
//        modal.find('.modal-nombre-producto').text(data.Nombre ? data.Nombre : "N/A");
//        modal.find('.modal-codigo-producto').text(data.Codigo ? data.Codigo : "N/A");
//        if (data.Precio.length > 0) {
//            modal.find('.product-main-price').text("$" + data.Precio[0].Precio1);
//        } else {
//            modal.find('.product-main-price').text("$0");
//        }
//        modal.find('.modal-descripcion-producto').text(data.Descripcion ? data.Descripcion : "N/A");
//        modal.find('.modal-color-producto').text(data.Color ? data.Color : "N/A");
//        modal.find('.modal-material-producto').text(data.Material ? data.Material : "N/A");
//        modal.find('.modal-capacidad-producto').text(data.Capacidad ? data.Capacidad : "N/A");
//        //modal.find('.modal-existencia-producto').text(data.Existencia ? data.Existencia : "N/A");
//        //modal.find('.modal-reservado-producto').text(data.Reservado ? data.Reservado : "N/A");

//        if (data.ProductoFotografia.length > 0) {
//            $.each(data.ProductoFotografia, function (index, item) {
//                if (index == 0) {
//                    var img = ''
//                        + '<div class="item active">'
//                        + '<img alt="image" class="img-responsive" src ="' + mvcUrl + item.UrlImagen + '" >'
//                        + '</div>';
//                } else {
//                    var img = ''
//                        + '<div class="item">'
//                        + '<img alt="image" class="img-responsive" src ="' + mvcUrl + item.UrlImagen + '" >'
//                        + '</div>';
//                }
//                $('.cargar-imagen').append(img);
//            });
//        }
//        else {
//            var img = ''
//                + '<div class="item active">'
//                + '<img alt="image" class="img-responsive" src ="' + mvcUrl + '/Images/productos/usb.png" >'
//                + '</div>';
//            $('.cargar-imagen').append(img);
//        }
//    })
//        .fail(function (data) {
//            console.log("error " + data);
//        })
//        .always(function () {
//            setTimeout(function () { $('.iboxDetalleProducto').children('.ibox-content').toggleClass('sk-loading'); }, 1000);

//        });

//});

function enviarCorreo() {

}

//Funcion para el slider de las imagenes del producto - INACTIVO
//function slideDetalleProducto() {
//    //$('.product-images').slick('unslick');
//    //$('.product-images').slick('reinit');
//    $('.product-images').slick({
//        centerMode: true,
//        dots: true,
//        speed: 500,
//        fade: true,
//        //autoplay: true,
//        cssEase: 'linear'
//    });

//}