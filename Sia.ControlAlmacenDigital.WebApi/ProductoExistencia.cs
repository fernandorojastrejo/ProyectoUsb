//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sia.ControlAlmacenDigital.WebApi
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductoExistencia
    {
        public int ProductoExistenciaId { get; set; }
        public int ProductoId { get; set; }
        public int Minimo { get; set; }
        public int Maximo { get; set; }
        public int Existente { get; set; }
        public Nullable<int> Reservado { get; set; }
        public Nullable<System.DateTime> FechaArribo { get; set; }
    
        public virtual Producto Producto { get; set; }
    }
}
