using Sia.ControlAlmacenDigital.Entidades;
using Sia.ControlAlmacenDigital.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sia.ControlAlmacenDigital.Controllers
{
    [RoutePrefix("api/Categoria")]
    public class CategoriaController : ApiController
    {
        [Route("")]
        public IEnumerable<Categoria> Get()
        {
            CategoriaNeg negocio = new CategoriaNeg();
            var resultado = negocio.Obtener();
            return resultado;
        }

        [Route("ObtenerDetalleCategoria/{id}")]
        public Categoria GetObtenerDetalleCategoria(int id)
        {
            CategoriaNeg negocio = new CategoriaNeg();
            var resultado = negocio.Obtener(id);
            return resultado;
        }

        [HttpPut]
        [Route("AgregarBannerTiempo")]
        public Resultado<Categoria> AgregarBannerTiempo(Categoria m)
        {
            CategoriaNeg negocio = new CategoriaNeg();
            return negocio.AgregarBannerTiempo(m);
        }
    }
}
