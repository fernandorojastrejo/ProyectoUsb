using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sia.ControlAlmacenDigital.Entidades
{
    public partial class Precio
    {
        public int PrecioId { get; set; }
        public int ProductoId { get; set; }
        public double PrecioUsuario { get; set; }
        public double Precio1 { get; set; }
        public Nullable<double> Precio2 { get; set; }
        public Nullable<double> Precio3 { get; set; }
        public Nullable<int> Mes { get; set; }
        public Nullable<int> ayo { get; set; }
        public Nullable<int> Emitio { get; set; }

        public virtual Producto Producto { get; set; }
    }
}