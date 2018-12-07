using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sia.ControlAlmacenDigital.Entidades;
using Sia.ControlAlmacenDigital.WebApi.Negocio;

namespace Sia.ControlAlmacenDigital.WebApi.Controllers
{
    [RoutePrefix("api/ProductoExcel")]
    public class ProductoExcelController : ApiController
    {
        [Route("AgregarEditaExcel")]
        public Resultado<Entidades.Producto> AgregarProducto(Entidades.Producto modelo)
        {
            ProductoExcelNeg neg = new ProductoExcelNeg();
            return neg.AgregarEditar(modelo);
        }
    }
}