using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sia.ControlAlmacenDigital.Entidades
{
    public class BuscarProductos
    {
        public int ProductoId { get; set; }
        public int CategoriaId { get; set; }
        public string Codigo { get; set; }
        public string CodigoBarra { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> Pieza { get; set; }
        public string Descripcion { get; set; }
        public string Color { get; set; }
        public string Capacidad { get; set; }
        public string Material { get; set; }
    }
}
