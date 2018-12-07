using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Sia.AlmacenDigital.WebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sia.AlmacenDigital.WebMvc.IdentityExtentions
{
    public class CustomizePasswordApp
    {
        public class AppUserManager : UserManager<ApplicationUser>

        {

            public AppUserManager()

                : base(new UserStore<ApplicationUser>(new ApplicationDbContext()))

            {

                PasswordValidator = new MinimumLengthValidator(8);

            }

        }
    }
}