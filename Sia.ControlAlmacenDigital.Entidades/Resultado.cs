using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sia.ControlAlmacenDigital.Entidades
{
    public class Resultado<T>
    {
        public bool Error { get; set; } // defecto false
        public bool Exito { get; set; } // defecto false
                                        
        public string MensajeError { get; set; }
        public string MensajeExito { get; set; }
        public T ObjCreado { get; set; }
    }
}