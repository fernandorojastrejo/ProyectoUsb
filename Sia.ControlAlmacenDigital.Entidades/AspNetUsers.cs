using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sia.ControlAlmacenDigital.Entidades
{
   public class AspNetUsers
    {
        public AspNetUsers()
        {
            this.AspNetRoles = new HashSet<AspNetRoles>();
        }

        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public Nullable<bool> LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }

        public Nullable<int> TipoPrecio { get; set; }
        public bool Activo { get; set; }
        public virtual ICollection<AspNetRoles> AspNetRoles { get; set; }

        public string UrlRedireccion { get; set; }
        public string NombreCompleto { get; set; }
    }
}
