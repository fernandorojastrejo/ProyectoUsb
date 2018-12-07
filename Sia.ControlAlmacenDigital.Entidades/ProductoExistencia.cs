using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sia.ControlAlmacenDigital.Entidades
{
    public partial class ProductoExistencia
    {
        public int ProductoExistenciaId { get; set; }
        public int ProductoId { get; set; }
        public int Minimo { get; set; }
        public int Maximo { get; set; }
        public Nullable<int> Reservado { get; set; }
        public int Existente { get; set; }
        public DateTime? FechaArribo { get; set; }
        public virtual Producto Producto { get; set; }
    }
}