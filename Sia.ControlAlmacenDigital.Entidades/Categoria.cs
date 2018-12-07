using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sia.ControlAlmacenDigital.Entidades
{
    public partial class Categoria
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Categoria()
        {
            this.Producto = new HashSet<Producto>();
        }

        public int CategoriaId { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> TiempoBanner { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Producto> Producto { get; set; }
    }
}