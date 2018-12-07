using Sia.ControlAlmacenDigital.Entidades;
using Sia.ControlAlmacenDigital.Negocio;
using System.Collections.Generic;
using System.Web.Http;

namespace Sia.ControlAlmacenDigital.Controllers
{
    [RoutePrefix("api/ProductoFotografia")]
    public class ProductoFotografiaController : ApiController
    {
        //[HttpPost]
        [Route("AgregarProductoFotografia")]
        public Resultado<ProductoFotografia> AgregarProductoFotografia(ProductoFotografia modelo)
        {
            ProductoFotografiaNeg neg = new ProductoFotografiaNeg();
            return neg.Agregar(modelo);
        }

        [Route("ObtenerProductoFotos/{id}")]
        public IEnumerable<ProductoFotografia> GetObtenerProductoFotos(int id)
        {
            ProductoFotografiaNeg neg = new ProductoFotografiaNeg();
            return neg.ObtenerProductoFotos(id);
        }

        [Route("EliminarProductoFoto/{id}")]
        public Resultado<ProductoFotografia> EliminarProductoFoto(int id)
        {
            ProductoFotografiaNeg neg = new ProductoFotografiaNeg();
            return neg.Eliminar(id);
        }

        //[HttpPost]
        //[Route("SubirImagenProducto")]
        //public Resultado<ProductoFotografia> SubirImagenProducto()
        //{
        //    ProductoFotografiaNeg neg = new ProductoFotografiaNeg();
        //    return neg.SubirImagenProducto();
        //}
    }
}
