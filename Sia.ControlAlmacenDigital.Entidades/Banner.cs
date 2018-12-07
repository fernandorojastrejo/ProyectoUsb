using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sia.ControlAlmacenDigital.Entidades
{
    public class Banner
    {
        public int BannerId { get; set; }
        public int CategoriaId { get; set; }
        public string UrlImagen { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}
