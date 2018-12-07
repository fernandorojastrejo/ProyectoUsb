using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sia.ControlAlmacenDigital.Entidades
{
    public class UsuarioEditar
    {
        public string Id { get; set; }

        public string Nombres { get; set; }

        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }
        public bool Activo { get; set; }
        public int? TipoPrecio { get; set; }

    }
}
