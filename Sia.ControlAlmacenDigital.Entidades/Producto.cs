using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sia.ControlAlmacenDigital.Entidades
{
    public partial class Producto
    {
        public int ProductoId { get; set; }
        public int CategoriaId { get; set; }
        public string Codigo { get; set; }
        public string CodigoBarra { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> Pieza { get; set; }
        public string Descripcion { get; set; }

        public string Color { get; set; }
        public string Material { get; set; }
        public string Capacidad { get; set; }

        public Nullable<bool> Activo { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual ICollection<Precio> Precio { get; set; }
        public virtual ICollection<ProductoExistencia> ProductoExistencia { get; set; }
        public virtual ICollection<ProductoFotografia> ProductoFotografia { get; set; }
        public string Medida { get; set; }
        public Nullable<bool> EsPrincipal { get; set; }
        public string CodigoPrincipal { get; set; }
        //Auxiliares
        public string DescripcionCorto { get; set; }
        public string RutaExcel { get; set; }
        
    }
}