using System.Collections.Generic;
using System.Web.Http;
using Sia.ControlAlmacenDigital.Negocio;
using Sia.ControlAlmacenDigital.Entidades;

namespace Sia.ControlAlmacenDigital.Controllers
{
    [RoutePrefix("api/Banner")]
    public class BannerController : ApiController
    {
        [Route("AgregarBanner")]
        public Resultado<Banner> AgregarBanner(Banner modelo)
        {
            BannerNeg neg = new BannerNeg();
            return neg.Agregar(modelo);
        }

        [Route("EliminarBanner/{id}")]
        public Resultado<Banner> EliminarBanner(int id)
        {
            BannerNeg neg = new BannerNeg();
            return neg.Eliminar(id);
        }

        [Route("ObtenerPorCategoria/{id}")]
        public IEnumerable<Banner> GetObtenerPorCategoria(int id)
        {
            BannerNeg negocio = new BannerNeg();
            var resultado = negocio.ObtenerPorCategoria(id);
            return resultado;
        }
    }
}
