using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sia.ControlAlmacenDigital.Entidades
{
    public partial class ProductoFotografia
    {
        public int ProductoFotoId { get; set; }
        public int ProductoId { get; set; }
        public string UrlImagen { get; set; }

        public virtual Producto Producto { get; set; }

    }
}