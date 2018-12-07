using Sia.ControlAlmacenDigital.Entidades;
using Sia.ControlAlmacenDigital.WebApi.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Sia.ControlAlmacenDigital.WebApi.Controllers
{
    [RoutePrefix("api/usuario")]
    public class UsuarioController : ApiController
    {
        [Route("obtener")]
        public List<Entidades.AspNetUsers> Get()
        {
            IdentidadNeg neg = new IdentidadNeg();
            List<Entidades.AspNetUsers> resultado = neg.Obtener();
            return resultado;
        }

        [Route("ObtenerDetalleUsuario/{id}")]
        public Entidades.AspNetUsers GetUsuarioId(string id)
        {
            IdentidadNeg neg = new IdentidadNeg();
            Entidades.AspNetUsers resultado = neg.ObtenerPorId(id);
            return resultado;
        }

        [HttpPut]
        [Route("EditarUsuario")]
        public async Task<string> EditarUsuario(UsuarioEditar usuario)
        {
            IdentidadNeg neg = new IdentidadNeg();
            string mensaje = await neg.EditarUsuario(usuario);
            return mensaje;
        }

        
        [Route("EditarRolUsuario")]
        public async Task<string> EditarRolUsuario(EditarRolUsuario modelo)
        {
            IdentidadNeg neg = new IdentidadNeg();
            string mensaje = await neg.EditarRolUsuario(modelo);
            return mensaje;
        }

        [Route("EditarPasswordUsuario")]
        public async Task<string> EditarPasswordUsuario(EditarPasswordUsuario modelo)
        {
            IdentidadNeg neg = new IdentidadNeg();
            string mensaje = await neg.EditarPasswordUsuario(modelo);
            return mensaje;
        }

        [Route("EditarEstatusUsuario")]
        public async Task<string> EditarEstatusUsuario(UsuarioEditar modelo)
        {
            IdentidadNeg neg = new IdentidadNeg();
            string mensaje = await neg.EditarEstatusUsuario(modelo);
            return mensaje;
        }
    }
}
